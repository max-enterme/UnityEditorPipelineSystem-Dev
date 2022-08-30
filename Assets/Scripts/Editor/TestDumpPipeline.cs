using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorPipelineSystem;
using UnityEditorPipelineSystem.Injector;
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

    public class DumpTask : ISyncableTask
    {
        public ITaskResult Run(IContextContainer contextContainer)
        {
            var context = contextContainer.GetContext<IDumpContext>();
            Debug.Log(context.Message);
            Debug.Log(Thread.CurrentThread.ManagedThreadId);
            return TaskResult.Success;
        }
    }

    public class AsyncableDumpTask : IAsyncableTask
    {
        [InjectContext(ContextUsage.In, name: "test")] private readonly IDumpContext dumpContext;

        private string hoge = "test";


        public async Task<ITaskResult> RunAsync(IContextContainer contextContainer)
        {
            await Task.Delay(2000);
            Debug.Log(dumpContext.Message);
            Debug.Log(Thread.CurrentThread.ManagedThreadId);
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

        await Pipeline.RunAsync(contextContainer, tasks);
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
            new AsyncableDumpTask()
        };

        await Pipeline.RunAsync(contextContainer, tasks);
    }
}
