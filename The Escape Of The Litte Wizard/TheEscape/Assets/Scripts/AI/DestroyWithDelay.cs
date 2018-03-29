using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys the game object after a specified delay
/// </summary>
public class DestroyWithDelay : MonoBehaviour
{
    public float delay;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, delay);
	}
}
