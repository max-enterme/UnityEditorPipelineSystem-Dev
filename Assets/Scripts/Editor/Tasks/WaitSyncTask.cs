using System.Threading;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Tasks;
using UnityEngine;

public class WaitSyncTask : TaskBase
{
    [SerializeField] private int milliSeconds = 0;

    public override ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
    {
        var start = Time.time;
        while ((Time.time - start) * 1000 < milliSeconds)
        {
            PipelineDebug.Log(Time.time.ToString());
            ct.ThrowIfCancellationRequested();
        }

        return TaskResult.Success;
    }
}
