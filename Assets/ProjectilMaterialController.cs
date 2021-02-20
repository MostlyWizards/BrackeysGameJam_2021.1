using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilMaterialController : MonoBehaviour
{
    public Material material;
    public float speed;
    public float maxAmount;

    float value;
    // Start is called before the first frame update
    void Start()
    {
        //material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        value += Time.deltaTime * speed;
        value %= maxAmount;
        material.SetFloat("_Amount", value + 2);
        //Debug.Log(material.GetFloat("_Amount"));
    }
}
