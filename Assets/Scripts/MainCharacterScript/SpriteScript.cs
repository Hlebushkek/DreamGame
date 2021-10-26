using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 5;
    [SerializeField]
    private float JumpForce = 3;
    private float Movement;
    private Rigidbody2D GirlSpriteRB;
    [SerializeField]
    private  Animator GirlRun;
    private void Start()
    {
        GirlSpriteRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Movement = Input.GetAxis("Horizontal") * MovementSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(GirlSpriteRB.velocity.y) < 0.001f)
        {
            GirlSpriteRB.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        if (Movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (Movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetAxis("Horizontal") != 0f)
        {
            GirlRun.SetFloat("RunSpeedAnim", Mathf.Abs(Movement));
        } else {GirlRun.SetFloat("RunSpeedAnim", 0f);}
        transform.position += new Vector3(Movement, 0f, 0f) * Time.deltaTime;
    }
}
