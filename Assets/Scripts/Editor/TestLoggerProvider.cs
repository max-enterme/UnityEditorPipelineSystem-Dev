using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystem.Editor;
using UnityEngine;

[CreateAssetMenu(menuName = "Test/TestLoggerProvider")]
public class TestLoggerProvider : LoggerProvider
{
    public override UnityEditorPipelineSystem.Core.ILogger CreateLogger(Pipeline pipeline)
    {
        var directory = $"Library/pkg.max-enterme.unityeditor-pipeline-system/Logs/{pipeline.Name}";
        return new UnityEditorPipelineSystem.Core.Logger($"{directory}/progress.log", $"{directory}/verbose.log", $"{directory}/warning.log", $"{directory}/error.log", $"{directory}/error.log");
    }
}
