using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private Animator myAnimator; 

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundLayerMask;

    private Collider2D[] results = new Collider2D[1];

    [SerializeField]
    private float speed = 3.5f;

    private float jumpForce = 5f;
        
    private bool jump = false;

    private bool isGrounded = false;

    private float life = 100f;

    private bool isAlive = true; 

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        CheckIfDead(); 
    }

    public void CheckIfDead()
    {
        if (isAlive) { 
            if (life < 0)
            {
                life = 0f; 
            }
            if (life == 0f)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        isAlive = false; 
        speed = 0f;

        myAnimator.SetTrigger("Died");

        //Destroy(gameObject);
    }


    private void Update()
    {
        if (isAlive) { 

            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (transform.right.x > 0 && horizontalInput < 0)
            {
                Flip();
            }

            if (transform.right.x < 0 && horizontalInput > 0)
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            myRigidbody.velocity =
            new Vector2(horizontalInput * speed,
            myRigidbody.velocity.y
            );


            myAnimator.SetFloat("HorizontalSpeed", Mathf.Abs(myRigidbody.velocity.x));
        }
    }

    private void FixedUpdate()
    {
        isGrounded = CheckForGround();

        if (jump && isGrounded)
        {
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        jump = false;
    }

    private bool CheckForGround()
    {
        return Physics2D.OverlapPointNonAlloc(groundCheck.position, results, groundLayerMask) > 0;
    }

    private void Flip()
    {
        Vector3 localRotation = transform.localEulerAngles;
        localRotation.y += 180f;
        transform.localEulerAngles = localRotation;  
    }
}
