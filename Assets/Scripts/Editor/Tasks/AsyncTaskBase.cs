using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public abstract class AsyncTaskBase : IAsyncTask
    {
        [SerializeField]
        protected int TimeoutMilliSeconds;

        public string Name { get; set; }
        public TimeSpan Timeout { get => TimeSpan.FromMilliseconds(TimeoutMilliSeconds); }

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