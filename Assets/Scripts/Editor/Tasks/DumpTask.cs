using System.Threading;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class DumpTask : TaskBase
    {
        [InjectContext(ContextUsage.In, bindingField: nameof(test2ContextName))] private readonly Test2Context test2Context = default;

        [SerializeField] private string test2ContextName = default;

        public override ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
        {
            PipelineDebug.Log(JsonUtility.ToJson(test2Context));
            PipelineDebug.Log(test2Context.valueEnum.ToString());

            PipelineDebug.LogWarning("Warning");
            PipelineDebug.LogError("Error");
            PipelineDebug.LogException(new System.Exception("Exception"));


            object test = null;
            test.ToString();

            return TaskResult.Success;
        }
    }
}