using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks for collisions with a breakable game object and then informs the SFXCtrl to do the needful
/// </summary>
public class HeadCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            // Inform the SFXCtrl to do the needful
            SFXCtrl.instance.HandleBoxBreaking(other.gameObject.transform.parent.transform.position);

            // Set the velocity of the cat to zero
            gameObject.transform.parent.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
