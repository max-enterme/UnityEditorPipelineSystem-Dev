using System.Threading;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class DumpAsyncTask : AsyncTaskBase
    {
        public static DumpAsyncTask CreateTask(string dumpContextName)
        {
            return new DumpAsyncTask
            {
                dumpContextName = dumpContextName
            };
        }

        [InjectContext(ContextUsage.In, bindingField: "dumpContextName")] private readonly IDumpContext dumpContext;

#pragma warning disable CS0414
        [SerializeField] private string dumpContextName = default;
#pragma warning restore CS0414

        public DumpAsyncTask() : base() { }

        public DumpAsyncTask(string name) : base(name) { }

        public override async Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct)
        {
            await Task.Delay(500);
            await PipelineDebug.LogAsync($"{Thread.CurrentThread.ManagedThreadId}:{dumpContext.Message}");
            return TaskResult.Success;
        }
    }
}