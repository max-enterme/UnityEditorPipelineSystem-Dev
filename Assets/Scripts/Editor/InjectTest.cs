using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Context1
{
    private int value1;
}

public class Context2 : Context1
{
    public int value1;
}

public class InjectTest
{
    [MenuItem("Test/Inejct Test")]
    public static void Dump()
    {
        var context2 = new Context2();

        typeof(Context1).GetField("value1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(context2, 100);
        Debug.Log(context2.value1);
    }
}
