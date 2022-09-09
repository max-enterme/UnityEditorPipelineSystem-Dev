using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEditorPipelineSystemDev.Editor.Tasks;
using UnityEngine;

public class TestBuildAppPipeline
{
    [MenuItem("Pipeline/TestBuildAppPipeline/RunAsync")]
    public static async void RunAsync()
    {
        var buildPlayerContext = new BuildPlayerContext
        {
            LocationPathName = "../Artifact/Artifact.exe",
        };

        var contextContainer = new ContextContainer();
        contextContainer.SetContext<IBuildPlayerContext>(buildPlayerContext);

        var tasks = new ITask[]
        {
            new BuildApplicationTask(),
        };

        using (var pipeline = new Pipeline(nameof(TestBuildAppPipeline), contextContainer, tasks))
        {
            pipeline.PipelineLoggerFactory = UnityPipelineLogger.GetDefaultPipelineLoggerFactory(pipeline);
            await pipeline.RunAsync();
        }


        if (Application.isBatchMode)
        {
            EditorApplication.Exit(0);
        }
    }
}