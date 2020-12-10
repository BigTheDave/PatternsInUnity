using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoingTheSingletonThing : MonoBehaviour
{ 
    public void DoTheSingletoneThing()
    {
        SingletonExample.Instance.DoMyThing();         
    }
}
