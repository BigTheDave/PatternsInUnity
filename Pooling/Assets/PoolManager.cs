using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoolManager:MonoBehaviour
{
    public static GameObject Get(GameObject prefab)
    {
        return Instance._get(prefab);
    }
    public static void Make(GameObject prefab, int count)
    {
        Instance._make(prefab, count);
    }
    private static PoolManager mInstance;
    private static PoolManager Instance => Equals(mInstance, null) ? mInstance = CreateNew() : mInstance;

    private static PoolManager CreateNew()
    {
        var gameObject = new GameObject("PoolManager");
        return gameObject.AddComponent<PoolManager>();
    }

    public Dictionary<GameObject, List<GameObject>> Pools = new Dictionary<GameObject, List<GameObject>>();
    private void OnDisable()
    {
        mInstance = null;
    }
    private void _make(GameObject prefab,int count)
    {
        if (!Pools.ContainsKey(prefab)) Pools.Add(prefab, new List<GameObject>(count));
        for(int i = 0; i< count; i++)
        {
            var instance = Instantiate(prefab,this.transform);
            Pools[prefab].Add(instance);
            instance.gameObject.SetActive(false);
        }
    }
    private GameObject _get(GameObject prefab)
    {
        if (!Pools.ContainsKey(prefab))
        {
            _make(prefab, 10);
        }

        var pooledGameObject = Pools[prefab].FirstOrDefault(x => x.activeSelf == false);
        if (pooledGameObject != null)
        {
            return pooledGameObject;
        }

        _make(prefab, 10);
        return Pools[prefab].FirstOrDefault(x => x.activeSelf == false);

    }
}
