using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //public GameObject enemyPrefab;
    public Transform spawnPosition;

    public float timeBetweenWaves = 5.0f;
    public float timeBetweenSpawns = 0.25f;
    public float countdown = 2.0f;

    private int waveNumber = 0;
    public static int enemiesAlive = 0;

    public Text waveCountdownText;

    public Wave[] waves;

    // Spawn enemies every countdown 
    void Update()
    {
        // Don't spawn a new wave until all enemies from the previous wave are dead
        if(enemiesAlive > 0)
        {
            return;
        }

        if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown = countdown - Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity); // Clamp countdown value to be strictly positive

        waveCountdownText.text = countdown.ToString();
    }

    private IEnumerator SpawnWave()
    {
        GameStateManager.Instance.IncreaseRound();
        Wave wave = waves[waveNumber];

        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1.0f/wave.rate);
        }
        waveNumber++;

        if(waveNumber == waves.Length)
        {
            Debug.Log("GAME FINISHED");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
        enemiesAlive++;
    }
}
