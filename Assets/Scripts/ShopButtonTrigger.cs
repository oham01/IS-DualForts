using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonTrigger : MonoBehaviour
{
    public enum TurretType { Turret1, Turret2, Turret3, Unselect }
    public TurretType turretType;

    public float triggerHeight = 1.0f;

    // Check if the tracker is low enough when the player enter's the box collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float playerHeight = other.transform.position.y;

            TowerSpawner spawner = other.GetComponent<TowerSpawner>();

            if (playerHeight < triggerHeight)
            {
                Shop.Instance.SelectTurret(turretType,spawner);
            }
        }
    }

}
