using api;
using ProtoBuf.Meta;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

/***
 * PBHead.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public static class PBUtils
    {
        /// <summary>
        /// 转换字节序
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static PBHead ConvertByteOrder(PBHead header)
        {
            header.mBodyLen = IPAddress.HostToNetworkOrder(header.mBodyLen);
            header.mMessageId = IPAddress.HostToNetworkOrder(header.mMessageId);
            header.mTimeStamp = IPAddress.HostToNetworkOrder(header.mTimeStamp);
            return header;
        }

        /// <summary>
        /// 获取包体大小
        /// </summary>
        /// <param name="protoBuff"></param>
        /// <returns></returns>
        public static Int32 GetPBBodyLength(PBBody pbbody)
        {
            int length = 0;
            using (MemoryStream m = new MemoryStream()) {
                RuntimeTypeModel.Default.Serialize(m, pbbody);
                length = (Int32)m.Length;
            }
            return length;
        }

        /// <summary>
        /// 获取消息Id
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Int16 GetPacketId(Type type)
        {
            if (type == null) {
                return -1;
            }

            string msgName = type.Name;
            return (Int16)(MessageMapping)Enum.Parse(typeof(MessageMapping), "Msg_" + msgName);
        }

        /// <summary>
        /// 生成消息包
        /// </summary>
        /// <param name="pbbody"></param>
        /// <returns></returns>
        public static PBPacket NewPacket(PBBody pbbody)
        {
            PBPacket packet = new PBPacket();
            PBHead header = new PBHead();
            header.mMessageId = GetPacketId(pbbody.GetType());
            header.mBodyLen = GetPBBodyLength(pbbody);
            header.mTimeStamp = DateTime.Now.Ticks;

            packet.mHead = header;
            packet.mBody = pbbody;

            return packet;
        }

        /// <summary>
        /// 将流转为包体
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static PBBody Buffer2PBBody(byte[] bytes, Type t)
        {
            using (MemoryStream m = new MemoryStream(bytes)) {
                return (PBBody)RuntimeTypeModel.Default.Deserialize(m, null, t);
            }
        }

        /// <summary>
        /// 将包体转为流
        /// </summary>
        /// <param name="pbbody"></param>
        /// <returns></returns>
        public static byte[] PBBody2Buffer(PBBody pbbody)
        {
            byte[] buffer = null;
            using (MemoryStream m = new MemoryStream()) {
                RuntimeTypeModel.Default.Serialize(m, pbbody);
                m.Position = 0;
                int length = (int)m.Length;
                buffer = new byte[length];
                m.Read(buffer, 0, length);
            }
            return buffer;
        }

        /// <summary>
        /// 将头转为流
        /// </summary>
        /// <param name="header"></param>
        /// <param name="isByteOrder"></param>
        /// <returns></returns>
        public static byte[] Head2Buffer(PBHead header, bool isByteOrder = true)
        {
            if (isByteOrder) {
                header = ConvertByteOrder(header);
            }
            int size = PBConst.PB_HEAD_SIZE;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try {
                Marshal.StructureToPtr(header, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            } finally {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 将流转包头
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static PBHead Buffer2Head(byte[] bytes)
        {
            int size = PBConst.PB_HEAD_SIZE;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try {
                Marshal.Copy(bytes, 0, buffer, size);
                PBHead header = (PBHead)Marshal.PtrToStructure(buffer, typeof(PBHead));
                // 接收到的byte需要转换字节序
                header.mBodyLen = header.mBodyLen;
                header.mMessageId = header.mMessageId;
                header.mTimeStamp = header.mTimeStamp;
                return header;
            } finally {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 将字节流转为消息包
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static PBPacket Buffer2Packet(PBPacket packet, byte[] bytes)
        {
            packet.mHead = Buffer2Head(bytes);

            // 获取包体字节流
            byte[] bodyBuf = new byte[packet.mHead.mBodyLen];
            Array.Copy(bytes, PBConst.PB_HEAD_SIZE, bodyBuf, 0, packet.mHead.mBodyLen);
            Type pbType = PBMessageType.GetMessageType((MessageMapping)packet.mHead.mMessageId);
            if (pbType == null) {
                // LogUtils.LogError("未知PBMessageType,请检查proto是否已更新!");
            }
            packet.mBody = Buffer2PBBody(bodyBuf, pbType);
            return packet;
        }

        /// <summary>
        /// 将消息包转为流
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] Packet2Buffer(PBPacket packet)
        {
            byte[] buf = new byte[PBConst.PB_HEAD_SIZE + packet.mHead.mBodyLen];

            byte[] headBuf = Head2Buffer(packet.mHead);
            byte[] bodyBuf = PBBody2Buffer(packet.mBody);

            Array.Copy(headBuf, 0, buf, 0, PBConst.PB_HEAD_SIZE);
            Array.Copy(bodyBuf, 0, buf, PBConst.PB_HEAD_SIZE, packet.mHead.mBodyLen);

            return buf;
        }

    }
}


