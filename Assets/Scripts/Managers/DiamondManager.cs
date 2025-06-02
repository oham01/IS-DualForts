using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    public static DiamondManager Instance;

    private DiamondCollector[] diamonds; // Array to hold the 2 diamonds

    public GameObject diamondPrefab; 

    public List<Transform> spawnPoints = new List<Transform>();

    private List<int> lastUsedIndices = new List<int>(); // Used to avoid respawning at same location

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(RespawnDiamonds());
    }


    public void CheckForCollection()
    {
        // Only collect diamonds if both are occupied
        if (diamonds.Length == 2 && diamonds[0].IsOccupied && diamonds[1].IsOccupied)
        {
            CollectDiamonds();
        }
    }

    // Add diamonds to the balance
    private void CollectDiamonds()
    {
        foreach (var diamond in diamonds)
        {
           Destroy(diamond.gameObject);
        }

        GameStateManager.Instance.GotDiamonds(50);

       // Respawn the diamonds again
       StartCoroutine(RespawnDiamonds());
    }

    // Respawn diamonds coroutine
    private IEnumerator RespawnDiamonds()
    {
        if (spawnPoints.Count < 2)
        {
            Debug.LogError("Not enough spawn points assigned for diamond respawn.");
            yield break;
        }

        List<int> usedIndices = new List<int>();
        diamonds = new DiamondCollector[2];

        // Randomly choose new diamond spawn points
        for (int i = 0; i < 2; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, spawnPoints.Count);
            } while (usedIndices.Contains(index) || lastUsedIndices.Contains(index));

            usedIndices.Add(index);

            GameObject newDiamond = Instantiate(diamondPrefab, spawnPoints[index].position, Quaternion.identity);
            diamonds[i] = newDiamond.GetComponent<DiamondCollector>();
        }

        // Update the record of last used spawn indices
        lastUsedIndices = usedIndices;
    }
}
