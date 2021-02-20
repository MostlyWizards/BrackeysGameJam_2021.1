using UnityEngine;

[RequireComponent(typeof(EnhancedPhysicController))]
public class Dashing : MomentumModifier
{
    // Required components
    EnhancedPhysicController physicController;

    // Parameters
    public AnimationCurve accCurv;
    public float dashSpeedMax;
    public float impulseTime;
    public float cooldown;

    // Internal
    bool isDashing;
    float currentDashSpeed;
    float currentTimer;
    float fixedAccStep;
    float currentAcc;
    float currentCooldown;

    public override E_MomentumAxis GetMomentumAxis() { return E_MomentumAxis.Vertical; }

    public override void Apply(ref Vector3 momentum)
    {
        if (!isDashing)
            return;
        momentum = VectorMath.RemoveDotVector(momentum, physicController.GetForward());
		momentum += physicController.GetForward() * currentDashSpeed;
    }

    void Start()
    {
        physicController = GetComponent<EnhancedPhysicController>();
        fixedAccStep = Mathf.Lerp(0, impulseTime, Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        currentCooldown -= Time.fixedDeltaTime;
        if (!isDashing)
            return;

        currentTimer += Time.fixedDeltaTime;
        currentAcc += fixedAccStep;

        currentDashSpeed = accCurv.Evaluate(currentAcc) * dashSpeedMax;

        if (currentTimer >= impulseTime)
            isDashing = false;
    }

    public bool Dash()
    {
        if (isDashing || currentCooldown >= 0)
            return false;

        isDashing = true;
        currentTimer = 0;
        currentAcc = 0;
        currentDashSpeed = 0;
        currentCooldown = cooldown;
        return true;
    }

    public float GetRemainingCooldown() { return currentCooldown; }
}