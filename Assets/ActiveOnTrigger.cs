using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnTrigger : MonoBehaviour
{
    public GameObject[] toActivate;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < toActivate.Length; ++i)
            toActivate[i].SetActive(true);
        Destroy(gameObject);
    }
}
