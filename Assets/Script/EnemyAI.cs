using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyStates
    {
        Walking,
        Knockback,
        Death
    }
    private EnemyStates currentState;
    private bool groundDetected, wallDetected;
    [SerializeField]
    private float groundCheckDistance, wallCheckDistance;
    [SerializeField]
    private float movementSpeed = 3.0f, maxhealth, currentHealth, knockbackStartTime;
    private Vector2 movement, knockbackSpeed;
    private int facingDirection, damageDirection;

    [SerializeField]
    private Transform groundCheck, wallCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private GameObject alive;
    [SerializeField]
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;
    private UnityEngine.Object EnemyReference;
    [SerializeField]
    private GameObject DropMoonStone;
    private void Start()
    {
        aliveRb = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
        aliveAnim = alive.GetComponent<Animator>();
        currentState = EnemyStates.Walking;
        EnemyReference = Resources.Load("Slime 1");
    }
    private void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Walking:
                UpdateWalkingState();
                break;
            case EnemyStates.Knockback:
                UpdateKnockBackState();
                break;
            case EnemyStates.Death:
                UpdateDeadState();
                break;
        }
    }
    //Walking ---------------------------------------------------------------
    private void EnterWalkingState()
    {
        
    }
    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        if (!groundDetected || wallDetected)
        {
            Flip();
        } else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        };
    }
    private void ExitWalkingState()
    {

    }
    //KnockBack--------------------------------------------------------------
    private void EnterKnockBackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y * damageDirection);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }
    private void UpdateKnockBackState()
    {
        if (Time.time >= knockbackStartTime + damageDirection)
        {
            SwitchState(EnemyStates.Walking);
        }
    }
    private void ExitKnockBackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    //Dead ------------------------------------------------------------------
    private void EnterDeadState()
    {
        //spawn blood
        DropItem();
        gameObject.SetActive(false);
        Invoke("Respawn", 5);
    }
    private void UpdateDeadState()
    {
        
    }
    private void ExitDeadState()
    {

    }
    //Another ---------------------------------------------------------------
    /*private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
    }*/
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
    private void SwitchState(EnemyStates state)
    {
        switch(currentState)
        {
            case EnemyStates.Walking:
                ExitWalkingState();
                break;
            case EnemyStates.Death:
                ExitDeadState();
                break;
        }
        switch(state)
        {
            case EnemyStates.Walking:
                EnterWalkingState();
                break;
            case EnemyStates.Death:
                EnterDeadState();
                break;
        }
        currentState = state;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    public void TakeDamage(float amountDamage)
    {
        currentHealth -= amountDamage;
        if (currentHealth > 0.0f)
        {
            SwitchState(EnemyStates.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(EnemyStates.Death);
        }
    }
    private void Respawn()
    {
        GameObject enemyCopy = (GameObject)Instantiate(EnemyReference);
        enemyCopy.transform.position = transform.position;
        Destroy(gameObject);
    }
    private void DropItem()
    {
        int numOfItem = Random.Range(1, 4);
        for (int i = 0; i < numOfItem; i++)
        {
            Debug.Log("Instantiate ore");
            GameObject obj = Instantiate(DropMoonStone, transform.position, Quaternion.identity);
        }
    }
}