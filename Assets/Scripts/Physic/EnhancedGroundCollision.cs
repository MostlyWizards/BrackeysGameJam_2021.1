using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Collider))]
public class EnhancedGroundCollision : MonoBehaviour
{
    // Required components
    new Collider collider;
    bool isGrounded;


    // Internal
    float castLenght;
    RaycastHit hit;

    void Start() {
        collider = GetComponent<Collider>();
        castLenght = (collider.bounds.max.y - collider.bounds.min.y) / 2 + 0.0001f;
    }

    void FixedUpdate() {

		isGrounded = Physics.Raycast(collider.bounds.center, -collider.transform.up, out hit, castLenght);
    }

    public bool IsGrounded() { return isGrounded; }

    void OnDrawGizmos()
    {
        if (isGrounded)
            Gizmos.DrawLine(transform.position, hit.point);
    }
}