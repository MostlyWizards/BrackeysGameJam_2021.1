using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDetected : MonoBehaviour
{
    public AutoDetection detector;
    public Material material;
    new public Renderer renderer;
    public AudioClip activate;
    public AudioClip desactivate;

    bool isDetecting;
    Material oldMaterial;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        isDetecting = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDetecting && detector.HasDetectedSomething())
        {
            oldMaterial = renderer.sharedMaterial;
            renderer.sharedMaterial = material;
            isDetecting = true;
            audioSource.clip = activate;
            audioSource.volume = 0.4f;
            audioSource.Play();
        }
        else if (isDetecting && !detector.HasDetectedSomething())
        {
            renderer.sharedMaterial = oldMaterial;
            isDetecting = false;
            audioSource.clip = desactivate;
            audioSource.volume = 0.1f;
            audioSource.Play();
        }
    }
}
