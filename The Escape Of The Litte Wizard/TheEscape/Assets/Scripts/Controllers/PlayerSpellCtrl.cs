using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's bullet movement and collisions with the enemies
/// </summary>
public class PlayerSpellCtrl : MonoBehaviour
{
    public Vector2 velocity;
    Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = velocity;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);
            Destroy(gameObject);
        }
        else if(!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
