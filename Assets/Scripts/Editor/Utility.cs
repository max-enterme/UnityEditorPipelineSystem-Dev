using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;

public class Utility
{
    public static async Task RunAsync(string name, IContextContainer contextContainer, IReadOnlyCollection<ITask> tasks)
    {
        try
        {
            using var logger = CreateLogger(name);
            PipelineDebug.Logger = logger;
            using var pipeline = new Pipeline(name, contextContainer, tasks);
            await pipeline.RunAsync();
        }
        catch
        {
            throw;
        }
        finally
        {
            PipelineDebug.Logger = default;
        }
    }

    private static ILogger CreateLogger(string name)
    {
        var directory = $"Library/pkg.max-enterme.unityeditor-pipeline-system/Logs/{name}";
        return new UnityEditorPipelineSystem.Editor.Logger($"{directory}/progress.log", $"{directory}/verbose.log", $"{directory}/warning.log", $"{directory}/error.log", $"{directory}/error.log");
    }
}
