using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiamondCollector : MonoBehaviour
{
    private bool isOccupied = false;
    private GameObject occupyingPlayer = null;
  
    public bool IsOccupied
    {
        get { return isOccupied; }
        private set { isOccupied = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOccupied)
        {
            isOccupied = true;
            occupyingPlayer = other.gameObject;
            Debug.Log($"{gameObject.name} occupied by {occupyingPlayer.name}");
            DiamondManager.Instance.CheckForCollection();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == occupyingPlayer)
        {
            isOccupied = false;
            occupyingPlayer = null;
            Debug.Log($"{gameObject.name} unoccupied by {other.gameObject.name}");
            DiamondManager.Instance.CheckForCollection();
        }
    }
}
