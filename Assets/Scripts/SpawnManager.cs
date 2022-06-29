using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Making script accessable within other scripts
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Making spawnpoints invisible
    public Transform[] spawnPoints;
    void Start()
    {
        foreach(Transform spawn in spawnPoints)
        {
            spawn.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    // Selecting a spawn point randomly
    public Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
