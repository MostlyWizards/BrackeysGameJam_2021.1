using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvuDamage : MonoBehaviour
{
    public Material material;
    public float timer;

    float currentTimer;
    Material oldMaterial;
    new Renderer renderer;
    bool isRunning;

    public void RunEffect()
    {
        currentTimer = timer;
        renderer = GetComponentInChildren<Renderer>();
        oldMaterial = renderer.sharedMaterial;
        renderer.sharedMaterial = material;
        isRunning = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRunning)
        return;

        currentTimer -= Time.fixedDeltaTime;
        if (currentTimer <= 0)
        {
            renderer.sharedMaterial = oldMaterial;
            isRunning = false;
        } 
    }

    public bool IsRunning() { return isRunning; }
}
