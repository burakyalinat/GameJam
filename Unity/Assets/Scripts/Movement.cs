using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;
    public float jump;
    public Transform groundCheckPoint;
    public float groundCheckLength;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Animator playerAnimation;
    public Vector3 respawnPoint;
    public LevelManager gameLevelManager;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        gameLevelManager = FindObjectOfType<LevelManager> ();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckLength, groundLayer);

        movement = Input.GetAxis("Horizontal");
        if (movement > 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (movement < 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("Ground", isTouchingGround);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDedector")
        {
            gameLevelManager.Respawn();
        }

        if (other.tag == "Enemy")
        {
            gameLevelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}