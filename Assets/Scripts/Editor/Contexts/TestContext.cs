using System;
using UnityEditorPipelineSystem.Core;
using UnityEngine;

namespace UnityEditorPipelineSystemDev.Editor.Contexts
{
    [Serializable]
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

        [SerializeField] protected TestStruct value1;
        [SerializeField] public int value2;
        public int value3;
        [field: SerializeField] public int value4 { get; protected set; }
        [field: SerializeField] public int value5 { get; private set; }
    }
}