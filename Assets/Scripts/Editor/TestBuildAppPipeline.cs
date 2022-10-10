using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEditorPipelineSystemDev.Editor.Tasks;

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

        await Utility.RunAsync(nameof(TestBuildAppPipeline), contextContainer, tasks);
    }
}