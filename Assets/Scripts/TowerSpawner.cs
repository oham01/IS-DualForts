using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private BuildZone currentBuildZone = null;

    public TowerBlueprint towerToBuild = null; 
    
    public float spawnHeight = 1.0f;
    
    // Para saber qué botón está seleccionado actualmente
    private ShopButtonTrigger currentSelectedButton = null;
    
    // Referencia al PlayerMovement para obtener el número de jugador
    private PlayerMovement playerMovement;

    void Start()
    {
        // Obtener el componente PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Check if conditions met to spawn tower at a buildzone, if tower is selected and height is low enough
    void Update()
    {
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
            Debug.Log("No tower blueprint selected!");
            return;
        }

        // Check if player can afford the tower when placing
        if (GameStateManager.Instance.diamondCount < towerToBuild.cost)
        {
            Debug.LogWarning("Not enough diamonds to place tower - but selection stays!");
            return; // Don't place but keep selection
        }

        // Deduct the cost
        GameStateManager.Instance.GotDiamonds(-towerToBuild.cost);

        Instantiate(towerToBuild.TowerPrefab, currentBuildZone.transform.position, towerToBuild.TowerPrefab.transform.rotation);
        currentBuildZone.isUsed = true; // Marcar la zona como usada
        DeselectTower();
    }

    // On boxcollider enter, set the current buildzone location
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildZone"))
        {
            currentBuildZone = other.GetComponent<BuildZone>();
        }
    }

    // Unselect the buildzone location when exiting
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
    
    public void SelectTowerToBuild(TowerBlueprint tower, ShopButtonTrigger.TurretType turretType)
    {
        // Verificar que tenemos el número de jugador correcto
        int currentPlayerNumber = playerMovement != null ? playerMovement.playerNumber : 0;
        
        // Ocultar selección anterior de ESTE jugador específico
        if (currentSelectedButton != null)
        {
            currentSelectedButton.ShowSelection(false, currentPlayerNumber);
        }
        
        towerToBuild = tower;
        
        // Mostrar nueva selección con color del jugador
        ShopButtonTrigger[] buttons = FindObjectsOfType<ShopButtonTrigger>();
        foreach (ShopButtonTrigger button in buttons)
        {
            if (button.turretType == turretType)
            {
                button.ShowSelection(true, currentPlayerNumber); // Usar el playerNumber del PlayerMovement
                currentSelectedButton = button;
                break;
            }
        }
    }

    public void DeselectTower()
    {
        // Verificar que tenemos el número de jugador correcto
        int currentPlayerNumber = playerMovement != null ? playerMovement.playerNumber : 0;
        
        // Ocultar selección actual de ESTE jugador específico
        if (currentSelectedButton != null)
        {
            currentSelectedButton.ShowSelection(false, currentPlayerNumber);
            currentSelectedButton = null;
        }
        
        towerToBuild = null;
    }
}
