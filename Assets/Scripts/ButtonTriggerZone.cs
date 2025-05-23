using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerZone : MonoBehaviour
{
    public float triggerHeight = 1.0f;

    // Check if the tracker is low enough when the player enter's the box collider
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            float playerHeight = other.transform.position.y;

            if (playerHeight < triggerHeight)
            { 
                GameStateManager.Instance.Retry();
            }
        }
    }
}
