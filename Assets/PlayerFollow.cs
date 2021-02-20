using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFollow : MonoBehaviour
{
    // Required components
    new Rigidbody rigidbody;

    // Parameters
    public AutoDetection detector;
    public float speed;

    // Internal
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (detector.HasDetectedSomething())
            return;
        
        var direction = player.position - transform.position;
        rigidbody.velocity = direction.normalized * speed * Time.fixedDeltaTime;
    }
}
