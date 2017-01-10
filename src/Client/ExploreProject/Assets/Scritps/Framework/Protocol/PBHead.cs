using System;
using System.Runtime.InteropServices;

/***
 * PBHead.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    /// <summary>
    /// 注意字节对其方式
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PBHead
    {
        // 消息长度（包体）
        public Int32 mBodyLen { get; set; }
        // 消息ID
        public Int16 mMessageId { get; set; }
        // 消息时间戳
        public Int64 mTimeStamp { get; set; }
    }
}

