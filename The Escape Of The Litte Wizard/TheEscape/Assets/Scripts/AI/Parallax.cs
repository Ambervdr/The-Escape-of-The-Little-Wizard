using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides the parallax effect to the background
/// </summary>
public class Parallax : MonoBehaviour
{
    public GameObject player;       //To get access to the playerManager script
    public float speed;             //speed at which the bg moves. Set it to 0.001
    float offSetX;                  //Secret to horizontal parallax
    Material mat;                   //The material attached to the renderer of the Quad
    PlayerManager playerManager;    //This script has the access to the isStuck variable

	// Use this for initialization
	void Start ()
    {
        mat = GetComponent<Renderer>().material;
        playerManager = player.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //offSetX += 0.005f;

        if (!playerManager.isStuck)
        {
            //Shows the parallax
            //Handles the keyboard and joystick
            offSetX += Input.GetAxisRaw("Horizontal") * speed;

            mat.SetTextureOffset("_MainTex", new Vector2(offSetX, 0));
        }
	}
}
