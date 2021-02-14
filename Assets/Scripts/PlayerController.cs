using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EnhancedPhysicController))]
[RequireComponent(typeof(Walking))]
[RequireComponent(typeof(Jumping))]
public class PlayerController : MonoBehaviour
{
    // Required components
    EnhancedPhysicController physicController;
    Walking walking;
    Jumping jumping;

    // Parameters
    public InputActionAsset inputs;

    void Start()
    {
        physicController = GetComponent<EnhancedPhysicController>();
        walking = GetComponent<Walking>();
        jumping = GetComponent<Jumping>();

        var movementsInputs = inputs.FindActionMap("Movements");
        movementsInputs.Enable();
        movementsInputs["MoveForward"].performed += OnMoveForward;
        movementsInputs["MoveRight"].performed += OnMoveRight;
        movementsInputs["Jump"].performed += OnJump;
    }

    void OnMoveForward(InputAction.CallbackContext context)
    {
        walking.SetForwardIntensity(context.ReadValue<float>());
    }

    void OnMoveRight(InputAction.CallbackContext context)
    {
        walking.SetRightIntensity(context.ReadValue<float>());
    }

    void OnJump(InputAction.CallbackContext context)
    {
        jumping.Jump();
    }
}