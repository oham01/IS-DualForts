using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;
    private BuildZone currentBuildZone = null;

    public float spawnHeight = 1.0f;

    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.P) && currentBuildZone != null && !currentBuildZone.isUsed)
        {
            SpawnTowerAtZone();
        }*/

        if (currentBuildZone != null && !currentBuildZone.isUsed)
        {
            float playerY = transform.position.y;

            // Physical height trigger by tracker touching ground
            if (playerY <= spawnHeight)
            {
                SpawnTowerAtZone();
            }
            
            // Place tower with P to debug
            if (Input.GetKeyDown(KeyCode.P))
            {
                SpawnTowerAtZone();
            }
        }
    }

    void SpawnTowerAtZone()
    {
        Instantiate(towerPrefab, currentBuildZone.transform.position, towerPrefab.transform.rotation);
        currentBuildZone.isUsed = true; // Marcar la zona como usada
        Debug.Log("Torre colocada en zona: " + currentBuildZone.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildZone"))
        {
            currentBuildZone = other.GetComponent<BuildZone>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuildZone"))
        {
            if (currentBuildZone != null && other.GetComponent<BuildZone>() == currentBuildZone)
            {
                currentBuildZone = null;
            }
        }
    }
}
