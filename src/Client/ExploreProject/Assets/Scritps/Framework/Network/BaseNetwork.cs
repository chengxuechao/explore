using api;
using System;
using System.Collections.Generic;
using UnityEngine;

/***
 * BaseNetwork.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    /// <summary>
    /// 网络层基类
    /// </summary>
    public abstract class BaseNetwork
    {
        // 多线程锁
        protected object mLockObj = new object();

        // 网络状态
        public static NetState mNetState = NetState.Disconnect;

        // 发送消息队列
        protected Queue<PBPacket> mSendQueue = new Queue<PBPacket>();
        // 接受消息队列
        protected Queue<PBPacket> mRcevQueue = new Queue<PBPacket>();

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        public virtual void SendMessage(PBBody msg, string key) { }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public virtual void SendMessage(PBBody msg) { }

        /// <summary>
        /// 断开链接
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// 网络心跳
        /// </summary>
        public virtual void Update()
        {
            try {
                // 客户端断线检测
                if (!IsConnect()) {
                    mNetState = NetState.Disconnect;
                    return;
                }
                // 处理发送消息包
                if (mSendQueue.Count > 0) {
                    ProcessSendPacket();
                }
                // 处理返回消息包
                if (mRcevQueue.Count > 0) {
                    ProcessBackPacket();
                }
            } catch (Exception e) {
                Debug.LogError("Socket Update Heart Exception: " + e.Message);
            }
        }

        /// <summary>
        /// 重新链接
        /// </summary>
        public virtual void Reconnect()
        {

        }

        /// <summary>
        /// 处理发送数据
        /// </summary>
        public virtual void ProcessSendPacket()
        {

        }

        /// <summary>
        /// 处理接收数据
        /// </summary>
        public virtual void ProcessBackPacket()
        {
            int count = 0;
            while (mRcevQueue.Count > 0) {
                PBPacket packet = null;
                lock (mLockObj) {
                    packet = mRcevQueue.Dequeue();
                }
                if (packet != null) {
                    NetworkManager.Instance.ExecResult(packet);
                }
                count++;
                if (count > NetworkConst.FRAME_MAX_PROCESS) {
                    break;
                }
            }
        }

        /// <summary>
        /// 是否链接成功
        /// </summary>
        /// <returns></returns>
        public virtual bool IsConnect()
        {
            return false;
        }

        /// <summary>
        /// 将包压入接收队列
        /// </summary>
        /// <param name="msg"></param>
        public virtual void EnqueuePacket(PBPacket msg)
        {
            lock (mLockObj) {
                mRcevQueue.Enqueue(msg);
            }
        }

    }
}
