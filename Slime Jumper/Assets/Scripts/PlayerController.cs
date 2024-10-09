using System.Collections;
using System.Collections.Generic;
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
    private float dashForce = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
           

        }
        else
        {
            rb.gravityScale = 1;
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
          
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                rb.velocity = new Vector2(dashForce, rb.velocity.y);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(-dashForce, rb.velocity.y);
            }
        }
   
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
 }
