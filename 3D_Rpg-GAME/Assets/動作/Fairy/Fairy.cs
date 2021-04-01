using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    Animator anim;
    public float Rotation = 5;
    public float ForeardSpeed = 2;

    float AnimSpeed = 0;
    float StandTime = 6;

    bool IsStand;
    bool IsPelt;
    bool IsAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (IsPelt || IsAttack)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Attack");
            IsAttack = true;
            Invoke("AttackInExecution", 1.05f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Pelt");
            IsPelt = true;
            Invoke("PelnExecution", 1.3f);
        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            StandTime -= Time.deltaTime;

            if(StandTime < 0)
            {
                StandTime = Random.Range(9, 12);
                IsStand = true;
                anim.SetBool("Stand", IsStand);

                Invoke("StandExecution", 4);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            ForeardSpeed = 0.15f;
            AnimSpeed = 0.5f;
        }
        else
        {
            ForeardSpeed = 0.1f;
            AnimSpeed = 0;
        }

        if(Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * Rotation, 0);
        }

        if(Input.GetAxis("Vertical") > 0)
        {
            AnimSpeed += Input.GetAxis("Vertical") / 2;
            anim.SetFloat("Speed", AnimSpeed);

            transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical") * ForeardSpeed));
        }
    }

    void AttackInExecution()
    {
        IsAttack = false;
    }

    void PelnExecution()
    {
        IsPelt = false;
    }

    void StandExecution()
    {
        IsStand = false;
        anim.SetBool("Stand", IsStand);
    }
}
