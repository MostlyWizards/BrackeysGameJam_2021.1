using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SlimePlayer : Damageable
{
    // Parameters
    public InputActionAsset inputs;
    public float startingScale;
    public float maxScale;
    public int maxSlimeStockCapacity = 10;

    public GameObject slimeWarriorPrefab;
    public float slimeProjectionForce;
    public AudioClip launchSlimeSound;
    public Slider healthUI;
    public Slider slimeStockUI;


    // Internal
    int slimeStock;
    AudioSource audioSource;
    InvuDamage damaged;
    EnhancedPhysicController physicController;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        slimeStock = 0;
        currentLife = initialLife;
        healthUI.maxValue = initialLife;
        slimeStockUI.maxValue = 5;
        audioSource = GetComponent<AudioSource>();
        damaged = GetComponent<InvuDamage>();
        physicController = GetComponent<EnhancedPhysicController>();
        RefreshScale();
        var movementsInputs = inputs.FindActionMap("SlimeActions");
        movementsInputs.Enable();
        movementsInputs["LaunchSlime"].performed += OnLaunchSlime;

        slimeStockUI.value = slimeStock;
        healthUI.value = currentLife;
    }

    void OnCollisionEnter(Collision collision)
    {
        var collectable = collision.gameObject.GetComponent<Collectable>();
        if (collectable && slimeStock < 5)
        {
            ++slimeStock;
            slimeStockUI.value = slimeStock;
            GameObject.Destroy(collectable.gameObject);
            RefreshScale();
        }
    }

    void RefreshScale()
    {
        var scaleValue = Mathf.Lerp(startingScale, maxScale, (float)slimeStock/(float)maxSlimeStockCapacity);
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    void OnLaunchSlime(InputAction.CallbackContext ctx)
    {
        if (slimeStock > 0)
        {
            slimeStock--;
            slimeStockUI.value = slimeStock;
            RefreshScale();
            var slime = GameObject.Instantiate(slimeWarriorPrefab, transform.position + new Vector3(0,2,0), transform.rotation);
            var rbSlime = slime.GetComponent<Rigidbody>();
            rbSlime.AddForce((physicController.GetForward() + transform.up) * slimeProjectionForce, ForceMode.Impulse);
            audioSource.clip = launchSlimeSound;
            audioSource.Play();
        }
    }

    public override void TakeDamage()
    {
        if (!damaged.IsRunning())
        {
            --currentLife;
            healthUI.value = currentLife;
            if (currentLife <= 0)
                GameObject.FindObjectOfType<GameManager>().Lose();
            if (damaged)
                damaged.RunEffect();
        }
    }
}
