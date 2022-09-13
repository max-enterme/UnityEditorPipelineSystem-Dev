using System.Threading;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public abstract class AsyncTaskBase : IAsyncTask
    {
        public string Name { get; set; }

        public AsyncTaskBase()
        {
            Name = GetType().FullName;
        }

        public AsyncTaskBase(string name)
        {
            Name = name;
        }

        public abstract Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct);
    }
}