using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInformation
{
    public float Health;
    public float PhysicalAttack;
    public float ElementalAttack;

    public float PhysicalDefense;
    public float ElementalDefense;

    public float Speed;
    public float Evasiveness;


}


public class EnemyScript : MonoBehaviour
{
    public bool isAttacking;
    public Animator animator;
    public TextAsset enemyInformationText;
    public EnemyInformation enemyInformation;
    public bool isDead;
    string[] lines;

    // Start is called before the first frame update
    void Awake()
    {
        this.animator = GetComponentInChildren<Animator>();
        enemyInformation = new();
        isDead = false;
        lines = enemyInformationText.text.Split("\n"[0]);

        enemyInformation.Health = float.Parse(lines[0]);
        enemyInformation.PhysicalAttack = float.Parse(lines[1]);
        enemyInformation.ElementalAttack = float.Parse(lines[2]);
        enemyInformation.PhysicalDefense = float.Parse(lines[3]);
        enemyInformation.ElementalDefense = float.Parse(lines[4]);
        enemyInformation.Speed = float.Parse(lines[5]);
        enemyInformation.Evasiveness = float.Parse(lines[6]);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    public void TakeDamage(float amount, bool isPhysical)
    {
        if (isPhysical)
        {
            enemyInformation.Health -= amount * enemyInformation.PhysicalDefense;
        }
        else
        {
            enemyInformation.Health = amount * enemyInformation.ElementalDefense;
        }

        if (enemyInformation.Health <= 0)
        {
            isDead = true;
        }
    }

    public float DealDamage(float amount, bool isPhysical)
    {
        if (isPhysical)
        {
            return amount * enemyInformation.PhysicalAttack;
        }
        else
        {
            return amount * enemyInformation.ElementalAttack;
        }
    }

    public float GetSpeed()
    {
        return Random.Range(enemyInformation.Speed - 0.1f, enemyInformation.Speed + 0.1f);
    }

    public void StartAnimation()
    {
        animator.SetBool("isAttacking", true);
        isAttacking = true;
    }


    public void EndAnimation()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}
