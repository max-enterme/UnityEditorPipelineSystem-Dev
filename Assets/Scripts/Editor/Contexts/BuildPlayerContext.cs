using UnityEditorPipelineSystem.Core;

namespace UnityEditorPipelineSystem.Editor.Contexts
{
    public interface IBuildPlayerContext : IContext
    {
        public string LocationPathName { get; set; }
    }

    public class BuildPlayerContext : IBuildPlayerContext
    {
        public string LocationPathName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
