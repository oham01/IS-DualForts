using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public int startingLives = 100;
    public int currentLives;

    public int startingDiamonds = 0;
    public int diamondCount;

    public bool gameEnded;

    public static int roundsSurvived;

    public string gameScene = "GameScene";
    public string endScene = "EndScene";
    public string winScene = "WinScene";

    void Awake()
    {
        Instance = this;
        gameEnded = false;
    }

    // Initialize variables upon new game state
    void Start()
    {
        diamondCount = startingDiamonds;
        currentLives = startingLives;
        gameEnded = false;
        roundsSurvived = 0;
        UIManager.Instance.UpdateDiamondsCount(diamondCount);
        SoundManager.Instance.PlayBackgroundMusic();
        Tower.globalFireRateMultiplier = 1f; // Reset tower fire rate
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

    }

    // Increment diamonds
    public void GotDiamonds(int value)
    {
        diamondCount+= value;
        UIManager.Instance.UpdateDiamondsCount(diamondCount);
        SoundManager.Instance.PlayDiamondCollectSound();
    }

    // Lose game
    private void GameOver()
    {
        if(gameEnded) return;
        Debug.Log("GAME OVER");
        EndGame();
        SoundManager.Instance.StopBackgroundMusic();
    }

    // Take damage
    public void LoseLife(int amount)
    {
        currentLives -= amount;
        if(currentLives <= 0)
        {
            UIManager.Instance.UpdateLivesCount(0);
        }
        else
        {
            UIManager.Instance.UpdateLivesCount(currentLives);
        }
        
        if(currentLives <= 0)
        {
            GameOver();
        }
    }

    // Gain diamonds from enemies
    public void KilledEnemy(int value)
    {
        diamondCount += value;
        UIManager.Instance.UpdateDiamondsCount(diamondCount);
    }

    public void IncreaseRound()
    {
        roundsSurvived++;
    }

    // SCENE MANAGEMENT
    public void EndGame()
    {
        gameEnded = true;
        SceneManager.LoadScene(endScene);   
    }

    public void Retry()
    {
        WaveSpawner.enemiesAlive = 0;
        SceneManager.LoadScene(gameScene); // Reload the active scene
    }

    public void WinGame()
    {
        gameEnded = true;
        SceneManager.LoadScene(winScene);
    }
}
