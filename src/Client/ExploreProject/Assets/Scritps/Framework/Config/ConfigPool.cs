using System;
using System.Collections.Generic;
using System.Reflection;

/***
 * ConfigPool.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class ConfigPool : Singleton<ConfigPool>
    {
        // Game表格
        private static Dictionary<Type, Dictionary<int, BaseConfig>> configPool = new Dictionary<Type, Dictionary<int, BaseConfig>>();

        // 表格初始化
        public void Init()
        {
            // InitCfg<PlayerLevelTbl, PlayerLevelCfg>();
            // InitCfg<LevelInfoTbl, LevelInfoCfg>();
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <typeparam name="TblClass"></typeparam>
        /// <typeparam name="CfgClass"></typeparam>
        private void InitCfg<TblClass, CfgClass>() where CfgClass : class, new()
        {
            CfgClass cfg = new CfgClass();
            Type type = cfg.GetType();
            MethodInfo info = type.GetMethod("Init");
            if (info != null) {
                Dictionary<int, BaseConfig> config = info.Invoke(cfg, null) as Dictionary<int, BaseConfig>;
                configPool.Add(typeof(TblClass), config);
            } else {
                Log.Error("Table Is Not Exist Init Method");
            }
            cfg = null;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Release()
        {
            foreach (var node in configPool) {
                node.Value.Clear();
            }
            configPool.Clear();
        }

        /// <summary>
        /// 根据表ID获取单个表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetTblByKey<T>(int id) where T : BaseConfig
        {
            Type type = typeof(T);
            if (!configPool.ContainsKey(type)) {
                Log.Error("Table Not Exist Table, TableName: " + type.Name + " id: " + id);
                return null;
            }
            if (!GetData<T>().ContainsKey(id)) {
                Log.Error("Table Not Exist Id, TableName: " + type.Name + " id: " + id);
                return null;
            }
            return (T)GetData<T>()[id];
        }

        /// <summary>
        /// 获取单张表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, BaseConfig> GetData<T>() where T : BaseConfig
        {
            Type type = typeof(T);
            if (configPool.ContainsKey(type)) {
                return configPool[type];
            }
            return null;
        }

        /// <summary>
        /// 获取一张表数据大小
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetDataSize<T>() where T : BaseConfig
        {
            Type type = typeof(T);
            if (configPool.ContainsKey(type)) {
                return configPool[type].Count;
            }
            return 0;
        }

        /// <summary>
        /// 是否包含表格
        /// </summary>
        /// <returns><c>true</c> if has config; otherwise, <c>false</c>.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool HasConfig<T>() where T : BaseConfig
        {
            return configPool.ContainsKey(typeof(T));
        }

        /// <summary>
        /// 获取国际化Key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetI18N(int id)
        {

            // string dictionaryCon = GetTblByKey<DictionaryTbl>(id).Name;
            // string dicTmp = dictionaryCon.Replace("\\n", "\n");
            // return dicTmp;
            return "";
        }

    }
}

