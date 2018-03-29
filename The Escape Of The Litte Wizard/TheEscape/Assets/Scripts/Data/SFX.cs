using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Groups the particle effects used in the game
/// </summary>

[Serializable]
public class SFX
{
    public GameObject sfx_coin_pickup;          //Shown when the player picks coins
    public GameObject sfx_spell_pickup;         //Shown when the player picks spell powerup
    public GameObject sfx_playerLands;          //Shown when the player lands after jumping

    public GameObject sfx_box_fragment_1;       //Box fragment shown when the crate breaks
    public GameObject sfx_box_fragment_2;       //Box fragment shown when the crate breaks

    public GameObject sfx_enemy_explosion;      //Shown when the player bullet hits the enemy
}
