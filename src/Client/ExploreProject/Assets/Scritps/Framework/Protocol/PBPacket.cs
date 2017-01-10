using api;

/***
 * PBPacket.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class PBPacket : IPBPacket
    {
        // 消息头
        public PBHead mHead { get; set; }

        // 消息体
        public PBBody mBody { get; set; }

        public byte[] Encoder()
        {
            return PBUtils.Packet2Buffer(this);
        }

        public void Decoder(byte[] bytes)
        {
            PBUtils.Buffer2Packet(this, bytes);
        }
    }

}

