using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    [Serializable]
    public class A
    {
        [field: SerializeField] public int IntValue { get; set; }
        public string StringValue;
    }

    [Serializable]
    public class B : A
    {
    }

    [SerializeField]
    private B value = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
