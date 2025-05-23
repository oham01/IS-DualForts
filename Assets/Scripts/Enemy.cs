using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy stats
    public float speed = 15.0f;
    public int health = 100;
    public int diamondValue = 10;

    private Transform target; // The next waypoint to walk to
    private int waypointIndex = 0; // The index in the waypoint array to target

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.waypoints[waypointIndex];
    }

    // Move the enemy in the direction of the waypoint
    void Update()
    {
        Vector3 direction = target.position - transform.position ; // Direction to move in to get closer to target
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        // Change waypoint index if enemy reached the current target
        if(Vector3.Distance(transform.position,target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    // Increment waypoint index and destroy the enemy
    void GetNextWayPoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            GameStateManager.Instance.LoseLife();
            Destroy(gameObject);
            
        }
        else
        {
            waypointIndex++;
            target = Waypoints.waypoints[waypointIndex];
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameStateManager.Instance.KilledEnemy(diamondValue);

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);

        Destroy(gameObject);
    }

}
