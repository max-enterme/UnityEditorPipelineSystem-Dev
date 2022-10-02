using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;
using UnityEditorPipelineSystemDev.Editor.Tasks;
using UnityEngine;

public class TestOutputLogPipeline
{
    public class LoopTask : AsyncTaskBase
    {
        public LoopTask() : base() { }

        public LoopTask(string name) : base(name) { }

        public async override Task<ITaskResult> RunAsync(IContextContainer contextContainer, CancellationToken ct)
        {
            int count = 10;

            while (!ct.IsCancellationRequested && count-- > 0)
            {
                await Task.Delay(2000);
                await PipelineDebug.LogAsync($"{count}:{ct.IsCancellationRequested}");

                ct.ThrowIfCancellationRequested();
            }

            return TaskResult.Success;
        }
    }

    private CancellationTokenSource cts = default;

    private TestOutputLogPipeline(CancellationTokenSource cts)
    {
        this.cts = cts;
    }

    private void Update()
    {
        if (EditorUtility.DisplayCancelableProgressBar("Task", "Hoge", 0) && !cts.Token.IsCancellationRequested)
        {
            Debug.Log("Cancel!!");
            cts.Cancel();
        }
    }

    [MenuItem("Pipeline/TestOutputLogPipeline/Run")]
    public static async void RunAsync()
    {
        using var cts = new CancellationTokenSource();

        using var logger = new UnityEditorPipelineSystem.Editor.Logger(
            "../logs/progress.log",
            "../logs/verbose.log");

        var contextContainer = new ContextContainer();

        var instance = new TestOutputLogPipeline(cts);

        var pipeline = new Pipeline(nameof(TestOutputLogPipeline), contextContainer, new ITask[] { new LoopTask() });

        EditorApplication.update += instance.Update;

        try
        {
            await pipeline.RunAsync(cts.Token);

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
        }

        EditorApplication.update -= instance.Update;

        EditorUtility.ClearProgressBar();
    }
}