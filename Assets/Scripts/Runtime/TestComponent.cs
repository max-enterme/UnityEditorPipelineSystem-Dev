using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    [Serializable]
    public struct TestStruct
    {
        public string stringValue;
        public int intValue;
        public bool boolValue;
    }

    [SerializeField]
    private TestStruct value = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
