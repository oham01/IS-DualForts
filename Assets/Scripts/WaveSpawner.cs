using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPosition;

    public float timeBetweenWaves = 10.0f;
    public float timeBetweenSpawns = 0.25f;
    public float countdown = 2.0f;

    private int waveNumber = 0;

    public Text waveCountdownText;

    // Spawn enemies every countdown 
    void Update()
    {
        if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown = countdown - Time.deltaTime;

        waveCountdownText.text = countdown.ToString();
    }

    private IEnumerator SpawnWave()
    {
        waveNumber++;
        GameStateManager.Instance.IncreaseRound();
        for(int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab,spawnPosition.position,enemyPrefab.transform.rotation);
    }
}
