using api;
using System;
using System.Collections.Generic;
using System.Net;

/***
 * HttpNetwork.cs
 * 
 * @author abaojin
 */ 
namespace GameCore
{
    public class HttpNetwork : BaseNetwork
    {
        // URL
        private string mURL;
        // 请求URL
        private string mReqUrl;
        // 请求方法
        private string mHttpMethod = "POST";
        // http错误码
        private Queue<HttpStatusCode> mStatueCodeQueue = new Queue<HttpStatusCode>();
        // 是否可以发送消息
        public static bool mCanSend = true;

        /// <summary>
        /// 获取一个Http对象
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpNetwork NewObject(string url)
        {
            return new HttpNetwork(url);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        private HttpNetwork(string url)
        {
            mURL = url;
        }

        #region 对外接口

        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// 发送Http消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        public override void SendMessage(PBBody msg, string key = null)
        {
            PBPacket packet = PBUtils.NewPacket(msg);
            if (string.IsNullOrEmpty(key)) {
                return;
            }
            mReqUrl = mURL + key;
            try {
                mSendQueue.Enqueue(packet);
            } catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 断开连接处理
        /// </summary>
        public override void Disconnect()
        {
            mCanSend = true;
            mStatueCodeQueue.Clear();

            mSendQueue.Clear();
            mRcevQueue.Clear();
        }

        /// <summary>
        /// 是否连接上
        /// </summary>
        /// <returns></returns>
        public override bool IsConnect()
        {
            return mCanSend;
        }

        /// <summary>
        /// 处理发送消息包
        /// </summary>
        public override void ProcessSendPacket()
        {
            if (mSendQueue == null) {
                return;
            }
            try {
                if (!IsConnect()) {
                    return;
                }
                mCanSend = false;
                PBPacket packet = mSendQueue.Dequeue();
                byte[] bytes = packet.Encoder();

                // 发送消息
                WebClient webClient = new WebClient();
                webClient.UploadDataCompleted += ProcessReceiveThreadCallback;
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webClient.UploadDataAsync(new Uri(mReqUrl), mHttpMethod, bytes);

                // LogUtils.Log("Http Send: " + JsonConvert.SerializeObject(msg), LType.Normal);
            } catch (Exception e) {
            }
        }

        /// <summary>
        /// 处理返回消息包
        /// </summary>
        public override void ProcessBackPacket()
        {
            while (mRcevQueue.Count > 0) {
                PBPacket packet = null;
                lock (mLockObj) {
                    packet = mRcevQueue.Dequeue();
                }
                if (packet != null) {
                    NetworkManager.Instance.ExecResult(packet);
                } else {
                }
            }
        }

        #endregion

        /// <summary>
        /// 异步接收数据线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProcessReceiveThreadCallback(object sender, UploadDataCompletedEventArgs e)
        {
            try {
                // 消息异常处理
                if (e.Error != null) {
                    string errorState = e.Error.GetType().Name;
                    if (errorState == "WebException") {
                        WebException webExp = (WebException)e.Error;
                        HttpWebResponse response = (HttpWebResponse)webExp.Response;
                        mStatueCodeQueue.Enqueue(response.StatusCode);
                    }
                    return;
                }
                // httpClient
                // WebClient mClient = (WebClient)sender;
                // 返回结果
                if (e.Result != null && e.Result.Length > 0) {
                    byte[] buffer = e.Result;
                    PBPacket packet = new PBPacket();
                    packet.Decoder(buffer);
                    EnqueuePacket(packet);
                }
            } catch (Exception ex) {
            } finally {
                mCanSend = true;
            }
        }

    }
}

