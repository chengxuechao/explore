using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***
 * NetworkConst.cs
 * 
 * @author abaojin
 */ 
namespace GameCore
{
    public class NetworkConst
    {
        /*
         * 网络重连次数 
         */
        public const byte RECONNECT_COUNT = 3;

        /*
         * 每帧处理包最大数
         */ 
        public const byte FRAME_MAX_PROCESS = 10;
    }
}
