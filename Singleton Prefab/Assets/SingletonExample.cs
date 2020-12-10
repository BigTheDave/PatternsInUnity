using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SingletonExample
{
    private static readonly Lazy<SingletonExample> _instance = new Lazy<SingletonExample>(() => new SingletonExample());

    public static SingletonExample Instance => _instance.Value;
    private SingletonExample()
    {

    }

    public void DoMyThing()
    {
        Debug.Log("My Thing is Done");
    }
}
