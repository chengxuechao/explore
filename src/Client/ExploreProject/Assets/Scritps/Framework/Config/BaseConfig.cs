using System;
using UnityEngine;

/***
 * BaseConfig.cs
 * 
 * @author abaojin
 */
public class BaseConfig
{
    /// <summary>
    /// 获取指定数据数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="segment"></param>
    /// <returns></returns>
    public T[] TryData<T>(string value, char segment = ';') where T : struct 
    {
        string[] valArr = value.Split(segment);
        int count = valArr.Length;
        T[] t = new T[count];
        for (int i = 0; i < count; i++) 
        {
            object val = Convert.ChangeType(valArr[i], typeof(T));
            t[i] = (T)val;
        }
        return t;
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="value"></param>
    /// <param name="segment"></param>
    /// <returns></returns>
    public string[] TryString(string value, char segment = ';')
    {
        string[] valArr = value.Split(segment);
        return valArr;
    }

    /// <summary>
    /// 解析向量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="segment"></param>
    /// <returns></returns>
    public Vector3 TryVector3(string value, char segment = ';')
    {
        Vector3 vector = Vector3.zero;
        string[] arr = value.Split(segment);
        if(arr.Length == 3)
        {
            float x = Convert.ToSingle(arr[0]);
            float y = Convert.ToSingle(arr[1]);
            float z = Convert.ToSingle(arr[2]);
            vector = new Vector3(x, y, z);
        } 
        else if(arr.Length == 2)
        {
            float x = Convert.ToSingle(arr[0]);
            float y = Convert.ToSingle(arr[1]);
            vector = new Vector3(x, y, 0);
        }
        return vector;
    }

}