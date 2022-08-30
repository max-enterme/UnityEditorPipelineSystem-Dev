using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorPipelineSystem;
using UnityEngine;

public class TestBuildAppPipeline
{
    public class BuildContext : IContext
    {
        public string LocationPathName { get; set; }
    }

    public class AsyncableBuildApplicationTask : IAsyncableTask
    {
        public async Task<ITaskResult> RunAsync(IContextContainer contextContainer)
        {
            var buildContext = contextContainer.GetContext<BuildContext>();

            await Task.Delay(1000);

            BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray(),
                target = EditorUserBuildSettings.activeBuildTarget,
                targetGroup = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget),
                locationPathName = buildContext.LocationPathName,
                options = BuildOptions.Development,
            });

            await Task.Delay(1000);

            return TaskResult.Success;
        }
    }

    public class AsyncableWriteScriptFileTask : IAsyncableTask
    {
        private const string filePath = "Assets/OutputFile.cs";

        public async Task<ITaskResult> RunAsync(IContextContainer contextContainer)
        {
            if (File.Exists(filePath))
            {
                AssetDatabase.DeleteAsset(filePath);
            }

            using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync($"public class Hoge {{ public string Value = \"{DateTime.Now}\"; }}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return TaskResult.Success;
        }
    }

    [MenuItem("Pipeline/TestBuildAppPipeline/RunAsync")]
    public static async void RunAsync()
    {
        var buildContext = new BuildContext
        {
            LocationPathName = "../Artifact/Artifact.exe",
        };

        var contextContainer = new ContextContainer();
        contextContainer.SetContext<BuildContext>(buildContext);

        var tasks = new ITask[]
        {
            new AsyncableWriteScriptFileTask(),
            new AsyncableBuildApplicationTask(),
        };

        await Pipeline.RunAsync(contextContainer, tasks);

        if (Application.isBatchMode)
        {
            EditorApplication.Exit(0);
        }
    }
}