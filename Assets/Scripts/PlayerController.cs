using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(EnhancedPhysicController))]
[RequireComponent(typeof(Walking))]
[RequireComponent(typeof(Jumping))]
[RequireComponent(typeof(Dashing))]
public class PlayerController : MonoBehaviour
{
    // Required components
    EnhancedPhysicController physicController;
    Walking walking;
    Jumping jumping;
    Dashing dashing;

    // Parameters
    public InputActionAsset inputs;
    public CinemachineFreeLook cameraController;
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public Slider cameraSpeed;

    // Internal
    List<System.Action> onJumpActions = new List<System.Action>();
    List<System.Action> onDashActions = new List<System.Action>();


    void Start()
    {
        physicController = GetComponent<EnhancedPhysicController>();
        walking = GetComponent<Walking>();
        jumping = GetComponent<Jumping>();
        dashing  = GetComponent<Dashing>();

        var movementsInputs = inputs.FindActionMap("Movements");
        movementsInputs.Enable();
        movementsInputs["MoveForward"].performed += OnMoveForward;
        movementsInputs["MoveRight"].performed += OnMoveRight;
        movementsInputs["Jump"].performed += OnJump;
        movementsInputs["LookAt"].performed += OnLookAt;
        //movementsInputs["LookAt"].canceled += OnStopLookAt;
        movementsInputs["Dash"].performed += OnDash;

        onJumpActions.Add(() => { audioSource.clip = jumpSound; audioSource.Play(); });
        onDashActions.Add(() => { audioSource.clip = dashSound; audioSource.Play(); });
    }

    void Stop()
    {
        var movementsInputs = inputs.FindActionMap("Movements");
        movementsInputs.Disable();
        movementsInputs["MoveForward"].performed -= OnMoveForward;
        movementsInputs["MoveRight"].performed -= OnMoveRight;
        movementsInputs["Jump"].performed -= OnJump;
        movementsInputs["LookAt"].performed -= OnLookAt;
        //movementsInputs["LookAt"].canceled += OnStopLookAt;
        movementsInputs["Dash"].performed -= OnDash;
    }

    void FixedUpdate()
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
        if (jumping.Jump())
            foreach (var action in onJumpActions)
                action();
    }

    void OnDash(InputAction.CallbackContext context)
    {
        if (dashing.Dash())
            foreach (var action in onDashActions)
                action();
    }

    void OnLookAt(InputAction.CallbackContext context)
    {
        //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
        Vector2 lookMovement = context.ReadValue<Vector2>().normalized;

        // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        //lookMovement.x = lookMovement.x * 180f; 

        //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        cameraController.m_XAxis.Value += lookMovement.x * cameraSpeed.value * Time.deltaTime;
        //cameraController.m_YAxis.Value += lookMovement.y * Time.deltaTime;

        //walking.RotateY(Camera.main.transform.rotation.y);
        var rot = transform.rotation.eulerAngles;
        rot.y = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation= Quaternion.Euler(rot);
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

    public void AddJumpAction(System.Action action) { onJumpActions.Add(action); }
    public void ClearJumpActions() { onJumpActions.Clear(); }

    public void AddDashAction(System.Action action) { onDashActions.Add(action); }
    public void ClearDashActions() { onDashActions.Clear(); }
}