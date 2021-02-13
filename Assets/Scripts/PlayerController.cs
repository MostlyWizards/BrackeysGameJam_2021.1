using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EnhancedPhysicController))]
[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(Walking))]
public class PlayerController : MonoBehaviour
{
    // Required components
    EnhancedPhysicController physicController;
    Walking walking;

    // Parameters
    public InputActionAsset inputs;

    void Start()
    {
        physicController = GetComponent<EnhancedPhysicController>();
        walking = GetComponent<Walking>();

        var movementsInputs = inputs.FindActionMap("Movements");
        movementsInputs.Enable();
        movementsInputs["MoveForward"].performed += OnMoveForward;
        movementsInputs["MoveRight"].performed += OnMoveRight;
    }

    void OnMoveForward(InputAction.CallbackContext context)
    {
        walking.SetForwardIntensity(context.ReadValue<float>());
    }

    void OnMoveRight(InputAction.CallbackContext context)
    {
        walking.SetRightIntensity(context.ReadValue<float>());
    }
}