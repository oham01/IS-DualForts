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

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }
}
