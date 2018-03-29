using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the audio in the game
/// </summary>
public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl instance;           // for calling public methods in this script
    public PlayerAudio playerAudio;             // for accessing player audio effects
    public AudioFX audioFX;                     // for accessing non player audio
    public Transform player;                    // to play the sound at the player position
    public GameObject BGMusic;                  // to toggle the bg music
    public bool bgMusicOn;                      // to toggle bg music on or off

    [Tooltip("soundOn is used to toggle sound on/off from the Inspector")]
    public bool soundOn;

	// Use this for initialization
	void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (bgMusicOn)
        {
            BGMusic.SetActive(true);
        }
	}
	
	public void PlayerJump(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player jumps at the position where the player is
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerJump, playerPos);
        }
    }

    public void CoinPickup(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player picks up the coin and at the position where the player is
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.coinPickup, playerPos);
        }
    }

    public void FireSpells(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player throws his spell
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.fireSpells, playerPos);
        }
    }

    public void EnemyExplosion(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the spell hits the enemy
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyExplosion, playerPos);
        }
    }

    public void BreakableCrates(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the spell hits the enemy
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.breakCrates, playerPos);
        }
    }

    public void PowerUp(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player picks up the powerup box
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.powerUp, playerPos);
        }
    }

    public void KeyFound(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player picks up the key
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.keyFound, playerPos);
        }
    }

    public void EnemyHit(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player stomps the enemy
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyHit, playerPos);
        }
    }

    public void PlayerDied(Vector3 playerPos)
    {
        // if the soundOn is checked, play the audio when the player dies
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerDied, playerPos);
        }
    }
}