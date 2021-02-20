using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhausted : MonoBehaviour
{
    public Material material;
    new public Renderer renderer;
    AutoFighter fighter;

    bool isExhausted;
    Material oldMaterial;

    // Start is called before the first frame update
    void Start()
    {
        fighter = GetComponent<AutoFighter>();
        isExhausted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isExhausted && !fighter.enabled)
        {
            oldMaterial = renderer.sharedMaterial;
            renderer.sharedMaterial = material;
            isExhausted = true;
        }
        else if (isExhausted && fighter.enabled)
        {
            renderer.sharedMaterial = oldMaterial;
            isExhausted = false;
        }
    }
}
