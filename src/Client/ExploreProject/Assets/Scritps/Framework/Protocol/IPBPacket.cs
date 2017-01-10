/***
 * IPBPacket.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public interface IPBPacket
    {
        byte[] Encoder();

        void Decoder(byte[] bytes);
    }
}
