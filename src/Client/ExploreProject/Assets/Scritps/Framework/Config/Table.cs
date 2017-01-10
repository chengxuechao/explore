using System.Collections.Generic;
using UnityEngine;

/***
 * Table.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class Table
    {
        // 类型
        public List<string> type;
        // 名称
        public List<string> name;
        // 数据
        public List<string[]> records;
        // 名称索引
        public Dictionary<string, int> indexMap;

        public Table()
        {
            type = new List<string>();
            name = new List<string>();
            records = new List<string[]>();
            indexMap = new Dictionary<string, int>();
        }

        public string FindField(int row, string nameCol)
        {
            if (row < 0 || row > records.Count) {
                Debug.LogError("Row Error!");
                return null;
            }
            if (!indexMap.ContainsKey(nameCol)) {
                Debug.LogError("Index Map Not ContainsKey: " + nameCol);
                return null;
            }
            int col = indexMap[nameCol];
            return records[row][col].Trim();
        }

        public int GetInt(int row, string nameCol)
        {
            int result = 0;
            int.TryParse(FindField(row, nameCol), out result);
            return result;
        }

        public string GetString(int row, string nameCol)
        {
            string value = FindField(row, nameCol);
            if (value == null) {
                return "";
            }
            return value;
        }

        public float GetFloat(int row, string nameCol)
        {
            float result = 0;
            float.TryParse(FindField(row, nameCol), out result);
            return result;
        }

        /// <summary>
        ///  加载表行
        /// </summary>
        /// <param name="line">Line.</param>
        public void LoadLine(string[] line)
        {
            if (type.Count == 0) {
                ReadType(line);
                return;
            }
            if (name.Count == 0) {
                ReadName(line);
                return;
            }
            ReadRecord(line);
        }

        /// <summary>
        /// 读取表类型 
        /// </summary>
        /// <param name="line">Line.</param>
        private void ReadType(string[] line)
        {
            type.AddRange(line);
        }

        /// <summary>
        /// 读取表名
        /// </summary>
        /// <param name="line">Line.</param>
        private void ReadName(string[] line)
        {
            if (line == null) {
                Debug.Log("Line Data Is Null!");
                return;
            }
            name.AddRange(line);
            int count = line.Length;
            for (int i = 0; i < count; i++) {
                string lineName = line[i].Trim();
                lineName = lineName.Substring(0, 1).ToUpper() + lineName.Substring(1, lineName.Length - 1);
                indexMap[lineName] = i;
            }
        }

        /// <summary>
        /// 读取表数据
        /// </summary>
        /// <param name="line">Line.</param>
        private void ReadRecord(string[] line)
        {
            if (line.Length != type.Count) {
                Debug.LogError("Record Data Null, Length=" + line.Length + ",type=" + type.Count);
                return;
            }
            records.Add(line);
        }
    }
}