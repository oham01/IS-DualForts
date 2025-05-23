using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text diamondCountText;
    public Text livesCountText;

    public GameObject gameOverUI;
    public Text roundsSurvived;

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

    public void ShowGameOver(int count)
    {
        roundsSurvived.text = count.ToString();
        gameOverUI.SetActive(true);
    }
}
