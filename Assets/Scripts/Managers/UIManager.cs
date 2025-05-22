using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text diamondCountText;
    public Text livesCountText;

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
}
