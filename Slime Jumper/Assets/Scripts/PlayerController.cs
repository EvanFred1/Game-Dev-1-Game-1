using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject slime;
    public Rigidbody2D rb;
    

    private bool isGrounded;
    private bool isClimbing;
    private bool isClimbingRight;

    private int speed = 3;
    private float jumpForce =7.0f;
    private float dashForce = 10.0f;
    private float dashTime = 0;
    
    private Vector3 currentVel;
    enum States
    {
        idle,
        move,

    }
    private States state = States.idle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.move:
                moveState();
                break;
            case States.idle:
                idleState();
                break;
        }

     
       /* if (dashTime > 0)
        {
            dashTime = dashTime - Time.deltaTime;
        }
        if (isClimbing)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else if (isClimbingRight)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

        }
        else
        {
            
            
            if (Input.GetKeyDown(KeyCode.E) && dashTime <= 0)
            {
                // rb.velocity = new Vector2(dashForce, rb.velocity.y);
                //DashRight();
               // currentVel = new Vector2(dashForce, rb.velocity.y);
                
               // dashTime = .5f;
            }
            

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(-dashForce, rb.velocity.y);
            }
        }
        rb.velocity = currentVel;
       */
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        
        }
        if (collision.gameObject.CompareTag("Climbable Wall"))
        {
            isClimbing = true;
            transform.Rotate(0, 0, 90);
        }
        if (collision.gameObject.CompareTag("Climbable Right Wall"))
        {
            isClimbingRight = true;
            transform.Rotate(0, 0, -90);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
           
        }
        if (collision.gameObject.CompareTag("Climbable Wall"))
        {
            isClimbing = false;
            transform.Rotate(0, 0, -90);
        }
        if (collision.gameObject.CompareTag("Climbable Right Wall"))
        {
            isClimbingRight = false;
            transform.Rotate(0, 0, 90);
        }
    }
    private void DashRight()
    {
        
        /*while (dashTime > 0) 
        {
            rb.velocity = new Vector2(dashForce, rb.velocity.y);
            dashTime =dashTime - Time.deltaTime;
        }
        
        if (dashTime <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x - dashForce, rb.velocity.y);
            dashTime = .5f;
        }
        */
    }
    private void moveState()
    {
        Debug.Log("Move");
        rb.gravityScale = 1;
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * Time.deltaTime * speed);
            rb.velocity = new Vector2( speed , rb.velocity.y );
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * Time.deltaTime * speed);
            rb.velocity = new Vector2(-speed , rb.velocity.y);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (rb.velocity == Vector2.zero)
        {
            state = States.idle;
        }
    }
    private void idleState()
    {
        rb.gravityScale = 1;
        Debug.Log("Idle");
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            state = States.move;
        }
    }
 }
