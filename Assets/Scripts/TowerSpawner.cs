using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Puedes personalizar esta tecla
        {
            SpawnTower();
        }
    }

    void SpawnTower()
    {
        if (towerPrefab != null)
        {
            Vector3 position = transform.position;
            Quaternion rotation = towerPrefab.transform.rotation; // Usa la rotación del prefab sin forzarla

            Instantiate(towerPrefab, position, rotation);
        }
        else
        {
            Debug.LogWarning("Tower prefab no asignado en " + gameObject.name);
        }
    }
}
