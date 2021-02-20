using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Parameters
    public int life;
    public float cooldown;

    // Internal
    List<GameObject> targets = new List<GameObject>();
    float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = cooldown;
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

    public void TakeDamage()
    {
        --life;
        if (life <= 0)
            Destroy(gameObject);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Warrior>())
            targets.Add(collider.gameObject);
    }

    void OnTriggerExit(Collider collider)
    {
        targets.Remove(collider.gameObject);
    }

    void Shoot(GameObject target)
    {
        target.GetComponent<Warrior>().TakeDamage();
        currentCooldown = cooldown;
    }
}
