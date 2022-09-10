using System.Threading;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class DumpAsyncTask : IAsyncTask
    {
        [InjectContext(ContextUsage.In)] private readonly IPipelineLogger pipelineLogger;
        [InjectContext(ContextUsage.In, bindingField: "dumpContextName")] private readonly IDumpContext dumpContext;

        private string dumpContextName = default;

        public DumpAsyncTask()
        {
        }

        public DumpAsyncTask(string dumpContextName)
        {
            this.dumpContextName = dumpContextName;
        }

        public async Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct)
        {
            await Task.Delay(500);
            await pipelineLogger.LogAsync($"{Thread.CurrentThread.ManagedThreadId}:{dumpContext.Message}");
            return TaskResult.Success;
        }
    }
}