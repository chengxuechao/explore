using System.Runtime.InteropServices;

/***
 * PBConst.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public static class PBConst
    {
        public static int PB_HEAD_SIZE = Marshal.SizeOf(typeof(PBHead));
    }
}
