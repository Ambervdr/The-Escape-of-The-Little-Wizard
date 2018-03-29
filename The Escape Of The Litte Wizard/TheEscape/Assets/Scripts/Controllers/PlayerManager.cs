using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [Tooltip("This is an integer which speeds up the player's movement")]
    public int speedBoost;
    public float jumpSpeed;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;

    bool canDoubleJump;
    public bool isJumping;

    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public LayerMask whatIsGround;

    public float boxWidth;
    public float boxHeight;

    public float delayForDoubleJump;

    public Transform leftSpellSpawnPos, rightSpellSpawnPos;
    public GameObject leftSpell, rightSpell;

    public bool SFXOn;
    public bool canFire;

    public bool isStuck;

    // Use this for initialization
    void Start ()
    {
        //Getting access to the component you want to work with
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth, boxHeight), 360.0f, whatIsGround);

        float playerSpeed = Input.GetAxisRaw("Horizontal");

        playerSpeed *= speedBoost;

        if (playerSpeed != 0)
        {
            MoveHorizontal(playerSpeed);
        }
        else
        {
            StopMoving();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        ShowFalling();

        if (Input.GetButtonDown("Fire1"))
        {
            FireSpell();
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(feet.position, feetRadius);

        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
    }

    void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if (playerSpeed < 0)
        {
            sr.flipX = true;
        }
        else if (playerSpeed > 0)
        {
            sr.flipX = false;
        }

        if (!isJumping)
        {
            anim.SetInteger("State", 1);
        }
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (!isJumping)
        {
            anim.SetInteger("State", 0);
        }
    }

    void ShowFalling()
    {
        if (rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed));
            anim.SetInteger("State", 2);

            // play the jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            Invoke("EnableDoubleJump", delayForDoubleJump);
        }

        if (canDoubleJump && !isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpSpeed));
            anim.SetInteger("State", 2);

            // play the jump sound
            AudioCtrl.instance.PlayerJump(gameObject.transform.position);

            canDoubleJump = false;
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);

            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
        }
        if (other.gameObject.CompareTag("BigCoin"))
        {
            GameCtrl.instance.UpdateCoinCount();

            SFXCtrl.instance.ShowSpellSparkle(other.gameObject.transform.position);

            Destroy(other.gameObject);

            GameCtrl.instance.UpdateScore(GameCtrl.Item.BigCoin);

            // play the coin pickup sound
            AudioCtrl.instance.CoinPickup(gameObject.transform.position);
        }
    }

    void FireSpell()
    {
        if (canFire)
        {
            // makes the player fire bullets in the left direction
            if (sr.flipX)
            {
                Instantiate(leftSpell, leftSpellSpawnPos.position, Quaternion.identity);
            }

            // makes the player fire bullets in the right direction
            if (!sr.flipX)
            {
                Instantiate(rightSpell, rightSpellSpawnPos.position, Quaternion.identity);
            }

            // plays the fire spell sounds
            AudioCtrl.instance.FireSpells(gameObject.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                if (SFXOn)
                {
                    SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);

                    GameCtrl.instance.UpdateCoinCount();

                    GameCtrl.instance.UpdateScore(GameCtrl.Item.Coin);

                    // play the coin pickup sound
                    AudioCtrl.instance.CoinPickup(gameObject.transform.position);
                }
                break;

            case "Powerup_spell":
                canFire = true;
                Vector3 powerupPos = other.gameObject.transform.position;

                AudioCtrl.instance.PowerUp(gameObject.transform.position);

                Destroy(other.gameObject);

                if (SFXOn)
                {
                    SFXCtrl.instance.ShowSpellSparkle(powerupPos);
                }
                break;

            case "Enemy":
                GameCtrl.instance.PlayerDiedAnimation(gameObject);
                AudioCtrl.instance.PlayerDied(gameObject.transform.position);
                break;

            default:
                break;
        }
    }
}
