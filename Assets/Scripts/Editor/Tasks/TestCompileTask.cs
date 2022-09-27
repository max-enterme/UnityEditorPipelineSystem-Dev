using System.IO;
using System.Threading;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEditorPipelineSystem.Core;
using UnityEditorPipelineSystemDev.Editor.Tasks;
using UnityEngine;

public class TestCompileTask : TaskBase
{
    [SerializeField] private string outputDirectory = "Library/pkg.max-enterme.unityeditor-pipeline-system";

    public override ITaskResult Run(IContextContainer contextContainer, CancellationToken ct)
    {
        var input = new ScriptCompilationSettings
        {
            target = EditorUserBuildSettings.activeBuildTarget,
            group = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget),
        };

        Directory.CreateDirectory(outputDirectory);

        var result = PlayerBuildInterface.CompilePlayerScripts(input, outputDirectory);
        var assemblies = result.assemblies;

        //var isSuccess =
        //    assemblies != null &&
        //    assemblies.Count != 0 &&
        //    result.typeDB != null;

        if (Directory.Exists(outputDirectory))
        {
            Directory.Delete(outputDirectory, true);
        }

        return TaskResult.Success;
    }
}
