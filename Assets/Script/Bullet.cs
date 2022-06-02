using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 20f;
    Rigidbody2D MyRigidbody2D;
    PlayerMovement Player;
    float xSpeed;
    private bool facingRight = true;
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<PlayerMovement>();
        xSpeed = Player.transform.localScale.x * BulletSpeed;
    }

    private void Flip(float x)
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        MyRigidbody2D.velocity = new Vector2(xSpeed, 0f);
        if (xSpeed > 0 && !facingRight)
        {
            Flip(xSpeed);
        }

        else if (xSpeed < 0 && facingRight)
        {
            Flip(xSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}


