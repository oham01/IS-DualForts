using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 25.0f;
    public float explosionRadius = 0.0f; // For the aoe bullet type

    public int damage = 50;

    public GameObject impactEffect; // Particles spawned upon impact

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Compute the direction to the current target
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Hit target if the bullet would reach or pass the target this frame
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move bullet in the direction
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    // Choose new target
    public void setTarget(Transform new_target)
    {
        target = new_target;
    }

    // Deal damage to target, depending on bullet type
    void HitTarget()
    {
        // Create particle effects on contact
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2.0f);

        if(explosionRadius > 0.0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    // Deal damage to target
    void Damage(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
    }

    // Deal damage to multiple targets
    void Explode()
    {
        // Shoot out a sphere and check all colliders hit by the sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // Only take the enemy tag colliders into account
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

}
