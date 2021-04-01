using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fairy_Mouse : MonoBehaviour
{
    Animator anim = null;
    NavMeshAgent navmeshagent;
    public LayerMask layer;
    public float Roatae = 5;
    public float ForeardSpeed = 2;
    public float WlakSpeed = 2;
    public float RunSpeed = 5;

    float AnimSpeed = 0;
    float StandTime = 6;

    bool IsStand = false;
    bool IsPelt = false;
    bool IsAttack = false;

    private void Start()
    {
        anim = GetComponent<Animator>(); 

        navmeshagent = GetComponent<NavMeshAgent>();
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
            Invoke("AttackInExcution", 1.05f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Pelt");
            IsPelt = true;
            Invoke("PeltInExcution", 1.3f);
        }

        if(navmeshagent.remainingDistance <= 0.1F)
        {
            StandTime = StandTime - Time.deltaTime;

            if(StandTime < 0)
            {
                StandTime = Random.Range(9, 12);
                IsStand = true;
                anim.SetBool("Stand", IsStand);

                Invoke("StandExecturion", 4);
            }
        }
        else
        {
            StandExecturion();
        }

        if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && navmeshagent.remainingDistance >= 0.1f)
        {
            navmeshagent.speed = RunSpeed;
            AnimSpeed = 1;
        }
        else if(navmeshagent.remainingDistance >= 0.1f)
        {
            navmeshagent.speed = WlakSpeed;
            AnimSpeed = 0.5f;
        }
        else
        {
            AnimSpeed = 0;
        }
        anim.SetFloat("Speed", AnimSpeed);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, layer))
        {
            navmeshagent.SetDestination(hit.point);
        }
    }

    void AttackInExcution()
    {
        IsAttack = false;
    }

    void PeltInExcution()
    {
        IsPelt = false;
    }

    void StandExecturion()
    {
        IsStand = false;
        anim.SetBool("Stand", IsStand);
    }
}
