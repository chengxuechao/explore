using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/***
 * ResourceLoad.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class ResourceLoad
    {
        // 内存中资源
        private static Dictionary<string, UnityEngine.Object> mResInMemory = new Dictionary<string, UnityEngine.Object>();

        /// <summary>
        /// 同步加载T（特定）资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            T t = GetObject<T>(path);
            if (t == null) {
                UnityEngine.Debug.LogError("Resource Load Res :" + path + " fail...");
                return null;
            }
            return (T)GameObject.Instantiate(t);
        }

        /// <summary>
        /// 异步加载T（特定）资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        public static void LoadAsyn<T>(string path, Action<T> finishCallback) where T : UnityEngine.Object
        {
            // BehaviourUtils.StartCorhavior(GetSyncObject<T>(path, finishCallback));
        }

        /// <summary>
        ///  释放资源（切换场景时释放所有资源）
        /// </summary>
        public static void UnloadAsset()
        {
            if (mResInMemory == null) {
                return;
            }
            mResInMemory.Clear();
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        /// <summary>
        /// 异步加载对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        private static IEnumerator GetSyncObject<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            T t = null;
            if (mResInMemory.ContainsKey(path)) {
                t = (T)mResInMemory[path];
                if (callback != null && t != null) {
                    callback((T)GameObject.Instantiate(t));
                }
                yield break;
            }
            ResourceRequest rr = Resources.LoadAsync<T>(path);
            while (!rr.isDone) {
                yield return null;
            }
            t = (T)rr.asset;
            mResInMemory.Add(path, t);
            if (callback != null && t != null) {
                callback((T)GameObject.Instantiate(t));
            }
        }

        /// <summary>
        /// 获取资源对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T GetObject<T>(string path) where T : UnityEngine.Object
        {
            if (mResInMemory == null) {
                return null;
            }
            if (mResInMemory.ContainsKey(path)) {
                return (T)mResInMemory[path];
            }
            T t = Resources.Load<T>(path);
            mResInMemory.Add(path, t);
            return t;
        }

        /// <summary>
        ///  是否包含该资源
        /// </summary>
        /// <returns></returns>
        /// <param name="path">Path.</param>
        public static bool HasRes(string path)
        {
            if (mResInMemory == null) {
                return false;
            }
            return mResInMemory.ContainsKey(path);
        }
    }
}

