using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;
using UnityEngine;

/***
 * LogDebug.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public static class Log
    {
        private static object lockObj = new object();
        private static Mutex mutex = new Mutex();
        private static bool isOpen = true;

        private static Queue<string> mLogQueue = new Queue<string>();
        private static System.Timers.Timer mFlushTimer = new System.Timers.Timer(10000);

        // 临时输出变量
        public static LinkedList<object> mOutList = new LinkedList<object>();
        // 日志文件名称
        private static string mLogFileName;
        // 是否初始化
        private static bool mIsInit = false;

        // 日志初始化
        private static void Init()
        {
            mIsInit = true;
            System.DateTime dt = System.DateTime.Now;
            mLogFileName = Application.persistentDataPath + "/" + dt.ToFileTimeUtc() + ".log";
            mFlushTimer.Enabled = true;
            mFlushTimer.Elapsed += new ElapsedEventHandler(WriteLog);

            UnityEngine.Debug.Log("Output log file in: " + mLogFileName);
        }

        public static void Close()
        {
            WriteLog(null, null);
            mLogFileName = null;
        }

        public static void Error(object message)
        {
            LogText(message.ToString(), LogType.Error);
        }

        public static void Warning(object message)
        {
            LogText(message.ToString(), LogType.Warning);
        }

        public static void Info(object message)
        {
            LogText(message.ToString(), LogType.Normal);
        }

        private static void LogText(string message, LogType type)
        {
            if (!mIsInit) {
                Init();
            }
            if (null != mLogFileName) {
                WriteLog(mLogFileName, message, type);
            }
            if (!isOpen) {
                return;
            }
            try {
                switch (type) {
                    case LogType.Normal:
                        UnityEngine.Debug.Log(message);
                        break;
                    case LogType.Assert:
                        UnityEngine.Debug.Log(message);
                        break;
                    case LogType.Warning:
                        UnityEngine.Debug.LogWarning(message);
                        break;
                    case LogType.Error:
                    case LogType.Fatal:
                        UnityEngine.Debug.LogError(message);
                        break;
                    default:
                        UnityEngine.Debug.LogError(message);
                        break;
                }
            } catch (System.Exception ex) {
                ex.ToString();
            }
        }

        private static void WriteLog(string filePath, string strInfo, LogType logType)
        {
            mutex.WaitOne();

            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strType = logType.ToString();

            JsonData logHead = new JsonData();
            logHead["Time"] = strTime;
            logHead["Type"] = strType;

            JsonData logBody = new JsonData();
            logBody["Property"] = logHead;
            logBody["Content"] = strInfo;

            string message = logBody.ToJson();
            message = message.Replace("\\", "");
            mLogQueue.Enqueue(message);

            EnqueueLog(message);

            mutex.ReleaseMutex();
        }

        public static bool DeleteLog(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists) {
                return false;
            } else {
                fi.Delete();
                return true;
            }
        }

        private static void EnqueueLog(string message)
        {
            if (mOutList.Count > 200) {
                mOutList.RemoveLast();
            }
            mOutList.AddFirst(message);
        }

        private static void WriteLog(object source, ElapsedEventArgs e)
        {
            mutex.WaitOne();

            StreamWriter writer = File.AppendText(mLogFileName);
            while (mLogQueue.Count > 0) {
                string message = mLogQueue.Dequeue();
                writer.WriteLine(message);
            }
            writer.Close();

            mutex.ReleaseMutex();
        }
    }
}

