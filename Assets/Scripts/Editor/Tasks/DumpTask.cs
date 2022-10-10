using System.Threading;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class DumpTask : TaskBase
    {
        [InjectContext(ContextUsage.In, bindingField: nameof(test2ContextName))] private readonly Test2Context test2Context = default;

#pragma warning disable CS0414
        [SerializeField] private string test2ContextName = default;
#pragma warning restore CS0414

        public override ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
        {
            PipelineDebug.Log(JsonUtility.ToJson(test2Context));
            PipelineDebug.Log(test2Context.valueEnum.ToString());

            PipelineDebug.LogWarning("Warning");
            PipelineDebug.LogError("Error");
            PipelineDebug.LogException(new System.Exception("Exception"));

            return TaskResult.Success;
        }
    }
}