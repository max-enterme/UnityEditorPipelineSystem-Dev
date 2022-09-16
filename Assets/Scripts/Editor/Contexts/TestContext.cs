using System;
using UnityEditorPipelineSystem.Core;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Contexts
{
    public class TestContext : IContext
    {
        [Serializable]
        public struct TestStruct
        {
            public bool valueBool;
            public byte valueByte;
            public sbyte valueSByte;
            public char valueChar;
            public decimal valueDecimal;
            public double valueDouble;
            public float valueFloat;
            public int valueInt;
            public uint valueUint;
            public long valueLong;
            public ulong valueUlong;
            public short valueShort;
            public ushort valueUShort;
        }

        [SerializeField] private TestStruct value;
    }
}