using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var playerToSpawn = SingletonComponent.Instance.GetComponent<GameState>().CurrentPlayerPrefab;
        Instantiate(playerToSpawn);
    }
}
