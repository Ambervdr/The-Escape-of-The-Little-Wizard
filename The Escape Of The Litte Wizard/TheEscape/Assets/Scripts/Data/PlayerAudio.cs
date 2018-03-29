using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;                           //Allows you to use the [Serializable] attribute

/// <summary>
/// Contains audio clips related to the player
/// </summary>

[Serializable]
public class PlayerAudio
{
    [Header("Part 1")]
    public AudioClip playerJump;            // when the player jumps
    public AudioClip coinPickup;            // when the player collects a coin
    public AudioClip fireSpells;           // when the player fires bullets
    public AudioClip enemyExplosion;        // when the player kills an enemy
    public AudioClip breakCrates;           // when the player breaks a crate

    [Header("Part 2")]
    public AudioClip powerUp;               // when the player collects the bullet poerwup
    public AudioClip keyFound;              // when the player collects the keys
    public AudioClip enemyHit;              // when he player jumps on the enemy's head
    public AudioClip playerDied;            // when the player dies
}