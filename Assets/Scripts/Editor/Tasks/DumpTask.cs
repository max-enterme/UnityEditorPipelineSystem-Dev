using System.Threading;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Contexts;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Tasks
{
    public class DumpTask : TaskBase
    {
        [InjectContext(ContextUsage.In)] private readonly IPipelineLogger logger = default;
        [InjectContext(ContextUsage.In, bindingField: nameof(test2ContextName))] private readonly Test2Context test2Context = default;

        [SerializeField] private string test2ContextName = default;

        public override ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
        {
            logger.LogAsync(JsonUtility.ToJson(test2Context));
            logger.LogAsync(test2Context.valueEnum.ToString());
            return TaskResult.Success;
        }
    }
}