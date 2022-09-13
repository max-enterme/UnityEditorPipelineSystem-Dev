using System.Threading;
using UnityEditorPipelineSystem.Core;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public abstract class TaskBase : ISyncTask
    {
        public string Name { get; set; }

        public TaskBase()
        {
            Name = GetType().FullName;
        }

        public TaskBase(string name)
        {
            Name = name;
        }

        public abstract ITaskResult Run(IContextContainer contextContainer, CancellationToken ct);
    }
}