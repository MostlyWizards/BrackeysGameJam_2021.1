using UnityEngine;

[RequireComponent(typeof(EnhancedPhysicController))]
public class Gravity : MomentumModifier
{
    //Required components
    EnhancedPhysicController controller;

    public float gravity;

    public override E_MomentumAxis GetMomentumAxis() { return E_MomentumAxis.Vertical; }
    public override void Apply(ref Vector3 momentum)
    {
        momentum -= controller.transform.up * gravity * Time.fixedDeltaTime;
    }

    void Start()
    {
        controller = GetComponent<EnhancedPhysicController>();
    }
}