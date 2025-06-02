using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    public int upgradeCost = 25;
    public TowerBlueprint tower1Blueprint;
    public TowerBlueprint tower2Blueprint;
    public TowerBlueprint tower3Blueprint;

    public AudioClip selectionSound;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        UIManager.Instance.UpdateUpgradeCost(upgradeCost);
    }

    // Select the turret blueprint (or unselect/upgrade) when the player enters the box collider
    public void SelectTurret(ShopButtonTrigger.TurretType type,TowerSpawner spawner)
    {
        TowerBlueprint selectedBlueprint = null;
        
        // Handle unselect case first
        if (type == ShopButtonTrigger.TurretType.Unselect)
        {
            Debug.Log("Unselected");
            spawner.DeselectTower();
            return;
        }

        // Handle Upgrade, Apply a boost to all turrets
        if (type == ShopButtonTrigger.TurretType.Upgrade)
        {
            // Check if the player has enough diamonds to apply the upgrade
            if (GameStateManager.Instance.diamondCount >= upgradeCost)
            {
                // Apply the boost to all turrets
                ApplyUpgradeToAllTurrets();

                // Deduct diamonds
                GameStateManager.Instance.diamondCount -= upgradeCost;

                // Play selection sound
                if (selectionSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(selectionSound);
                }
            }
            else
            {
                // Not enough money for upgrade, play error sound
                ShopButtonTrigger[] buttons = FindObjectsOfType<ShopButtonTrigger>();
                foreach (ShopButtonTrigger button in buttons)
                {
                    if (button.turretType == ShopButtonTrigger.TurretType.Upgrade)
                    {
                        button.PlayErrorSound();
                        break;
                    }
                }

            }
        }

            // Get the blueprint for the selected turret
            switch (type)
            {
            case ShopButtonTrigger.TurretType.Turret1:
                selectedBlueprint = tower1Blueprint;
                break;
            case ShopButtonTrigger.TurretType.Turret2:
                selectedBlueprint = tower2Blueprint;
                break;
            case ShopButtonTrigger.TurretType.Turret3:
                selectedBlueprint = tower3Blueprint;
                break;
            }

        if (selectedBlueprint != null)
        {
            // Verificar si hay dinero suficiente ANTES de seleccionar
            if (GameStateManager.Instance.diamondCount >= selectedBlueprint.cost)
            {
                // Suficiente dinero, permitir selección
                spawner.SelectTowerToBuild(selectedBlueprint, type);
            }
            else
            {
                
                // Encontrar el botón que se intentó presionar y reproducir sonido de error
                ShopButtonTrigger[] buttons = FindObjectsOfType<ShopButtonTrigger>();
                foreach (ShopButtonTrigger button in buttons)
                {
                    if (button.turretType == type)
                    {
                        button.PlayErrorSound();
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("No blueprint found for this turret type.");
        }
    }

    // Change the global fire rate modifier to boost all turrets
    public void ApplyUpgradeToAllTurrets()
    { 
        Tower.globalFireRateMultiplier *= 1.125f;
        upgradeCost = (int)(upgradeCost * 1.25f);
        UIManager.Instance.UpdateUpgradeCost(upgradeCost);
    }

}
