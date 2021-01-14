using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float TimeBetweenSpawns = 0.4f;
    public GameObject Prefab;
    public void Spawn(int count)
    {
        StartCoroutine(SpawnBullets(count));
    }
    public IEnumerator SpawnBullets(int count) { 
        for(int i = 0; i < count; i++)
        {
            Spawn();
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }

    }
    public void Spawn()
    {
        float randomAngle = UnityEngine.Random.Range(0, 360);
        var pooledObject = PoolManager.Get(Prefab);
        pooledObject.transform.position = this.transform.position;
        pooledObject.transform.rotation = Quaternion.Euler(0, 0, randomAngle);
        pooledObject.SetActive(true);
    }
}
