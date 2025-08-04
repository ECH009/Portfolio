/*Platformer - Spawner.cs
 * Version 1.0 by Micah Disney
 * 
 * Description:Script used to Spawn projectiles.
 * 
 *
 * */




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject flipperLink;
    GameObject spawnLink;
    [SerializeField] GameObject projectile;
    GameObject instance;
    [SerializeField] float speed;
    float launchForce;
    Rigidbody2D rigidLink;

    void Start()
    {
        flipperLink = transform.Find("Flipper").gameObject;
        spawnLink = flipperLink.transform.Find("Spawner").gameObject;

    }

    public void fireProjectile()
    {
        instance = Instantiate(projectile, spawnLink.transform.position, spawnLink.transform.rotation);

        launchForce = speed;

        if(flipperLink.transform.localScale.x == -1)
        {
            launchForce = launchForce * -1;
        }
        rigidLink = instance.GetComponent<Rigidbody2D>();
        rigidLink.AddForce(new Vector2(launchForce, 0));

    }



}