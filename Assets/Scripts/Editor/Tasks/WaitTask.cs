using System.Threading;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Tasks;
using UnityEngine;

public class WaitTask : AsyncTaskBase
{
    [SerializeField] private int waitMillSeconds = 0;

    public override async Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct)
    {
        await Task.Delay(waitMillSeconds, ct);
        return TaskResult.Success;
    }
}
