using UnityEngine;
using Cinemachine;
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
    public CinemachineFreeLook cameraController;

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
        movementsInputs["LookAt"].performed += OnLookAt;
        //movementsInputs["LookAt"].canceled += OnStopLookAt;
    }

    void Update()
    {

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

    void OnLookAt(InputAction.CallbackContext context)
    {
        //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
        Vector2 lookMovement = context.ReadValue<Vector2>().normalized;

        Debug.Log(cameraController.LookAt.forward);
        // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        //lookMovement.x = lookMovement.x * 180f; 

        //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        cameraController.m_XAxis.Value += lookMovement.x * 10 * Time.deltaTime;
        //cameraController.m_YAxis.Value += lookMovement.y * Time.deltaTime;

        walking.RotateY(Camera.main.transform.rotation.y);
    }
    void OnStopLookAt(InputAction.CallbackContext context)
    {
        //cameraController.m_XAxis.Value = 0;
        //cameraController.m_YAxis.Value = 0;
    }

    public Vector3 GetVelocity()
    {
        return physicController.GetVelocity();
    }

    public bool IsGrounded()
    {
        return physicController.IsGrounded();
    }
}