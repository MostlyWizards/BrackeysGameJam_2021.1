using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Warrior : MonoBehaviour
{
    // Required components
    new Collider collider;

    // Parameters
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float cooldown;
    public int life;

    // Internal
    List<GameObject> targets = new List<GameObject>();
    Vector3 projectileOffset;
    float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        projectileOffset = new Vector3(0, collider.bounds.center.y, 0);    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentCooldown -= Time.fixedDeltaTime;
        targets.RemoveAll(item => item == null);
        if (currentCooldown <= 0)
            foreach(var target in targets) // Kill me pls
            {
                Shoot(target);
                break; //First one only
            }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Enemy>())
            targets.Add(collider.gameObject);
    }

    void OnTriggerExit(Collider collider)
    {
        targets.Remove(collider.gameObject);
    }

    void Shoot(GameObject target)
    {
        currentCooldown = cooldown;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        var projectile = GameObject.Instantiate(projectilePrefab, transform.position + projectileOffset + direction * 1.5f, Quaternion.LookRotation(direction, transform.up));
        projectile.GetComponent<Projectile>().SetVelocity(direction * projectileSpeed);
    }
    public void TakeDamage()
    {
        --life;
        if (life <= 0)
            enabled = false;
    }

    void OnDrawGizmos()
    {
        targets.RemoveAll(item => item == null);
        foreach(var target in targets)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
