using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 5;
    public int hurt = 20;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            other.GetComponent<GolemPolyart>().Injured(hurt);
        }
    }
}
