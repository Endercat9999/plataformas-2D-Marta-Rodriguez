using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public static bool isGrounded;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
           isGrounded = true; 
           playerControler.characterAnimator.SetBool("isjumping", false);
        }
        
       
    }

    void OnTriggerStry2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
           isGrounded = true; 
        }
       
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            isGrounded = false;
            playerControler.characterAnimator.SetBool("isjumping", true);
        }
        
    }
}
