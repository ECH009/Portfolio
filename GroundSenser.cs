/*Platformer - GroundSenser.cs
 * Version 1.0 by Micah Disney
 * 
 * Description:Script used to detect when player is on the ground.
 * 
 *
 * */


using UnityEngine;

public class GroundSenser : MonoBehaviour
{
    [SerializeField] PlayerControls playerLink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.gameObject.tag;

         if (hitTag == "Mover")
            {
            playerLink.transform.parent = collision.gameObject.transform;
            }
        
        if(hitTag == "Ground")
        {
            playerLink.isGrounded = true;
        }
    }
       private void OnTriggerExit2D(Collider2D collision)
        {
            var hitTag = collision.gameObject.tag;
           
            if (hitTag == "Ground")
            {
                playerLink.isGrounded = false;
            }
      
            if (hitTag == "Mover")
            {
                playerLink.transform.parent = null;
                playerLink.isGrounded = false;
            }

    }

        private void OnTriggerStay2D(Collider2D collision)
    {
        var hitTag = collision.gameObject.tag;

        if(hitTag == "Mover" || hitTag == "Ground")
        {
            playerLink.isGrounded = true;
        }
        if(hitTag == "Mover")
        {
            
            playerLink.isGrounded = true;
        }

    }
   

}
