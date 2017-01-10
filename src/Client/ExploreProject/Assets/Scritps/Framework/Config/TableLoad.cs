using System.Text;
using UnityEngine;

/***
 * TableLoad.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class TableLoad
    {
        public static Table LoadTbl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) {
                Debug.LogError("FileName Is Name " + fileName);
                return null;
            }
            Table table = new Table();
            TextAsset text = Resources.Load(fileName) as TextAsset;
            string tableContext = text.text;

            UTF8Encoding utf8 = new UTF8Encoding();
            string content = utf8.GetString(utf8.GetBytes(tableContext));

            string[] rowArray = content.Split('\n');
            int length = rowArray.Length;
            for (int i = 0; i < length; i++) {
                string line = rowArray[i];
                if (line.StartsWith("#")) {
                    continue;
                }
                string[] fields = line.Split('\t');
                if (fields.Length > 1) {
                    table.LoadLine(fields);
                }
            }
            return table;
        }
    }
}

