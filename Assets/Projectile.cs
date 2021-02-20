using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : BaseProjectile
{
    // Required components
    new Rigidbody rigidbody;

    // Parameters
    public float lifeTime;
    public float speed;

    // Internal
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void Setup(Collider emitter, Collider target)
    {
        var pos = transform.position;
        pos.y += 1;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        pos += direction * 1.5f;

        transform.position = pos;
        SetVelocity(direction * speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
        
        rigidbody.velocity = velocity;
    }

    public void SetVelocity(Vector3 value) { velocity = value; }

    void OnTriggerEnter(Collider collider)
    {
        var enemy = collider.gameObject.GetComponent<AutoFighter>();
        if (enemy)
        {
            enemy.TakeDamage();
            GameObject.Destroy(gameObject);
        }
    }
}
