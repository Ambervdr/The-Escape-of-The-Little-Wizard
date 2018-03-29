using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the camera follow the player
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    public float yOffset;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // makes the camera follow the player in the x axis
         transform.position = new Vector3(player.position.x, transform.position.y,transform.position.z);

        // makes the camera follow the player in the x axis and y axis
        //transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
    }
}
