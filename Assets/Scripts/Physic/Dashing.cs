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

    // Internal
    bool isDashing;
    float currentDashSpeed;
    float currentTimer;
    float fixedAccStep;
    float currentAcc;

    public override E_MomentumAxis GetMomentumAxis() { return E_MomentumAxis.Vertical; }

    public override void Apply(ref Vector3 momentum)
    {
        if (!isDashing)
            return;
        momentum = VectorMath.RemoveDotVector(momentum, transform.forward);
		momentum += transform.forward * currentDashSpeed;

        Debug.Log(currentDashSpeed);
    }

    void Start()
    {
        fixedAccStep = Mathf.Lerp(0, impulseTime, Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        if (!isDashing)
            return;

        currentTimer += Time.fixedDeltaTime;
        currentAcc += fixedAccStep;

        currentDashSpeed = accCurv.Evaluate(currentAcc) * dashSpeedMax;

        if (currentTimer >= impulseTime)
            isDashing = false;
    }

    public void Dash()
    {
        if (isDashing)
            return;

        Debug.Log("Dash!");
        isDashing = true;
        currentTimer = 0;
        currentAcc = 0;
        currentDashSpeed = 0;
    }
}