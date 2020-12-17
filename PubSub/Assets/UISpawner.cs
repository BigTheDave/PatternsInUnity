using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour
{
    public GameObject[] Prefabs;
    private void Awake()
    {
        foreach(var prefab in Prefabs)
        {
            Instantiate(prefab, this.transform, true);
        }
    }
}
