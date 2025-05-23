using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 25.0f;
    public float explosionRadius = 0.0f;

    public int damage = 50;

    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

    }

    public void setTarget(Transform new_target)
    {
        target = new_target;
    }

    void HitTarget()
    {
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
        Debug.Log("BULLET HIT");
    }

    void Damage(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
    }

    void Explode()
    {
        //
    }
}
