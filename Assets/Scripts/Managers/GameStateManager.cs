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
        //UIManager.Instance.ShowGameOverWindow(); // doesn't exist yet
        EndGame();
    }

    public void LoseLife()
    {
        currentLives -= 50;
        UIManager.Instance.UpdateLivesCount(currentLives);

        if(currentLives < 0)
        {
            GameOver();
        }
    }

    public void EndGame()
    {
        gameEnded = true;
    }
}
