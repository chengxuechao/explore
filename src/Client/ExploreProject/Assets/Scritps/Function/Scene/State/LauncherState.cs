using GameCore;
using GameExplore;

namespace Game.Explore
{
    public class LauncherState : ISceneState
    {
        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override void OnStart()
        {
            SceneManager.SwitchScene("UI");
        }

        public override void OnDestroy()
        {

        }
    }
}

