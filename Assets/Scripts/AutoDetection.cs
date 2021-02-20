using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDetection : MonoBehaviour
{
    // Parameters
    public AutoFighter parent;

    // Internal
    E_TargetType targetType;
    List<Damageable> targets = new List<Damageable>();


    // Start is called before the first frame update
    void Start()
    {
        targetType = parent.GetTypeOfTarget();
    }

    // Update is called once per frame
    void Update()
    {
        targets.RemoveAll(item => item == null);
    }
    void OnTriggerEnter(Collider collider)
    {
        var fighter = collider.GetComponent<Damageable>();
        if (fighter && fighter.GetTType() == targetType)
            targets.Add(fighter);
    }

    void OnTriggerExit(Collider collider)
    {
        var fighter = collider.GetComponent<Damageable>();
        if (fighter)
            targets.Remove(fighter);
    }
    public Damageable GetTarget()
    {
        foreach(var target in targets) // Kill me pls
            {
                if (target.enabled)
                    return target;
            }
            return null;
    }

    public List<Damageable> GetAllTargets() { return targets; }

    public bool HasDetectedSomething()
    {
        targets.RemoveAll(item => item == null);
        foreach (var target in targets)
            if (target.enabled)
                return true;
        return false;
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
