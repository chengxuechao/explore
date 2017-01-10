using UnityEngine;

namespace GameExplore
{
    public class SceneStateListener : MonoBehaviour
    {
        [SerializeField]
        private SceneStateType mStateType = SceneStateType.None;

        void Awake()
        {
            SceneManager.Init(mStateType);
            SceneManager.SceneState.OnAwake();
        }

        void Start()
        {
            SceneManager.SceneState.OnStart();
        }

        void Update()
        {
            SceneManager.SceneState.OnUpdate();
        }

        void Destroy()
        {
            SceneManager.SceneState.OnDestroy();
        }
    }
}

