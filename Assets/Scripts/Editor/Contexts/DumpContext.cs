using System;
using UnityEditorPipelineSystem.Core;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Contexts
{
    public interface IDumpContext : IContext
    {
        public string Message { get; }
    }

    [Serializable]
    public class DumpContext : IDumpContext
    {
        [field: SerializeField]
        public string Message { get; set; }
    }
}