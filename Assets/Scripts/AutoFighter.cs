using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum E_TargetType
{
    Player,
    Ally,
    Enemy
}

[System.Serializable]
public enum E_DeathType
{
    Disable,
    Destroy
}

[RequireComponent(typeof(Collider))]
public class AutoFighter : Damageable
{
    // Required components
    new Collider collider;

    // Parameters
    public AutoDetection detector;
    public GameObject projectilePrefab;
    public float cooldown;
    public E_TargetType targetType;
    public E_DeathType deathType;

    // Internal
    float currentCooldown;
    InvuDamage damaged;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        damaged = GetComponent<InvuDamage>();
        NewLife();
    }

    void NewLife()
    {
        currentLife = initialLife;
    }

    public E_TargetType GetTypeOfTarget() { return targetType; }    

    // Update is called once per frame
    void FixedUpdate()
    {
        currentCooldown -= Time.fixedDeltaTime;
        if (currentCooldown <= 0)
        {
            var target = detector.GetTarget();
            if (target)
                Shoot(target);
        }
            
    }

    void Shoot(Damageable target)
    {
        currentCooldown = cooldown;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0; // grounded
        var projectile = GameObject.Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(direction, transform.up));
        projectile.GetComponent<BaseProjectile>().Setup(collider, target.GetComponent<Collider>());
    }

    public override void TakeDamage()
    {
        if (!damaged || (damaged && !damaged.IsRunning()))
        {
            --currentLife;
            if (currentLife <= 0)
                switch(deathType)
                {
                    case E_DeathType.Disable:
                    enabled = false;
                    return;
                    case E_DeathType.Destroy:
                    Destroy(gameObject);
                    return;
                }
        if (damaged)
            damaged.RunEffect();
        }
    }
}
