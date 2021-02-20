using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAfterLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var collider = GetComponent<Collider>();
        if (Physics.Raycast(collider.bounds.center, -collider.transform.up, 1f))
        {
            gameObject.AddComponent<Collectable>();
            gameObject.GetComponent<AutoFighter>().enabled = true;
            Destroy(this);
        }
    }
}
