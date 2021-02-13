using UnityEngine;

public enum E_MomentumAxis
{
    Global,
    Vertical,
    Horizontal
}

[RequireComponent(typeof(EnhancedPhysicController))]
public abstract class MomentumModifier : MonoBehaviour
{
    public abstract E_MomentumAxis GetMomentumAxis();
    public abstract void Apply(ref Vector3 momentum);
}