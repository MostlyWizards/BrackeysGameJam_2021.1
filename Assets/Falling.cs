using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public float yLimit;
    public float speed;
    public GameObject[] requiredToDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < requiredToDie.Length; ++i)
            if (requiredToDie[i] != null)
                return;
        GetComponent<Collider>().enabled = false;       
        var pos = transform.position;
        pos.y -= speed * Time.fixedDeltaTime;
        transform.position = pos;

        if (pos.y <= yLimit)
            Destroy(gameObject);
    }
}
