using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEditorPipelineSystemDev.Editor.Tasks;

public class TestDumpPipeline
{
    [MenuItem("Pipeline/TestDumpPipeline/Run")]
    public static async void Run()
    {
        var dumpContext = new DumpContext
        {
            Message = "Test",
        };

        var contextContainer = new ContextContainer();
        contextContainer.SetContext<IDumpContext>(dumpContext);

        var tasks = new ITask[]
        {
            new DumpTask()
        };

        await Utility.RunAsync(nameof(TestDumpPipeline), contextContainer, tasks);
    }

    [MenuItem("Pipeline/TestDumpPipeline/RunAsync")]
    public static async void RunAsync()
    {
        var dumpContext = new DumpContext
        {
            Message = "Test Async",
        };

        var contextContainer = new ContextContainer();
        contextContainer.SetContext<IDumpContext>(dumpContext, "test");

        var tasks = new ITask[]
        {
            DumpAsyncTask.CreateTask("test")
        };

        await Utility.RunAsync(nameof(TestDumpPipeline), contextContainer, tasks);
    }
}
