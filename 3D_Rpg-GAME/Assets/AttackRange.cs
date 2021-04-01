using UnityEngine;
using System.Collections;

public class AttackRange : MonoBehaviour
{
    public int hurt = 20;
    private bool IsAttack = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && IsAttack == false)
        {
            IsAttack = true;
            other.GetComponent<ChaWitch>().BloodDeduction(hurt);
        }
    }

    public void RemakeAttack()
    {
        IsAttack = false;
    }
}
