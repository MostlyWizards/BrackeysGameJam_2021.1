using UnityEngine;
using Unity.Mathematics;

[RequireComponent(typeof(Collider))]
public class EnhancedGroundCollision : MonoBehaviour
{
    // Required components
    new Collider collider;
    bool isGrounded;

    void Start() {}
    void FixedUpdate() {}
}