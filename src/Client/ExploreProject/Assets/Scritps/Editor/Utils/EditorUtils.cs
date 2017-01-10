using System.Collections.Generic;
using UnityEditor;

/***
 * EditorUtils.cs
 * 
 * @anthor abaojin
 */
namespace GameEditor
{
    public static class EditorUtils
    {
        public static string GetPrjName()
        {
            return PlayerSettings.productName;
        }

        public static string GetPrjVersion()
        {
            string bundleVersion = PlayerSettings.bundleVersion;
            string appVersion = null;

#if UNITY_ANDROID
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
            appVersion = PlayerSettings.Android.bundleVersionCode.ToString();
        }
#elif UNITY_IPHONE
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
                appVersion = PlayerSettings.iOS.buildNumber.ToString();
            } 
#else
            appVersion = EditorUserBuildSettings.activeBuildTarget.ToString();
#endif

            return string.Format("{0}.{1}", appVersion, bundleVersion);
        }

        public static string[] GetEnabledScenes()
        {
            List<string> list = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (!scene.enabled) {
                    continue;
                }
                list.Add(scene.path);
            }

            return list.ToArray();
        }

    }

}
