using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public Transform TargetPosint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && 
           other.GetComponent<ChaWitch>().dragonSkingCount > 0)
        {
            other.transform.position = TargetPosint.position;
        }
    }
}
