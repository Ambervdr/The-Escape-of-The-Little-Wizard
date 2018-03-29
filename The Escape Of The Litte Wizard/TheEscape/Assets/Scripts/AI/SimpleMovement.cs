using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the enemy move in a straight line
/// Also checks for collisions and reverse enemy movement direction
/// </summary>
public class SimpleMovement : MonoBehaviour
{
    public float speed;         // speed at which the enemy is gonna move
    Rigidbody2D rb;             // getting access to the Rigidbody of the enemy
    SpriteRenderer sr;          

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        SetStartingDirection();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void SetStartingDirection()
    {
        if (speed > 0)
        {
            sr.flipX = false;
        }
        else if (speed < 0)
        {
            sr.flipX = true;
        }
    }

    void FlipOnCollision()
    {
        speed = -speed;
        SetStartingDirection();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            FlipOnCollision();
        }
    }
}
