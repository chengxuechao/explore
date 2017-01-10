using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * TimerSched.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class TimerSched
    {
        // 调度器字典
        private static Dictionary<string, TimerTask> mTimerTaskMap = new Dictionary<string, TimerTask>();

        /// <summary>
        /// 注册计时器
        /// </summary>
        /// <param name="taskKey">计时器Key</param>
        /// <param name="callback">回调</param>
        /// <param name="totalTime">总时间,-1代表一直计时（毫秒）</param>
        /// <param name="intervalTime">间隔时间（毫秒）</param>
        /// <param name="endCallback">计时结束回调</param>
        public static void Register(string taskKey, Action<long> callback, long totalTime = -1, int delayTime = 0, float intervalTime = 1, Action endCallback = null)
        {
            TimerTask timerTask = null;
            if (!mTimerTaskMap.ContainsKey(taskKey)) {
                timerTask = new TimerTask();
                mTimerTaskMap.Add(taskKey, timerTask);
            } else {
                timerTask = mTimerTaskMap[taskKey];
            }
            if (timerTask != null) {
                timerTask.RunLogic(totalTime, intervalTime, delayTime, callback, endCallback);
            }
        }

        /// <summary>
        /// 停止计时器
        /// </summary>
        /// <param name="timerKey">Timer key.</param>
        public static void StopTimer(string timerKey)
        {
            if (!mTimerTaskMap.ContainsKey(timerKey)) {
                return;
            }
            TimerTask timerItem = mTimerTaskMap[timerKey];
            if (timerItem != null) {
                timerItem.Stop(true);
            }
        }

        /// <summary>
        /// 移除计时器
        /// </summary>
        /// <param name="timeKey"></param>
        public static void UnRegister(string timeKey)
        {
            if (!mTimerTaskMap.ContainsKey(timeKey)) {
                return;
            }
            TimerTask timerTask = mTimerTaskMap[timeKey];
            if (timerTask != null) {
                timerTask.Stop(true);
                timerTask = null;
                mTimerTaskMap.Remove(timeKey);
            }
        }
    }

    /// <summary>
    /// Desc: 计时组件对象
    /// Author: xiangjinbao
    /// </summary>
    public class TimerTask
    {
        // 总时间(毫秒)
        private long mTotalTime;
        // 延迟（秒）
        private int mDelayTime;
        // 计时间隔时间（秒）
        private float mIntervalTime;
        // 计时间隔回调
        private Action<long> mCallback;
        // 计时结束回调
        private Action mEndCallback;
        // 当前剩余时间
        private float mCurrentTime;
        // 停止计时器
        private bool mStopTimer = false;

        // 等待时间对象
        private WaitForSeconds mWaitSeconds;

        /// <summary>
        /// 运行计时器逻辑
        /// </summary>
        /// <param name="total"></param>
        /// <param name="interTime"></param>
        /// <param name="callback"></param>
        /// <param name="endCallback"></param>
        public void RunLogic(long total, float interTime, int delayTime, Action<long> callback, Action endCallback)
        {
            this.Stop(false);
            this.mTotalTime = total;
            this.mDelayTime = delayTime;
            this.mCurrentTime = (total / 1000.0f);
            this.mIntervalTime = (interTime / 1000.0f);
            this.mCallback = callback;
            this.mEndCallback = endCallback;
            this.mWaitSeconds = new WaitForSeconds(this.mIntervalTime);

            // 进入计时间隔
            // MonoDelegator.Instance.StartCoroutine(IntervalAction());
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop(bool isStop)
        {
            this.mStopTimer = isStop;
        }

        /// <summary>
        /// 计时间隔函数
        /// </summary>
        /// <returns></returns>
        private IEnumerator IntervalAction()
        {
            if (this.mStopTimer) {
                yield break;
            }
            if (this.mDelayTime > 0) {
                yield return new WaitForSeconds(this.mDelayTime);
                this.mDelayTime = 0;
            }
            yield return this.mWaitSeconds;
            this.mCurrentTime -= this.mIntervalTime;
            // 经过时间
            long passTime = (long)(this.mCurrentTime * 1000);
            // 间隔回调
            if (this.mCallback != null) {
                if (passTime < 0) {
                    this.mCallback(0);
                } else {
                    this.mCallback(passTime);
                }
            }
            // 是否结束
            if (this.mTotalTime != -1) {
                if (passTime <= 0) {
                    if (this.mEndCallback != null) {
                        this.mEndCallback();
                    }
                    yield break;
                }
            }
            // MonoDelegator.Instance.StartCoroutine(IntervalAction());
        }
    }
}
