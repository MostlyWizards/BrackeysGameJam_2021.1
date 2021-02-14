using UnityEngine;
using Unity.Mathematics;

public class Walking : VelocityModifier
{
    // Parameters
    public float speed;

    public Transform modelRoot;

    // Internal
    Vector2 oldDirection;
    Vector2 direction;

    public void SetForwardIntensity(float intensity) { direction.x = intensity; direction.Normalize(); }
    public void SetRightIntensity(float intensity) { direction.y = intensity; direction.Normalize(); }
    public override void Apply(ref Vector3 velocity) {

        var forward = transform.forward;
        var right = transform.right;

        velocity.x = ((forward.x * direction.x) + (right.x * direction.y)) * speed * Time.fixedDeltaTime;
        velocity.z = ((forward.z * direction.x) + (right.z * direction.y)) * speed * Time.fixedDeltaTime;

        if (direction.magnitude >= 0.1f && oldDirection != direction)
        {
            modelRoot.rotation = quaternion.LookRotation(velocity, transform.up);
            oldDirection = direction;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}