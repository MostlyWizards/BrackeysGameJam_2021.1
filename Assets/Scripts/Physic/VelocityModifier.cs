using UnityEngine;

[RequireComponent(typeof(EnhancedPhysicController))]
public abstract class VelocityModifier : MonoBehaviour
{
    public abstract void Apply(ref Vector3 velocity);
}