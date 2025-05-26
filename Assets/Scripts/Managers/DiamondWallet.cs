using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondWallet : MonoBehaviour
{
    public static DiamondWallet Instance;

    private int diamonds = 0;

    public int Diamonds => diamonds; // read-only access

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        Debug.Log("Diamonds increased. Total: " + diamonds);
    }

    public bool SpendDiamonds(int amount)
    {
        if (diamonds >= amount)
        {
            diamonds -= amount;
            Debug.Log("Spent " + amount + " diamonds. Remaining: " + diamonds);
            return true;
        }
        else
        {
            Debug.Log("Not enough diamonds to spend.");
            return false;
        }
    }
}