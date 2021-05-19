using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDawnCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1;
    bool makeMovement = true;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator anim;
    [SerializeField] GameObject levelloader;
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelloader.GetComponent<LevelLoaderScript>().LoadLevel(0);
        }
    }
    void FixedUpdate()
    {
        if (makeMovement == true)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }
    void HoeAnimStart()
    {
        anim.SetBool("HoeStrike", true);
        makeMovement = false;
    }
    void HoeAnimEnd()
    {
        anim.SetBool("HoeStrike", false);
        makeMovement = true;
    }
}
