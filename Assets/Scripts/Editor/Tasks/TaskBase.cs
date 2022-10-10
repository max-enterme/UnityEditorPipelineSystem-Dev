using System;
using System.Threading;
using UnityEditorPipelineSystem.Core;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public abstract class TaskBase : ISyncTask
    {
        [SerializeField]
        protected int TimeoutMilliSeconds;
        public TimeSpan Timeout { get => TimeSpan.FromMilliseconds(TimeoutMilliSeconds); }

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