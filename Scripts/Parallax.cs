/* Parrallax.cs 
 * version: 1.0 
 * by Micah Disney
 *Description: Script to add parralax effect to backround objects
 *adding the illusion of depth.
 * 
 * 
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float lastX;
    float currentX;
    float deltaX;

        [SerializeField] GameObject backround1;
        [SerializeField] GameObject backround2;
        [SerializeField] GameObject backround3;
        [SerializeField] GameObject backround4;

    [SerializeField] float multiplier1;
    [SerializeField] float multiplier2;
    [SerializeField] float multiplier3;
    [SerializeField] float multiplier4;

   
    void Start()
    {
        lastX = transform.position.x;
    }

    
    void Update()
    {
        currentX = transform.position.x;
        deltaX = currentX - lastX;

        backround1.transform.Translate(deltaX * multiplier1, 0, 0);
        backround2.transform.Translate(deltaX * multiplier2, 0, 0);
        backround3.transform.Translate(deltaX * multiplier3, 0, 0);
        backround4.transform.Translate(deltaX * multiplier4, 0, 0);

        lastX = currentX;


    }



}
