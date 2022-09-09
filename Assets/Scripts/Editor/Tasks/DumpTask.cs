using System.Threading;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class DumpTask : ISyncTask
    {
        [InjectContext(ContextUsage.In)] private readonly IPipelineLogger logger = default;
        [InjectContext(ContextUsage.In)] private readonly IDumpContext dumpContext = default;

        public ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
        {
            logger.LogAsync($"{Thread.CurrentThread.ManagedThreadId}:{dumpContext.Message}");
            return TaskResult.Success;
        }
    }
}