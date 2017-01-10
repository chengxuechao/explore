using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using api;

/***
 * SocketNetwork.cs
 * 
 * @author abaojin
 */ 
namespace GameCore
{
    public class SocketNetwork : BaseNetwork
    {
        private Socket mSocket = null;
        private string mIp = null;
        private int mPort = 0;

        // 缓冲区大小
        private int mRecvLen = 1024 * 1024;

        // 读取数据Buff
        private byte[] mReadBuff;
        // 临时数据Buff
        private byte[] mRecvBuff;

        // 临时读取数据位置
        private int mReceivePos = 0;

        // 重连的次数
        private int mReconnectCount = 0;

        // 返回数据处理线程
        private Thread mThread = null;
        // 是否停止接收socket数据接收线程
        private bool mStopRecvThread = false;

        private static ManualResetEvent mConnectDone = new ManualResetEvent(false);
        private static ManualResetEvent mSendDone = new ManualResetEvent(false);
        private static ManualResetEvent mReceiveDone = new ManualResetEvent(false);

        // 链接超时时间
        private static int mTimeoutMillisecond = 500;

        /// <summary>
        /// 创建Socket
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static SocketNetwork NewObject(string ip, int port)
        {
            return new SocketNetwork(ip, port);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        private SocketNetwork(string ip, int port)
        {
            // 连接信息
            mIp = ip;
            mPort = port;
            mReconnectCount = 0;
            mThread = null;
            mNetState = NetState.Disconnect;

            // 缓存队列
            mReadBuff = new byte[mRecvLen];
            mRecvBuff = new byte[mRecvLen];
            mReceivePos = 0;

            // 创建连接
            ConnectServer();
        }

        #region 对外接口

        /// <summary>
        /// 创建Socket链接
        /// </summary>
        public void ConnectServer()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(mIp), mPort);
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mSocket.SendBufferSize = mRecvLen;

            // 异步连接Socket
            mSocket.BeginConnect(endPoint, (IAsyncResult async) => {
                try {
                    mSocket.EndConnect(async);
                    mNetState = NetState.Connected;
                    mReconnectCount = 0;
                    StartThread();
                } catch (SocketException ex) {
                    SocketException(ex);
                } finally {
                    mConnectDone.Set();
                }
            }, null);

            // TimeOut
            if (mConnectDone.WaitOne(mTimeoutMillisecond, false)) {
                if (IsConnect()) {
                    return;
                }
                throw new TimeoutException("Socket Connect Exception...");
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public override void SendMessage(PBBody pbbody)
        {
            PBPacket packet = PBUtils.NewPacket(pbbody);
            try {
                mSendQueue.Enqueue(packet);
            } catch (Exception ex) {
                // TODO
            }
        }

        /// <summary>
        /// 断开游戏链接
        /// </summary>
        public override void Disconnect()
        {
            try {
                mNetState = NetState.Disconnect;
                AbortThread();
                if (mSocket == null) {
                    return;
                }
                mSocket.Shutdown(SocketShutdown.Both);
                mSocket.Close();
            } catch (Exception e) {
            } finally {
                mSocket = null;
            }
        }

        /// <summary>
        /// 是否链接成功
        /// </summary>
        /// <returns></returns>
        public override bool IsConnect()
        {
            if (mSocket == null) {
                return false;
            }
            return (mNetState == NetState.Connected);
        }

        /// <summary>
        /// 断线重连
        /// </summary>
        public override void Reconnect()
        {
            Disconnect();
            // 记录客户端链接次数
            mReconnectCount++;
            if (mReconnectCount >= NetworkConst.RECONNECT_COUNT) {
                mReconnectCount = 0;
                mNetState = NetState.TimeOut;
            } else {
                ConnectServer();
            }
        }

        /// <summary>
        /// 发送消息函数
        /// </summary>
        public override void ProcessSendPacket()
        {
            if (mSendQueue == null) {
                return;
            }
            while (mSendQueue.Count > 0) {
                if (!IsConnect()) {
                    continue;
                }

                // 当前待发送消息包
                try {
                    PBPacket packet = mSendQueue.Dequeue();

                    NGUIDebug.Log("headlen: " + PBConst.PB_HEAD_SIZE);
                    NGUIDebug.Log("bodylen: " + packet.mHead.mBodyLen);
                    NGUIDebug.Log(packet.mHead.mMessageId);

                    byte[] buffer = packet.Encoder();
                    mSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, (IAsyncResult async) => {
                        try {
                            int sendLen = mSocket.EndSend(async);
                        } catch (Exception ex) {
                            // TODO
                        }
                    }, null);
                } catch (Exception ex) {
                }
            }
        }

        #endregion

        /// <summary>
        /// 接收数据线程
        /// </summary>
        void ProcessReceiveThread()
        {
            while (!mStopRecvThread) {
                try {
                    if (!IsConnect()) {
                        mNetState = NetState.Disconnect;
                        return;
                    }
                    // 清空临时缓冲区
                    Array.Clear(mReadBuff, 0, mRecvLen);
                    int length = mSocket.Receive(mReadBuff);
                    // 接受数据长度
                    if (length > 0) {
                        Buffer.BlockCopy(mReadBuff, 0, mRecvBuff, mReceivePos, length);
                        // LogUtils.Log("Once Receive Msg Size: " + length, LType.Normal);
                        TryParsePacket(length);
                    }
                } catch (Exception e) {
                    // TODO
                }
            }
        }

        /// <summary>
        /// 处理缓冲区中的数据包
        /// </summary>
        /// <param name="recvLen"></param>
        void TryParsePacket(int recvLen)
        {
            // 更新标识
            mReceivePos += recvLen;
            // 包头长度
            int headerSize = PBConst.PB_HEAD_SIZE;
            while (mReceivePos >= headerSize) {
                try {
                    // 获取到消息包体长度
                    int bodySize = BitConverter.ToInt32(mRecvBuff, 0);
                    int packetSize = headerSize + bodySize;
                    // 检查是否满足一个包长度
                    if (mReceivePos >= packetSize) {
                        PBPacket packet = new PBPacket();
                        packet.Decoder(mRecvBuff);
                        if (packet == null) {
                            continue;
                        }
                        EnqueuePacket(packet);
                        // 接收缓冲区偏移处理
                        for (int i = 0; i < (mReceivePos - packetSize); i++) {
                            mRecvBuff[i] = mRecvBuff[packetSize + i];
                        }
                        mReceivePos -= packetSize;
                    } else {
                        break;
                    }
                } catch (Exception e) {
                    break;
                }
            }
        }

        /// <summary>
        /// 启动线程
        /// </summary>
        void StartThread()
        {
            AbortThread();
            mThread = new Thread(new ThreadStart(ProcessReceiveThread));
            mThread.IsBackground = true;
            mThread.Start();
        }

        /// <summary>
        /// 关闭线程
        /// </summary>
        void AbortThread()
        {
            if (mThread != null) {
                mStopRecvThread = true;
                // 清空缓冲区
                Array.Clear(mRecvBuff, 0, mRecvLen);
                Array.Clear(mReadBuff, 0, mRecvLen);
                mReceivePos = 0;
                mThread.Abort();
                mThread = null;
            }
        }

        /// <summary>
        /// Socket连接异常
        /// </summary>
        void SocketException(SocketException se)
        {
            mNetState = NetState.Exception;
            switch ((SocketErrorCode)se.ErrorCode) {
                case SocketErrorCode.ConnectionRefused:
                    // 连接拒绝处理
                    break;
                case SocketErrorCode.ConnectionTimeout:
                    // 连接超时处理
                    break;
                case SocketErrorCode.HostNotNetwork:
                    // 本地网络不通
                    break;
            }
        }
    }

}

