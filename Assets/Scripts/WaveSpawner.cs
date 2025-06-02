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
    public float countdown = 10.0f;

    private int waveNumber = 0;
    public static int enemiesAlive = 0;

    public Wave[] waves;

    void Awake()
    {
        enemiesAlive = 0; 
    }

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

        if (waveNumber == waves.Length)
        {
            Debug.Log("GAME FINISHED");
            this.enabled = false;
            GameStateManager.Instance.WinGame();
        }

        countdown = countdown - Time.deltaTime;
        UIManager.Instance.UpdateCountdown(countdown);
    }

    private IEnumerator SpawnWave()
    {
        // Increment wave number
        GameStateManager.Instance.IncreaseRound();
        waveNumber++;
        UIManager.Instance.UpdateWaveNumber(waveNumber);

        Wave wave = waves[waveNumber - 1];

        // Flatten all enemies into one list
        List<GameObject> spawnQueue = new List<GameObject>();
        foreach (WaveEntry entry in wave.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                spawnQueue.Add(entry.enemy);
            }
        }

        // Shuffle the list randomly
        for (int i = 0; i < spawnQueue.Count; i++)
        {
            int randomIndex = Random.Range(i, spawnQueue.Count);
            GameObject temp = spawnQueue[i];
            spawnQueue[i] = spawnQueue[randomIndex];
            spawnQueue[randomIndex] = temp;
        }

        // Spawn enemies in shuffled order
        foreach (GameObject enemy in spawnQueue)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(1.0f / wave.rate);
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
        enemiesAlive++;
    }
}
