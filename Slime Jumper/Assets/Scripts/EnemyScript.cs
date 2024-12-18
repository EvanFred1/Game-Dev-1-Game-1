using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float speed = 2f;
    private bool hitWall;
    private Vector2 direction = Vector2.right;
    public Sprite rightS;
    public Sprite leftS;
    private SpriteRenderer Sr;
    // Start is called before the first frame update
    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction = -direction;
            if (Sr.sprite.name == "Enemy Slime Right")
            {
                Sr.sprite = leftS;
            }
            else
            {
                Sr.sprite = rightS;
            }
        }
    }
}
    

   
