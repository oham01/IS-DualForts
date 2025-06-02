using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy stats
    public float speed = 15.0f;
    public int health = 100;
    public int diamondValue = 10;
    public int damage = 60;

    private AudioSource audioSource;

    private Transform target; // The next waypoint to walk to
    private int waypointIndex = 0; // The index in the waypoint array to target

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        target = Waypoints.waypoints[waypointIndex];
    }

    // Move the enemy in the direction of the waypoint
    void Update()
    {
        Vector3 direction = target.position - transform.position ; // Direction to move in to get closer to target
        
        // Move in world space instead of local space
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Rotate the enemy to face the direction of movement
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }

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
            GameStateManager.Instance.LoseLife(damage);
            WaveSpawner.enemiesAlive--;
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

        WaveSpawner.enemiesAlive--;

        Destroy(gameObject);
    }

}
