using UnityEngine;

/***
 * TransformHelper.cs
 * 
 * @author abaojin 
 */
namespace GameCore
{
    public static class TransformHelper
    {
        /// <summary>
        /// 搜索指定名字对象
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="childName"></param>
        /// <param name="recursivelyDepth"></param>
        /// <returns></returns>
        public static Transform SearchChild(this Transform tr, string childName, int recursivelyDepth = -1)
        {
            if (string.IsNullOrEmpty(childName)) {
                return null;
            }

            return DoSearchChild(tr, childName, recursivelyDepth);
        }

        /// <summary>
        /// 递归寻找对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="childName"></param>
        /// <param name="recursivelyDepth"></param>
        /// <returns></returns>
        private static Transform DoSearchChild(Transform tr, string childName, int recursivelyDepth)
        {
            Transform child = tr.FindChild(childName);
            if (null != child) {
                return child;
            }
            if (recursivelyDepth != 0) {
                if (recursivelyDepth > 0) {
                    recursivelyDepth--;
                }
                foreach (Transform t in tr.transform) {
                    child = DoSearchChild(t, childName, recursivelyDepth);
                    if (child != null) {
                        return child;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetMissingScript<T>(this Transform tr) where T : Component
        {
            T t = tr.GetComponent<T>();
            if (t == null) {
                t = tr.gameObject.AddComponent<T>();
            }
            return t;
        }
        
        /// <summary>
        /// 重置Transform
        /// </summary>
        /// <param name="go"></param>
        public static void Reset(this Transform tr)
        {
            tr.localPosition = Vector3.zero;
            tr.localRotation = Quaternion.identity;
            tr.localScale = Vector3.one;
        }

    }
}

