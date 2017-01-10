using api;
using GameCore;

/***
 * BagController.cs
 * 
 * @author abaojin
 */ 
namespace GameExplore
{
    [InjectController]
    public class BagController
    {
        [InjectAction(MessageMapping.Msg_CreatePlayerGC, typeof(CreatePlayerGC))]
        public static void OnGetBagInfo(CreatePlayerGC msg)
        {
            Log.Error(msg.roleId.ToString());
        }

        [InjectAction(MessageMapping.Msg_LoginGC, typeof(LoginGC))]
        public static void OnDelBagInfo(LoginGC msg)
        {
            Log.Error(msg.id.ToString());
        }
    }
}