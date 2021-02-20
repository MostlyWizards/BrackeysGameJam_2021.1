using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SlimePlayer>())
            GameObject.FindObjectOfType<GameManager>().Win();
    }
}
