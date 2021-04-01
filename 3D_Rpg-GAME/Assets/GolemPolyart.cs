using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemPolyart : MonoBehaviour
{
    public enum EnemyState
    {
        Move,   // 移動
        Attack, // 攻擊
        Head,   // 受傷
        Dead    // 死亡
    }
    public EnemyState enemystate = EnemyState.Move;
    NavMeshAgent EnemyNav;
    Animator EnemyAnim;

    [Range(0, 100)]
    public int HP = 100;
    public int Attack = 2;
    public int walkSpeed = 2;
    public int RunSpeed = 3;
    private bool IsAttack = false;
    private float AttackTime = 2;
    private bool IsHead = false;
    private float HeadTime = 1;
    private Transform Player;
    public Transform[] TargetPoint;
    private int NextPoint = 0;
    public GameObject HeadHitParticlelSystem;
    public GameObject AttackRange;
    public GameObject rainbow;

    // Start is called before the first frame update
    void Start()
    {
        EnemyNav = GetComponent<NavMeshAgent>();
        EnemyAnim = GetComponent<Animator>();
        Player = FindObjectOfType<ChaWitch>().transform;
    }

    void FixedUpdate()
    {
        switch (enemystate)
        {
            // 怪物移動狀態
            case EnemyState.Move:

                // 怪物要前往的位置
                EnemyNav.SetDestination(TargetPoint[NextPoint].position);
                // 播放走路動畫
                EnemyAnim.SetFloat("Move", 0.5f);
                // 如果怪物距離目標巡邏點的距離小於2
                if (Vector3.Distance(transform.position, TargetPoint[NextPoint].position) < 2)
                {
                    // 換成下個巡邏點
                    NextPoint++;
                    // 如果已經跑完最後一個目標我們就回到第一個巡邏點
                    if (NextPoint >= TargetPoint.Length)
                        NextPoint = 0;
                }
                // 玩家距離怪物只剩下5公尺我們要追擊玩家
                if (Vector3.Distance(transform.position, Player.position) < 5)
                {
                    // 改成怪物攻擊狀態去追玩家
                    enemystate = EnemyState.Attack;
                    // 怪物改成跑步速度
                    EnemyNav.speed = RunSpeed;
                    // 怪物到目標點停止距離設為2
                    EnemyNav.stoppingDistance = 2;
                }

                break;
            // 怪物攻擊狀態
            case EnemyState.Attack:
                // 怪物追擊目標
                EnemyNav.SetDestination(Player.position);
                // 判斷是不是在攻擊中
                if (IsAttack)
                {
                    // 還剩下多少攻擊時間
                    AttackTime -= Time.deltaTime;
                    // 攻擊已經撥放完畢
                    if (AttackTime < 0)
                    {
                        // 結束攻擊
                        IsAttack = false;
                        // 回到移動狀態
                        enemystate = EnemyState.Move;
                        // 速度改為走路速度
                        EnemyNav.speed = walkSpeed;
                        // 怪物到目標點停止距離設為0
                        EnemyNav.stoppingDistance = 0;
                    }
                    return;
                }
                // 撥放跑步動畫
                EnemyAnim.SetFloat("Move", 1);
                // 確認玩家是否活著
                if (Player != null)
                {
                    // 玩家與怪物距離小於1
                    if (Vector3.Distance(transform.position, Player.position) < 3)
                    {
                        // 判斷要攻擊
                        IsAttack = true;
                        // 撥放攻擊動畫
                        EnemyAnim.SetTrigger("Witch Attack");
                        // 攻擊時間為2秒鐘
                        AttackTime = 2;
                        StartCoroutine(AttackEvent());
                    }
                }
                // 怪物與玩家距離大於10公尺
                else if (Vector3.Distance(transform.position, Player.position) > 10)
                {
                    // 回復到移動狀態
                    enemystate = EnemyState.Move;
                    // 速度改為走路速度
                    EnemyNav.speed = walkSpeed;
                    // 怪物到目標點停止距離設為0
                    EnemyNav.stoppingDistance = 0;
                }

                break;
            // 怪物受傷狀態
            case EnemyState.Head:
                EnemyNav.SetDestination(transform.position);
                if (IsHead)
                {
                    HeadHitParticlelSystem.SetActive(true);
                    IsHead = false;
                    EnemyAnim.SetTrigger("Witch Head Hit");
                    rainbow.SetActive(true);
                }

                HeadTime -= Time.deltaTime;
                if(HeadTime < 0)
                {
                    enemystate = EnemyState.Move;
                    HeadHitParticlelSystem.SetActive(false);
                    rainbow.SetActive(false);
                }

                break;
            // 怪物死亡狀態
            case EnemyState.Dead:
                EnemyAnim.SetBool("Witch Death", true);
                StartCoroutine(DeathEvent());
                break;
        }
    }

    public void Injured(int BloodDeduction)
    {
        HP -= BloodDeduction;

        if (HP <= 0)
            enemystate = EnemyState.Dead;
        else
            enemystate = EnemyState.Head;
        HeadTime = 0.7f;
        IsHead = true;
    }

    IEnumerator DeathEvent()
    {
        EnemyNav.enabled = false;

        while (transform.position.y < -2)
        {
            transform.Translate(Vector3.down);
            yield return new WaitForFixedUpdate();
        }

        enabled = false;
        Destroy(gameObject);
    }

    public IEnumerator AttackEvent()
    {
        yield return new WaitForSeconds(1.18f);
        AttackRange.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        AttackRange.SetActive(false);
        AttackRange.GetComponent<AttackRange>().RemakeAttack();
    }
}
