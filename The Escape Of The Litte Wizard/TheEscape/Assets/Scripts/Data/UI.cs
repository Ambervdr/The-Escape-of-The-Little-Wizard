using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;                       // Gives access to the [Serializable] attribute
using UnityEngine.UI;               // Gives access to the Unity UI elements

/// <summary>
/// Groups all the user interface elements together for GameCtrl
/// </summary>

[Serializable]
public class UI
{
    [Header("Text")]
    public Text txtCoinCount;           // For updating the number of coins collected
    public Text txtScore;               // Used for showing the text in the HUD
    public Text txtTimer;               // For showing the timer in the HUD

    [Header("Images/Sprites")]
    public Image key0;
    public Image key1;
    public Image key2;
    public Sprite key0Full;
    public Sprite key1Full;
    public Sprite key2Full;
    public Image heart1;                // represents one player life
    public Image heart2;                // represents one player life
    public Image heart3;                // represents one player life
    public Sprite emptyHeart;           // shown when one life is lost
    public Sprite fullHeart;            // shown when lives are full

    public GameObject panelGameOver;    // the game over panel
}
