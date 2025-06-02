using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target; // Enemy to target
    private string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint; // Where the bullet comes out from 

    // Tower stats
    public float range = 15.0f;
    public float fireRate = 1.0f;
    private float fireCountdown = 0.0f;
    public static float globalFireRateMultiplier = 1f;

    public AudioClip shootSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Repeatedly search for the closest target every 0.5 seconds
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f); 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        if(fireCountdown <= 0.0f)
        {
            Shoot();
            fireCountdown = 1.0f / (fireRate * globalFireRateMultiplier);
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position,enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Shoot current target, instantiating a new bullet
    void Shoot()
    {
        GameObject new_bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = new_bullet.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.setTarget(target);
        }

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

}
