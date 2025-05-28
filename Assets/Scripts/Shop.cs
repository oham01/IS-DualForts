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
        switch (type)
        {

            case ShopButtonTrigger.TurretType.Turret1:
                selectedBlueprint = tower1Blueprint;
                //spawner.SelectTowerToBuild(tower1Blueprint);
               // Debug.Log("Turret 1 selected");
                break;
            case ShopButtonTrigger.TurretType.Turret2:
                selectedBlueprint = tower2Blueprint;
               // Debug.Log("Turret 2 selected");
               // spawner.SelectTowerToBuild(tower2Blueprint);
                break;
            case ShopButtonTrigger.TurretType.Turret3:
                selectedBlueprint = tower3Blueprint;
               // Debug.Log("Turret 3 selected");
                //spawner.SelectTowerToBuild(tower3Blueprint);
                break;
            case ShopButtonTrigger.TurretType.Unselect:
                Debug.Log("Unselected");
                spawner.DeselectTower();
                break;
        }

        if (GameStateManager.Instance.diamondCount >= selectedBlueprint.cost)
        {
            spawner.SelectTowerToBuild(selectedBlueprint);
            Debug.Log($"{type} selected");
        }
        else
        {
            Debug.Log("Not enough diamonds to select this turret.");

        }
    }


}
