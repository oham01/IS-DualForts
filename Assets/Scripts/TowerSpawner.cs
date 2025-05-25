using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private BuildZone currentBuildZone = null;

    public TowerBlueprint towerToBuild = null;
    
    public float spawnHeight = 1.0f;

    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.P) && currentBuildZone != null && !currentBuildZone.isUsed)
        {
            SpawnTowerAtZone();
        }*/

        if (currentBuildZone != null && !currentBuildZone.isUsed && towerToBuild != null)
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
        if (towerToBuild == null || towerToBuild.TowerPrefab == null)
        {
            Debug.LogWarning("No tower blueprint selected!");
            return;
        }

        Instantiate(towerToBuild.TowerPrefab, currentBuildZone.transform.position, towerToBuild.TowerPrefab.transform.rotation);
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

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
    }

    public void DeselectTower()
    {
        towerToBuild = null;
    }
}
