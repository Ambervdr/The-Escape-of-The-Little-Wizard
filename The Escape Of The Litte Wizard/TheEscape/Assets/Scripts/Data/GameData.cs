using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The data model for the game data
/// </summary>
/// 
[Serializable]
public class GameData
{
    public int coinCount;       // Tracks the number of coins collected
    public int lives;           // tracks the player lives
    public int score;           // Tracks the score

    public bool[] keyFound;     // For tracking which keys have been found

    public bool isFirstBoot;    // For initializing data when the game is started for the first time

}
