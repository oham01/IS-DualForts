using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text diamondCountText;
    public Text livesCountText;

    //public GameObject gameOverUI;
    public Text roundsSurvived;

    public Text waveCountdownText;
    public Text waveNumberText;

    public Text upgradeCost;

    void Awake()
    {
        Instance = this;

    }

    public void UpdateDiamondsCount(int count)
    {
        diamondCountText.text = count.ToString();
    }

    public void UpdateLivesCount(int count)
    {
        livesCountText.text = count.ToString();
    }
    
    public void UpdateWaveNumber(int number)
    {
        waveNumberText.text = "Wave: " + number.ToString();
    }

    public void UpdateCountdown(float countdown)
    {
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity); // Clamp countdown value to be strictly positive

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    public void ShowGameOver(int count)
    {
        roundsSurvived.text = count.ToString();
       //gameOverUI.SetActive(true);
    }

    public void UpdateUpgradeCost(int cost)
    {
        upgradeCost.text = cost.ToString();
    }

}
