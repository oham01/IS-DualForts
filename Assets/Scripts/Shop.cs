using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    public TowerBlueprint tower1Blueprint;
    public TowerBlueprint tower2Blueprint;
    public TowerBlueprint tower3Blueprint;

    void Awake()
    {
        Instance = this;

    }

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
                // Suficiente dinero - permitir selección
                spawner.SelectTowerToBuild(selectedBlueprint, type);
                Debug.Log($"{type} selected - Ready to build!");
            }
            else
            {
                // No hay dinero suficiente - reproducir sonido de error y NO seleccionar
                Debug.Log($"Cannot select {type} - Not enough diamonds!");
                
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


}
