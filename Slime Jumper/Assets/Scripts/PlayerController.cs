using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject slime;
    public Rigidbody2D rb;
    

    private bool isGrounded;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = new Vector2(-dashForce, rb.velocity.y);
        }
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
           
        }
    }
 }
