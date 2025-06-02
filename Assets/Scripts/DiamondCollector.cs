using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiamondCollector : MonoBehaviour
{
    private bool isOccupied = false; // Determine if player is standing ontop of diamond
    private GameObject occupyingPlayer = null; // Player object instance on the diamond
  
    // Getters and setters
    public bool IsOccupied
    {
        get { return isOccupied; }
        private set { isOccupied = value; }
    }

    // Occupy the diamond on box collider trigger with the player if it's not occupied
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOccupied)
        {
            isOccupied = true;
            occupyingPlayer = other.gameObject;
            DiamondManager.Instance.CheckForCollection();
        }
    }

    // Unoccupy the diamond on exit of the box collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == occupyingPlayer)
        {
            isOccupied = false;
            occupyingPlayer = null;
            DiamondManager.Instance.CheckForCollection();
        }
    }
}
