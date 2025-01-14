using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
#if UNITY_EDITOR_OSX
#if UNITY_2022_2_OR_NEWER
using UnityEditor.Build;
#endif
using UnityEditor.OSXStandalone;
#endif

namespace UniTools.Build
{
    [CreateAssetMenu(
        fileName = nameof(BuildMacOS),
        menuName = MenuPaths.Standalone + nameof(BuildMacOS)
    )]
    public sealed class BuildMacOS : UnityBuildStepWithOptions
    {
#if UNITY_EDITOR_OSX

#if UNITY_2022_2_OR_NEWER
        [SerializeField] private OSArchitecture m_architecture = default;
#else
        [SerializeField] private MacOSArchitecture m_architecture = default;
#endif
        [SerializeField] private bool m_createXcodeProject = false;
#endif
        public override BuildTarget Target => BuildTarget.StandaloneOSX;

        public override async Task Execute()
        {
#if UNITY_EDITOR_OSX
            UserBuildSettings.architecture = m_architecture;
            UserBuildSettings.createXcodeProject = m_createXcodeProject;
#endif
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(Options);

            await Task.CompletedTask;

            BuildSummary summary = report.summary;
            if (summary.result == BuildResult.Failed)
            {
                throw new Exception($"{nameof(BuildPipeline)}: {name} Build failed!");
            }
        }
    }
}