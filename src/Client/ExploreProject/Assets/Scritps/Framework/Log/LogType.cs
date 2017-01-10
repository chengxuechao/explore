using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***
 * LogType.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public enum LogType
    {
        // 正常
        Normal,
        // 输出信息
        Assert,
        // 警告
        Warning,
        // 错误
        Error,
        // 致命
        Fatal
    }
}
