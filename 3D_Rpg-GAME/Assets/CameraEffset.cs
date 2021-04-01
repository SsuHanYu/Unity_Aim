using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffset : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private int RayValue = 2;
    private float Distance = 15;
    [SerializeField] Material NormalMaterial;
    [SerializeField] Material TransparentMaterial;
    [SerializeField] LayerMask layermask;
    [SerializeField] float MaxDistance = 15;

    private Queue<GameObject> HitTheObject;

    private void Start()
    {
        HitTheObject = new Queue<GameObject>(RayValue);
    }

    private void Update()
    {
        for(int i = 0; i < RayValue; i++)
        {
            Distance = -Distance;
            Vector3 ScreemCemter = new Vector3((Screen.width / 2) + Distance * RayValue, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(ScreemCemter);
            RaycastHit[] hit = Physics.RaycastAll(ray.origin, ray.direction, MaxDistance, layermask);
            Debug.DrawRay(ray.origin, ray.direction * MaxDistance);

            for (int HitValue = 0; HitValue < hit.Length; HitValue++)
            {
                if (hit[HitValue].transform.gameObject != null && !AlreadyTransParent(hit[HitValue].transform.gameObject))
                {
                    hit[HitValue].transform.GetComponent<MeshRenderer>().material = TransparentMaterial;
                    HitTheObject.Enqueue(hit[HitValue].transform.gameObject);
                }
            }

            if (hit.Length == 0 && HitTheObject.Count > 0)
            {
                for (int HitObj = 0; HitObj < HitTheObject.Count; HitObj++)
                {
                    GameObject obj = HitTheObject.Dequeue();
                    obj.GetComponent<MeshRenderer>().material = NormalMaterial;
                }
            }
        }
    }

    bool AlreadyTransParent(GameObject obj)
    {
        for (int HitObj = 0; HitObj < HitTheObject.Count; HitObj++)
        {
            if (HitTheObject.Contains(obj))
                return true;
        }

        return false;
    }
}
