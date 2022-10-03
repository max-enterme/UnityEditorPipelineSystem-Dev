using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;

public class Utility
{
    public static async Task RunAsync(string name, IContextContainer contextContainer, IReadOnlyCollection<ITask> tasks)
    {
        using var logger = CreateLogger(name);
        var pipeline = PipelineUtility.InstantiatePipeline(name, contextContainer, tasks);
        await PipelineUtility.RunAsync(pipeline, logger);
    }

    private static ILogger CreateLogger(string name)
    {
        var directory = $"Library/pkg.max-enterme.unityeditor-pipeline-system/Logs/{name}";
        return new UnityEditorPipelineSystem.Editor.Logger($"{directory}/progress.log", $"{directory}/verbose.log", $"{directory}/warning.log", $"{directory}/error.log", $"{directory}/error.log");
    }
}
