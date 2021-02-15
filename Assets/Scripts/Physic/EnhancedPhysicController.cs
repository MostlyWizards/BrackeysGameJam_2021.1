using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnhancedGroundCollision))]
public class EnhancedPhysicController : MonoBehaviour
{
    // Components required
    new Rigidbody rigidbody;
    EnhancedGroundCollision groundChecker;

    // Internal
    Vector3 horizontalMomentum;
    Vector3 verticalMomentum;
    Vector3 velocity;

    MomentumModifier[] verticalMomentumModifiers;
    MomentumModifier[] horizontalMomentumModifiers;
    MomentumModifier[] globalMomentumModifiers;
    VelocityModifier[] velocityModifiers;


    void Start()
    {
        //velocity = new Vector3(7,0,7);
        rigidbody = GetComponent<Rigidbody>();
        groundChecker = GetComponent<EnhancedGroundCollision>();

        SetupRequiredComponents();
        LoadModifiers();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < verticalMomentumModifiers.Length; ++i)
            verticalMomentumModifiers[i].Apply(ref verticalMomentum);
        for (int i = 0; i < horizontalMomentumModifiers.Length; ++i)
            horizontalMomentumModifiers[i].Apply(ref horizontalMomentum);

        Vector3 momentum = horizontalMomentum + verticalMomentum;
        for (int i = 0; i < globalMomentumModifiers.Length; ++i)
            globalMomentumModifiers[i].Apply(ref momentum);

        for (int i = 0; i < velocityModifiers.Length; ++i)
            velocityModifiers[i].Apply(ref velocity);

        rigidbody.velocity = velocity + momentum;
    }

    void SetupRequiredComponents()
    {
        rigidbody.freezeRotation = true;
    }

    void LoadModifiers()
    {
        var allMomentumModifiers = GetComponents<MomentumModifier>();
        var vertical = new List<MomentumModifier>();

        for (int i = 0; i < allMomentumModifiers.Length; ++i)
        {
            switch (allMomentumModifiers[i].GetMomentumAxis())
            {
                case E_MomentumAxis.Vertical:
                    vertical.Add(allMomentumModifiers[i]);
                break;
            }
        }
        verticalMomentumModifiers = vertical.ToArray();
        vertical.Clear();

        horizontalMomentumModifiers = new MomentumModifier[0];
        globalMomentumModifiers = new MomentumModifier[0];

        velocityModifiers = GetComponents<VelocityModifier>();
    }

    public bool IsGrounded() { return groundChecker.IsGrounded(); }
    public Vector3 GetVelocity() { return velocity; }
}