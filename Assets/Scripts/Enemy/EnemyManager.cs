using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnTimes = 20f;
    public float spawnTimer = 20f;

    // Spawns the instantiated enemy. A period of 180 seconds is give to the player to complete the game, afterwhich zombies start to spawn rapidly.

    void Start()
    {

        StartSpawning();
        StartCoroutine(time());
    }
    
    void StartSpawning()
    {
        InvokeRepeating("SpawnNewEnemy", spawnTimes, spawnTimer);
    }

    void SpawnNewEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        
    }
    IEnumerator time()
    {
        yield return new WaitForSecondsRealtime(180);
        spawnTimer = 5f;
    }
}
