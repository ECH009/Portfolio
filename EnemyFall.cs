using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFall : MonoBehaviour
{
    [SerializeField] GameObject enemyLink;
    AudioSource audioLink;
    float startY;
    float currentY;
    [SerializeField] float fallDistance;
    [SerializeField] AudioClip fallSound;
    bool fallen;



    void Start()
    {
        audioLink = enemyLink.GetComponent<AudioSource>();
        startY = enemyLink.transform.position.y;
    }


    void Update()
    {
        currentY = enemyLink.transform.position.y;


        if(currentY < startY - fallDistance && fallen == false)
        {

            StartCoroutine(FallDelay());

        }
    }


    IEnumerator FallDelay()
    {
    fallen = true;
    audioLink.PlayOneShot(fallSound);
     yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}


