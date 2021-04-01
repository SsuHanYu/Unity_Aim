using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaWitch : MonoBehaviour
{
    public enum PlayerBehavior
    {
        Move,
        Attack,
        HeadHit,
        Death,
    }

    public PlayerBehavior playerbehavior;
    public Rigidbody PlayerRig; // 鋼體
    public Animator PlayerAnim; // 動畫控制器

    [Range(0, 100)]
    public float Hp = 100;
    public float Speed = 1; // 移動速度
    public float RunSpeed = 1.5f;
    public float Roation = 30; // 旋轉速度
    public KeyCode AtttackKey = KeyCode.Z;
    public bool IsAttack = false;
    public float AttackTime = 0.8f;
    public bool IsHeadHit = false;
    public float HeadHitTime = 0.7f;

    public int dragonSkingCount = 0;
    public GameObject Bullet;
    public Transform Muzzle;
    public GameObject AttackPaticle;
    public GameObject FireCircle;
    public GameObject rainbow;

    private void Start()
    {
        // 取得玩家的鋼體
        PlayerRig = GetComponent<Rigidbody>();
        // 取的玩家子物件的動畫控制器
        PlayerAnim = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        if (Hp <= 0)
        {
            playerbehavior = PlayerBehavior.Death;
        }
        else if (Input.GetKeyDown(AtttackKey))
        {
            playerbehavior = PlayerBehavior.Attack;
            IsAttack = true;
            AttackTime = 0.8f;
        }
    }

    private void FixedUpdate()
    {
        switch (playerbehavior)
        {
            case PlayerBehavior.Move:

                // 取得按下上下方向建的數值
                float InputZ = Input.GetAxis("Vertical");
                // 取得按下左右方向建的數值
                float InputX = Input.GetAxis("Horizontal");
                // 腳色前進的方向
                Vector3 Distance = new Vector3(InputX, 0, InputZ);
                float Move = Mathf.Clamp(Distance.magnitude, 0, 0.5f);

                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // 玩家前進的目標位置
                    PlayerRig.MovePosition(transform.position + Distance * Time.deltaTime * RunSpeed);
                    Move += 0.5f;
                }
                else
                {
                    // 玩家前進的目標位置
                    PlayerRig.MovePosition(transform.position + Distance * Time.deltaTime * Speed);
                }

                // 播放玩家走路或待機動畫
                PlayerAnim.SetFloat("Move", Move);
                if (Distance != Vector3.zero)
                {
                    // 創建目標的旋轉方向 ，設定Y軸的座標為正上方
                    // Distance 目標方向
                    // Vector3.up 參考座標
                    Quaternion targetRoation = Quaternion.LookRotation(Distance, Vector3.up);
                    Quaternion PlayerNewRoation = Quaternion.Lerp(transform.rotation, targetRoation, Time.deltaTime * Roation);
                    // 更新玩家目前的角度
                    PlayerRig.MoveRotation(PlayerNewRoation);
                }
                break;
            case PlayerBehavior.Attack:

                if (IsAttack)
                {
                    PlayerAnim.SetTrigger("Witch Attack");
                    IsAttack = false;
                    StartCoroutine(emission());
                }

                AttackTime -= Time.deltaTime;
                if (AttackTime <= 0)
                {
                    playerbehavior = PlayerBehavior.Move;
                }

                break;
            case PlayerBehavior.HeadHit:

                if (IsHeadHit)
                {
                    PlayerAnim.SetTrigger("Witch Head Hit");
                    StartCoroutine(HeadParticle());
                    IsHeadHit = false;
                }

                HeadHitTime -= Time.deltaTime;
                if (HeadHitTime <= 0)
                {
                    playerbehavior = PlayerBehavior.Move;
                }

                break;
            case PlayerBehavior.Death:

                PlayerAnim.SetBool("Witch Death", true);
                enabled = false;

                break;
        }
    }

    // 扣血機制
    public void BloodDeduction(int Number)
    {
        playerbehavior = PlayerBehavior.HeadHit;
        Hp -= Number;
        HeadHitTime = 0.7f;
        IsHeadHit = true;
    }

    public IEnumerator emission()
    {
        yield return new WaitForSeconds(0.65f);
        AttackPaticle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        FireCircle.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Instantiate(Bullet, Muzzle.position, Muzzle.rotation);
        yield return new WaitForSeconds(1);
        AttackPaticle.SetActive(false);
        FireCircle.SetActive(false);
    }

    public IEnumerator HeadParticle()
    {
        yield return new WaitForSeconds(0.1f);
        rainbow.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        rainbow.SetActive(false);
    }
}
