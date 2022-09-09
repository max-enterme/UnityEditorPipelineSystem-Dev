using UnityEditorPipelineSystem.Core;

namespace UnityEditorPipelineSystemDev.Editor.Contexts
{
    public interface IDumpContext : IContext
    {
        public string Message { get; }
    }

    public class DumpContext : IDumpContext
    {
        public string Message { get; set; }
    }
}