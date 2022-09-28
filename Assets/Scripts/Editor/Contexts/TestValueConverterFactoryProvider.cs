using System.Reflection;
using UnityEditorPipelineSystem.Editor.Contexts.CommandLineArgumentConversion.ValueConverters;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Contexts
{
    [CreateAssetMenu(fileName = "TestValueConverterFactoryProvider", menuName = "Test/TestValueConverterFactoryProvider")]
    public class TestValueConverterFactoryProvider : ValueConverterFactoryProvider
    {
        public override IValueConverter GetValueConverter(FieldInfo info)
        {
            return default;
        }
    }
}