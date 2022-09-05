using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorPipelineSystem;
using UnityEngine;

public class TestOutputLogPipeline
{
    public class LoopTask : IAsyncTask
    {
        public async Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(2000);
                Debug.Log("Hoge");
            }

            return TaskResult.Success;
        }
    }

    [MenuItem("Pipeline/TestOutputLogPipeline/RunAsync")]
    public static async void RunAsync()
    {
        var cts = new CancellationTokenSource();

        void OnTaskStart(string taskName)
        {
            if (EditorUtility.DisplayCancelableProgressBar($"Processing {nameof(TestOutputLogPipeline)}", taskName, 0)
                && !cts.IsCancellationRequested)
            {
                Debug.Log("Requested Cancel");
                cts.Cancel();
            }
        }

        void OnTaskFinished(string taskName)
        {
        }

        var pipeline = new Pipeline(nameof(TestOutputLogPipeline), new ContextContainer(), new ITask[] { new LoopTask() });

        pipeline.OnStartTask += OnTaskStart;
        pipeline.OnFinishedTask += OnTaskFinished;

        try
        {
            await pipeline.RunAsync(cts.Token);

        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            pipeline.OnStartTask -= OnTaskStart;
            pipeline.OnFinishedTask -= OnTaskFinished;
        }

        EditorUtility.ClearProgressBar();
    }


}
