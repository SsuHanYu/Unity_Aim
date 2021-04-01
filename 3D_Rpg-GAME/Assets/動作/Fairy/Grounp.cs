using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounp : MonoBehaviour
{
    [Tooltip("玩家鋼體")]
    public Rigidbody PlayerRig;
    public LayerMask Layer;
    public float GroundHight;

    private void Update()
    {
        Ray myray = new Ray(transform.position, Vector3.down);
        RaycastHit myhit;

        if(Physics.Raycast(myray, out myhit, GroundHight, Layer))
        {
            PlayerRig.useGravity = false;

            Debug.DrawLine(transform.position, myhit.point, Color.red);

            float PlayerHight = myhit.point.y;
            PlayerHight += GroundHight;

            PlayerRig.transform.position = new Vector3
                (
                PlayerRig.transform.position.x,
                PlayerHight,
                PlayerRig.transform.position.z
                );
        }
        else
        {
            PlayerRig.useGravity = true;
        }
    }
}
