using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;
    private BuildZone currentBuildZone = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && currentBuildZone != null && !currentBuildZone.isUsed)
        {
            SpawnTowerAtZone();
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
