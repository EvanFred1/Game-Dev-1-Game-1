using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject slime;
    public Rigidbody2D rb;
    public GameObject endScreen;
    public GameObject winScreen;
    public bool isGrounded;
    private int speed = 3;
    private float jumpForce =9.0f;
    private float jumpTime = 1f;
    
    private float dashForce = 10.0f;
    private float dashTime = .5f;
   
    public int health = 3;
    private bool isInvinicble = false;
    private float invinicblityTimer = 0;
    private bool isDashinhg = false;
    
    private Vector3 currentVel;
    enum States
    {
        idle,
        move,
        jump,
        dashRight,
        dashLeft,
        climbRight,
        climbLeft
    }
    private States state = States.idle;

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
            case States.jump:
                break;
            case States.dashRight:
                dashRight();
                break;
            case States.dashLeft:
                dashLeft();
                break;
            case States.climbLeft:
                climbLeftState();
                break;
            case States.climbRight:
                climbRightState();
                break;
           
        }
        if (isInvinicble)
        {
            invinicblityTimer -= Time.deltaTime;
        }
        if (invinicblityTimer <= 0)
        {
            isInvinicble = false;
        }
        if(health <= 0)
        {
            GameOver();
        }
        Debug.Log(state);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Grounded");
            isGrounded = true;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("Jump", false);

        }
        if (collision.gameObject.CompareTag("Climbable Wall"))
        {
            state = States.climbLeft;
            transform.Rotate(0, 0, 90);
        }
        if (collision.gameObject.CompareTag("Climbable Right Wall"))
        {           
            state = States.climbRight;
            transform.Rotate(0, 0, -90);
        }
        if (collision.gameObject.CompareTag("Enemy") && !isInvinicble)
        {
            health--;
            isInvinicble = true;
            invinicblityTimer = 1;
            Debug.Log("Health is" + health);
        }
        if (collision.gameObject.CompareTag("Enemy") && isDashinhg)
        {
            Destroy(collision.gameObject);
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
            transform.Rotate(0, 0, -90);
            state = States.move;
        }
        if (collision.gameObject.CompareTag("Climbable Right Wall"))
        {
            transform.Rotate(0, 0, 90);
            state = States.move;
        }
    }
    private void dashRight()
    {
        rb.gravityScale = 1;
        isInvinicble = true;
        isDashinhg = true;
        invinicblityTimer = .5f;
        if (dashTime > 0) 
        {
            rb.velocity = new Vector2(dashForce, rb.velocity.y);
            dashTime =dashTime - Time.deltaTime;
        }
        
        if (dashTime <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x - dashForce, rb.velocity.y);
            dashTime = .5f;
            state = States.move;
            isDashinhg = false;
        }
        if (isInvinicble)
        {
            invinicblityTimer -= Time.deltaTime;
        }
    }
    private void dashLeft()
    {
        rb.gravityScale = 1;
        isInvinicble = true;
        isDashinhg = true;
        invinicblityTimer = .5f;
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(-dashForce, rb.velocity.y);
            dashTime = dashTime - Time.deltaTime;
        }

        if (dashTime <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x + dashForce, rb.velocity.y);
            dashTime = .5f;
            state = States.move;
            isDashinhg = false;
        }
        if (isInvinicble)
        {
            invinicblityTimer -= Time.deltaTime;
        }
    }
    private void moveState()
    {
        //Debug.Log("Move");
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Idle", false);

        animator.SetBool("Climb", false);
        rb.gravityScale = 1;
        if (isGrounded)
        {
            animator.SetBool("Walk", true);
        }
        
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
            StartCoroutine(Jump());
        }
        if (rb.velocity == Vector2.zero)
        {
            state = States.idle;
        }
        if (Input.GetKey(KeyCode.E))
        {
            state |= States.dashRight;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            state = States.dashLeft;
        }
    }
    private void idleState()
    {
        rb.gravityScale = 1;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        animator.SetBool("Climb", false);
        //Debug.Log("Idle");
        animator.SetBool("Walk", false);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            state = States.move;
        }
        if (Input.GetKey(KeyCode.E))
        {
            state = States.dashRight;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            state = States.dashLeft;
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(Jump());
        }
    }
    public IEnumerator Jump()
    {
        Animator animator = GetComponent<Animator>();
        yield return new WaitForSeconds(.05f);
        animator.SetBool("Walk", false );
        animator.SetBool("Jump", true);
    }
    private void climbLeftState()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("Climb", true);
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
    private void climbRightState()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("Climb", true);
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
  
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kill Barrier"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            winScreen.SetActive(true);
        }
    }
    private void GameOver()
    {
        Destroy(slime);
        endScreen.SetActive(true);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Grounded");
            isGrounded = true;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("Jump", false);

        }
    }
}
