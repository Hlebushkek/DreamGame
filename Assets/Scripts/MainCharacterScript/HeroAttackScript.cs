using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeroAttackScript : MonoBehaviour
{
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackPoint;
    
    public void Attack(float damage)
    {
        //AttackAnimator.SetTrigger("Attack");
        //Collider2D[] hitedEnemy = Physics2D.OverlapCapsuleAll(this.transform.position, new Vector2(2, 1), CapsuleDirection2D.Vertical, 0, enemyLayer);
        Collider2D[] hitedEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitedEnemy)
        {
            //Debug.Log("Hited" + enemy.name);
            enemy.GetComponent<EnemyAI>().TakeDamage(damage);
        } 
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
