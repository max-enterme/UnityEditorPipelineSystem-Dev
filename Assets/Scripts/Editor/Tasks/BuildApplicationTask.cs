using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class BuildApplicationTask : ISyncTask
    {
        [InjectContext(ContextUsage.In)]
        private readonly IBuildPlayerContext buildPlayerContext = default;

        public ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
        {
            BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray(),
                target = EditorUserBuildSettings.activeBuildTarget,
                targetGroup = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget),
                locationPathName = buildPlayerContext.LocationPathName,
                options = BuildOptions.Development,
            });

            return TaskResult.Success;
        }
    }
}