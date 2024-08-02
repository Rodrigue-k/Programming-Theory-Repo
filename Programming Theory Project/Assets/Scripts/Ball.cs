using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    private int score;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask groundLayer;


    private TMP_Text scoreText;

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
        scoreText = GameObject.Find("Canvas/Score").GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateScore();

        isGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGround)
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            score++;
        }
        if (collision.gameObject.tag == "obstacle")
        {
            score--;
        }
    }

    void UpdateScore()
    {
       scoreText.text = $"score : {score}";
    }  

}
