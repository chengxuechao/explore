using GameCore;

/***
 * GameActivity.cs
 * 
 * @author abaojin
 */
namespace GameExplore
{
    /// <summary>
    /// Game的Activity
    /// </summary>
    public class GameActivity : SingletonMono<GameActivity>
    {
        private NetworkManager mNetwork = NetworkManager.Instance;
        private InputManager mInput = InputManager.Instance;

        void Start()
        {
            GameFacade.StartUp();

            if(mNetwork != null) {
                mNetwork.ConnectServer(ServerType.Game, "10.230.72.21:9999");
            }
        }

        void Update()
        {
            if(mNetwork != null) {
                mNetwork.Update();
            }
        }

        void FixedUpdate()
        {
            if(mInput != null) {
                mInput.FixedUpdate();
            }
        }

        void OnDestroy()
        {
            if(mNetwork != null) {
                mNetwork.Disconnect();
            }

            GameFacade.ClearUp();
        }

        void OnApplicationQuit()
        {
            
        }
    }
}
