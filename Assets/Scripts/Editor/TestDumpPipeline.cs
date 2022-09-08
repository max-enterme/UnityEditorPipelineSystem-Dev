using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEngine;

public class TestDumpPipeline
{
    public interface IDumpContext : IContext
    {
        public string Message { get; }
    }

    public class DumpContext : IDumpContext
    {
        public string Message { get; set; }
    }

    public class DumpTask : ISyncTask
    {
        public ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
        {
            var context = contextContainer.GetContext<IDumpContext>();
            Debug.Log(context.Message);
            Debug.Log(Thread.CurrentThread.ManagedThreadId);
            return TaskResult.Success;
        }
    }

    public class AsyncableDumpTask : IAsyncTask
    {
        [InjectContext(ContextUsage.In, optional: true)] private readonly IPipelineLogger pipelineLogger;

        [InjectContext(ContextUsage.In, bindingField: "dumpContextName")] private readonly IDumpContext dumpContext;

        private string dumpContextName = default;

        public AsyncableDumpTask(string dumpContextName)
        {
            this.dumpContextName = dumpContextName;
        }

        public async Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct)
        {
            await Task.Delay(500);
            //Debug.Log($"{Thread.CurrentThread.ManagedThreadId}:{dumpContext.Message}");
            await pipelineLogger?.LogAsync($"{Thread.CurrentThread.ManagedThreadId}:{dumpContext.Message}");
            return TaskResult.Success;
        }
    }

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

        await Pipeline.RunAsync(nameof(TestDumpPipeline), contextContainer, tasks);
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
            new AsyncableDumpTask("test")
        };

        await Pipeline.RunAsync(nameof(TestDumpPipeline), contextContainer, tasks);
    }
}
