using UnityEngine;

[RequireComponent(typeof(EnhancedPhysicController))]
public class Jumping : MomentumModifier
{
    // Required components
    EnhancedPhysicController physicController;

    // Parameters
    public AnimationCurve accCurv;
    public float jumpSpeedMax;
    public float risingTime;

    // Internal
    bool isJumping;
    float currentJumpSpeed;
    float currentTimer;
    float fixedAccStep;
    float currentAcc;

    public override E_MomentumAxis GetMomentumAxis() { return E_MomentumAxis.Vertical; }

    public override void Apply(ref Vector3 momentum)
    {
        if (!isJumping)
            return;
        momentum = VectorMath.RemoveDotVector(momentum, transform.up);
		momentum += transform.up * currentJumpSpeed;
    }

    void Start()
    {
        fixedAccStep = Mathf.Lerp(0, risingTime, Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        if (!isJumping)
            return;

        currentTimer += Time.fixedDeltaTime;
        currentAcc += fixedAccStep;

        currentJumpSpeed = accCurv.Evaluate(currentAcc) * jumpSpeedMax;

        if (currentTimer >= risingTime)
            isJumping = false;
    }

    public void Jump()
    {
        if (isJumping)
            return;

        isJumping = true;
        currentTimer = 0;
        currentAcc = 0;
        currentJumpSpeed = 0;
    }
}