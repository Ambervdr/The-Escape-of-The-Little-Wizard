using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks when the player is stuck
/// </summary>
public class PlayerStuck : MonoBehaviour
{
    public GameObject player;       //To get access to the playerManager script
    PlayerManager playerManager;    //To reference the PlayerManager script

	// Use this for initialization
	void Start ()
    {
        playerManager = player.GetComponent<PlayerManager>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        playerManager.isStuck = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerManager.isStuck = false;
    }
}
