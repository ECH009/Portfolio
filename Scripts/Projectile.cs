/*Platformer - Projectile.cs
 * Version 1.0 by Micah Disney
 * 
 * Description:Script for destroying projectiles on impact.
 * 
 *
 * */




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.gameObject.tag;

        if(hitTag != "CheckPoint")
        {
            Destroy(gameObject);
        }



        
    }

}
