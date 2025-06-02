using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayVictorySound();
    }
}
