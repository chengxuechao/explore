using api;
using System;

/***
 * NetworkManager.cs
 * 
 * @author abaojin
 */
namespace GameCore
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private BaseNetwork mNetwork = null;
        private ServerType mServerType = ServerType.None;
        private Facade mFacade = Facade.Instance;

        public void Update()
        {
            if (mNetwork != null) {
                mNetwork.Update();
            }
        }

        public void ExecResult(PBPacket packet)
        {
            try {
                int id = packet.mHead.mMessageId;
                mFacade.SendNotification(((MessageMapping)id).ToString(), packet.mBody);
            } catch (Exception ex) {
                Log.Error("Error: " + ex.Message);
            }
        }

        public void Reconnnect()
        {
            if (mNetwork != null) {
                mNetwork.Reconnect();
            }
        }

        public void SendMessage(PBBody msg, string key = null)
        {
            if (mNetwork == null) {
                return;
            }
            if (mNetwork is HttpNetwork) {
                mNetwork.SendMessage(msg, key);
            }
            if (mNetwork is SocketNetwork) {
                mNetwork.SendMessage(msg);
            }
        }

        public void ConnectServer(ServerType type, string address)
        {
            mServerType = type;
            switch (type) {
                case ServerType.Login:
                    mNetwork = HttpNetwork.NewObject(address);
                    break;
                case ServerType.Game:
                    string[] svrIp = address.Split(':');
                    mNetwork = SocketNetwork.NewObject(svrIp[0], Convert.ToInt32(svrIp[1]));
                    break;
                default:
                    mServerType = ServerType.None;
                    break;
            }
        }

        public T Network<T>() where T : BaseNetwork
        {
            if (mNetwork == null) {
                return default(T);
            }
            return mNetwork as T;
        }

        public void Disconnect()
        {
            if (mNetwork != null) {
                mNetwork.Disconnect();
            }
            mNetwork = null;
        }
    }
}