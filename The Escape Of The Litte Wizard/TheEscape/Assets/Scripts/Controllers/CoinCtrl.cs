using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the coin behaviour when the player interacts with it
/// </summary>
public class CoinCtrl : MonoBehaviour
{
    public enum CoinFX
    {
        Vanish,
        Fly
    }

    public CoinFX coinFX;       // Variable of the type CoinFX enum
    public float speed;         // Speed at which the coin flies
    public bool startFlying;    // If true, coin will fly to the HUD when collected

    GameObject coinMeter;       // Receives the coin in the HUD

    void Start()
    {
        startFlying = false;

        if (coinFX == CoinFX.Fly)
        {
            coinMeter = GameObject.Find("img_Coin_Count");
        }
    }

    void Update()
    {
        if (startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coinFX == CoinFX.Vanish)
            {
                Destroy(gameObject); // Destroys the coin
            }
            else if (coinFX == CoinFX.Fly)
            {
                startFlying = true;
            }
        }
    }
}
