using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackScript : MonoBehaviour
{
    [SerializeField] private Animator AttackAnimator;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackPoint;
    public float attackRate = 0.05f; //Attack per second
    private float nextAttackTime = 0f;
    
    public void Attack(float damage)
    {
        if (Time.time >= nextAttackTime)
        {
            //AttackAnimator.SetTrigger("Attack");

            nextAttackTime = Time.time + 1.0f / attackRate;

            Collider2D[] hitedEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach(Collider2D enemy in hitedEnemy)
            {
                Debug.Log("Hited" + enemy.name);
                enemy.GetComponent<EnemyAI>().TakeDamage(damage);
            } 
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
