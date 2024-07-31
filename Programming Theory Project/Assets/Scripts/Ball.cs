using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    public bool isGround;

    public float Speed { get => speed; set
        {
            if (value >= 0f)
                speed = value;
            else
                Debug.LogWarning("Speed must be no-negative");
        }
         }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();

        isGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGround && Input.GetKey(KeyCode.Space))
        {
           Jump();
        }
        
    }

    protected virtual void Move()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
    }

    protected virtual void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

}
