using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text diamondCountText;

    void Awake()
    {
        Instance = this;

    }

    public void UpdateDiamondsCount(int count) // 1
    {
        diamondCountText.text = count.ToString();
    }
}
