using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class DamageArea : BaseProjectile
{
    // Parameters
    public float scaleInitial;
    public float scaleFinal;
    public float rotationStep;
    public float speed;
    public float time;
    public AutoDetection detector;
    
    // Internal
    float step;
    Vector3 stepVector;
    bool movementsOver;

    // Start is called before the first frame update
    void Start()
    {
        step = speed * Time.fixedDeltaTime;
        stepVector = new Vector3(step,0,step);
        movementsOver = false;

        transform.localScale = new Vector3(scaleInitial, scaleInitial, scaleInitial);
        transform.rotation = quaternion.Euler(new Vector3(0, 0, 0));
    }

    public override void Setup(Collider emitter, Collider target)
    {
        var pos = target.transform.position;
        pos.y += 0.01f;
        transform.position = pos;
        detector = gameObject.AddComponent<AutoDetection>();
        detector.parent = emitter.GetComponent<AutoFighter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if (time <= 0)
            Destroy(gameObject);

        if (!movementsOver)
        {
            var scale = transform.localScale;
            if (scale.x < scaleFinal)
            {
                scale += stepVector;
                transform.localScale = scale;
            }
            else
                movementsOver = true;

            transform.Rotate(0, rotationStep, 0, Space.Self);
        }
        else
        {
            var targets = detector.GetAllTargets();
            foreach (var target in targets)
                target.TakeDamage();
        }
    }
}
