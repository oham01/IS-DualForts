using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayDefeatSound();
        UIManager.Instance.ShowGameOver(GameStateManager.roundsSurvived);
    }
}
