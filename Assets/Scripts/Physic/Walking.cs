using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

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
            modelRoot.rotation = quaternion.LookRotation(velocity, new Vector3(0,1f,0));
            
            // Die mesh export :)
            var rot = modelRoot.rotation;
            var angles = rot.eulerAngles;
            angles.y += 90;
            rot.eulerAngles = angles;
            modelRoot.rotation = rot;
            
            oldDirection = direction;
        }
    }

    public void RotateY(float angle)
    {
        var rotation = transform.rotation;
        rotation.y = angle;
        transform.rotation = rotation;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}