using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public int startingLives = 100;
    public int currentLives;

    public int startingDiamonds = 0;
    public int diamondCount;

    public bool gameEnded = false;
    public GameObject GameOverUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        diamondCount = startingDiamonds;
        currentLives = startingLives;
    }

    public void GotDiamonds()
    {
        diamondCount++;
        UIManager.Instance.UpdateDiamondsCount(diamondCount);
        SoundManager.Instance.PlayDiamondCollectSound();
    }

    private void GameOver()
    {
        if(gameEnded) return;
        Debug.Log("GAME OVER");
        EndGame();
    }

    public void LoseLife()
    {
        currentLives -= 50;
        if(currentLives < 0)
        {
            UIManager.Instance.UpdateLivesCount(0);
        }
        else
        {
            UIManager.Instance.UpdateLivesCount(currentLives);
        }
        

        if(currentLives < 0)
        {
            GameOver();
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        UIManager.Instance.ShowGameOver();
    }

    public void KilledEnemy(int value)
    {
        diamondCount += value;
        UIManager.Instance.UpdateDiamondsCount(diamondCount);
    }
}
