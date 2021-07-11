using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float walkSpeed;
    public bool mustPatrol;
    private bool mustTurn;
    public Rigidbody2D rb;
    public Transform GroundCheck;
    public Transform WallCheck;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if( mustPatrol)
        {
            patrol();
        }
    }

    private void FixedUpdate()
    {
        if ( mustPatrol)
        {
            mustTurn = (!Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer) || Physics2D.OverlapCircle(WallCheck.position, 0.1f, groundLayer));
        }
    }
    void patrol()
    {
        if(mustTurn)
        {
            flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
