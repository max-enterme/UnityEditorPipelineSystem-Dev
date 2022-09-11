using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;
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

        var pipeline = new Pipeline(nameof(TestDumpPipeline), contextContainer, tasks);
        pipeline.PipelineLoggerFactory = UnityPipelineLogger.GetDefaultPipelineLoggerFactory(pipeline);
        await pipeline.RunAsync();
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

        var pipeline = new Pipeline(nameof(TestDumpPipeline), contextContainer, tasks);
        pipeline.PipelineLoggerFactory = UnityPipelineLogger.GetDefaultPipelineLoggerFactory(pipeline);
        await pipeline.RunAsync();
    }
}
