using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys any game object that comes in contact with it except the player
/// For the player, the level will be restarted
/// </summary>
public class GarbageCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.PlayerDied(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }     
}
