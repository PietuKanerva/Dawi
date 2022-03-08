using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wave : MonoBehaviour
{
    // Unused wave script.

    public Text text;
    public GameObject enemyPrefab;
    int waveCount = 1;
    public int spawnedEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        waveCount = 1;
        Waves();
    }
    void Waves()
    {
        switch (waveCount)
        {
            case 1:
                Wave1();
                break;
            case 2:
                Wave2();
                break;
            case 3:
                Wave3();
                break;
        }
    }
    void Wave1()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemies();
        }
        StartCoroutine(WaitingTime());
    }
    void Wave2()
    {
        for (int i = 0; i < 5; i++)
        {
           SpawnEnemies();
        }
        StartCoroutine(WaitingTime());
    }
    void Wave3()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnEnemies();
        }
        StartCoroutine(WaitingTime());

    }
    void SpawnEnemies()
    {   
        Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0, 180, 0));       
    }
    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(30);
        waveCount++;
        Waves();
        
    }

}
