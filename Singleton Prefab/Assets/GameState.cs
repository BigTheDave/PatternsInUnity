using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public int PlayerNumber = 0;
    public GameObject[] PlayerPrefabs;

    public GameObject CurrentPlayerPrefab => PlayerPrefabs[PlayerNumber];
}
