using UnityEditorPipelineSystem.Core;

namespace UnityEditorPipelineSystemDev.Editor.Contexts
{
    public interface IBuildPlayerContext : IContext
    {
        public string LocationPathName { get; set; }
    }

    public class BuildPlayerContext : IBuildPlayerContext
    {
        public string LocationPathName { get; set; }
    }
}
