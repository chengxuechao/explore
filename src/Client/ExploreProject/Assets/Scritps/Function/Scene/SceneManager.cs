using Game.Explore;
using GameCore;
using UnityEngine;

/***
 * SceneManager.cs
 * 
 * @author abaojin
 */ 
namespace GameExplore
{
    public class SceneManager : Singleton<SceneManager>
    {
        private static ISceneState mSceneState;

        private static string mTargetName;

        /// <summary>
        /// 初始化场景管理器
        /// </summary>
        /// <param name="type"></param>
        public static void Init(SceneStateType type)
        {
            if(type == SceneStateType.None) {
                return;
            }
            switch (type) {
                case SceneStateType.Launcher:
                    mSceneState = new LauncherState();
                    break;

                case SceneStateType.Loading:
                    mSceneState = new LoadingState();
                    break;

                case SceneStateType.Combat:
                    mSceneState = new CombatState();
                    break;

                case SceneStateType.MainCity:
                    mSceneState = new MainCityState();
                    break;

                default:
                    break;
            }
        }

        public static ISceneState SceneState 
        {
            get {
                return mSceneState;
            }
        }

        public static string TargetName 
        {
            get {
                return mTargetName;
            }
        }

        public static void SwitchScene(string name)
        {
            mTargetName = name;
            Application.LoadLevel("Loading");
        }
    }
}