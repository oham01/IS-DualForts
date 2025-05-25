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
        switch (type)
        {


            case ShopButtonTrigger.TurretType.Turret1:
                spawner.SelectTowerToBuild(tower1Blueprint);
                Debug.Log("Turret 1 selected");
                
                break;
            case ShopButtonTrigger.TurretType.Turret2:
                Debug.Log("Turret 2 selected");
                spawner.SelectTowerToBuild(tower2Blueprint);
                break;
            case ShopButtonTrigger.TurretType.Turret3:
                Debug.Log("Turret 3 selected");
                spawner.SelectTowerToBuild(tower3Blueprint);
                break;
            case ShopButtonTrigger.TurretType.Unselect:
                Debug.Log("Unselected");
                spawner.DeselectTower();
                break;
        }
    }
}
