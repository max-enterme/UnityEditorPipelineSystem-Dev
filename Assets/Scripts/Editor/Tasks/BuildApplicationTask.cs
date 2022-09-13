using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class BuildApplicationTask : TaskBase
    {
        [InjectContext(ContextUsage.In)]
        private readonly IBuildPlayerContext buildPlayerContext = default;

        public BuildApplicationTask() : base() { }

        public BuildApplicationTask(string name) : base(name) { }

        public override ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
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