using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/***
 * BuildEditor.cs
 * 
 * @author abaojin
 */
namespace GameEditor
{
    public class BuildEditor : Editor
    {
        [MenuItem("Tool/Build Android")]
        private static void PerformAndroidBuild()
        {
            BulidTarget(BuildTarget.Android);
        }

        [MenuItem("Tool/Build iPhone")]
        private static void PerformiPhoneBuild()
        {
            BulidTarget(BuildTarget.iOS);
        }

        private static void BulidTarget(BuildTarget target)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(target);

            string appName = GetAppName();
            string targetDir = string.Empty;
            string targetName = string.Empty;
            BuildTargetGroup targetGroup = BuildTargetGroup.Unknown;
            BuildTarget buildTarget = BuildTarget.Android;
            string applicationPath = Application.dataPath.Replace("/Assets", "");

            if (target == BuildTarget.Android) {
                targetDir = applicationPath + "/Bin";
                targetName = appName + ".apk";
                targetGroup = BuildTargetGroup.Android;
            }
            if (target == BuildTarget.iOS) {
                targetDir = applicationPath + "/Bin";
                targetName = appName;
                targetGroup = BuildTargetGroup.iOS;
                buildTarget = BuildTarget.iOS;
            }

            if (Directory.Exists(targetDir)) {
                if (File.Exists(targetName)) {
                    File.Delete(targetName);
                }
            } else {
                Directory.CreateDirectory(targetDir);
            }

            string[] scenes = EditorUtils.GetEnabledScenes();
            GenericBuild(scenes, targetDir + "/" + targetName, buildTarget, BuildOptions.None);
        }

        private static void GenericBuild(string[] scenes, string targetDir, BuildTarget buildTarget, BuildOptions buildOption)
        {
            string result = BuildPipeline.BuildPlayer(scenes, targetDir, buildTarget, buildOption);
            if (result.Length > 0) {
                throw new Exception("BuildPlayer failure: " + result);
            }
        }

        private static string GetAppName()
        {
            string name = EditorUtils.GetPrjName();
            string version = EditorUtils.GetPrjVersion();
            return string.Format("{0}_{1}_{2}", name, version, DateTime.Now.ToString("yyyyMMddHHmm"));
        }
    }
}
