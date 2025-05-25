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

    public RectTransform worldSpaceButton; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        diamondCount = startingDiamonds;
        currentLives = startingLives;
        gameEnded = false;
        roundsSurvived = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

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

    public void LoseLife(int amount)
    {
        currentLives -= amount;
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

    public void KilledEnemy(int value)
    {
        diamondCount += value;
        UIManager.Instance.UpdateDiamondsCount(diamondCount);
    }

    public void IncreaseRound()
    {
        roundsSurvived++;
    }


    public void EndGame()
    {
        gameEnded = true;
        UIManager.Instance.ShowGameOver(roundsSurvived);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the active scene
    }
}
