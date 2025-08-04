/*Platformer - Enemy.cs
 * Version 1.0 by Micah Disney
 * 
 * Description:Script for Controlling the enemy, this includes, Destroying the enemy on Death.
 * 
 *
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 currentPosition;
    Vector2 goalPosition;
    [SerializeField] GameObject currentGoal;
    [SerializeField] float speedModifier;
    float moveAmount;
    float distanceToGoal;

    [SerializeField] GameObject startGoal;
    [SerializeField] GameObject endGoal;
    bool moveToGoal = true;
    [SerializeField] float idleTime;
    Animator animatorLink;
    SpriteRenderer renderLink;
    AudioSource audioLink;
    [SerializeField] AudioClip hitSound;
    bool enemyDead;
    [SerializeField] AudioClip attackSound;
    bool attack = true;
    [SerializeField] float attackTimeDelay;
    GameObject playerLink;
    Vector2 playerPosition;
    float distanceToPlayerX;
    float distanceToPlayerY;
    GameObject flipperLink;
    [SerializeField] float attackDistance;

    bool playerActive = true;


    void Start()
    {
        animatorLink = GetComponent<Animator>();
        renderLink = GetComponent<SpriteRenderer>();
        audioLink = GetComponent<AudioSource>();
        playerLink = GameObject.Find("Player");
        flipperLink = transform.Find("Flipper").gameObject;

    }




    void Update()
    {
        Patrol();
        Attack();
    }

    private void Attack()
    {
        playerPosition = playerLink.transform.position;

        distanceToPlayerX = Mathf.Abs(playerPosition.x - transform.position.x);
        distanceToPlayerY = Mathf.Abs(playerPosition.y - transform.position.y);

        if (distanceToPlayerY < 0.5 && playerActive == true)
        {
            if (distanceToPlayerX < attackDistance && attack == true)
            {
                attack = false;

                if (playerPosition.x > transform.position.x)
                {
                    renderLink.flipX = false;
                    flipperLink.transform.localScale = new Vector2(1, 1);

                }

                else
                {
                    renderLink.flipX = true;
                    flipperLink.transform.localScale = new Vector2(-1, 1);

                }


                animatorLink.SetTrigger("Attack");
                StartCoroutine(AttackDelayTimer());


            }


        }




    }

    private void Patrol()
    {
        currentPosition = transform.position;
        goalPosition = currentGoal.transform.position;
        moveAmount = speedModifier * Time.deltaTime;

        distanceToGoal = Vector2.Distance(goalPosition, currentPosition);

        if (distanceToGoal > moveAmount && moveToGoal == true)
        {
            transform.position = Vector2.MoveTowards(currentPosition, goalPosition, moveAmount);
        }

        else if (distanceToGoal < moveAmount && moveToGoal == true)
        {
            if (currentGoal == startGoal)
            {
                currentGoal = endGoal;
                renderLink.flipX = false;
            }
            else
            {
                currentGoal = startGoal;
                renderLink.flipX = true;


            }
            StartCoroutine(IdleTimer());
        }

        animatorLink.SetBool("Move", moveToGoal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.gameObject.tag;

        if (hitTag == "PlayerAttack" && enemyDead == false)
        {
            enemyDead = true;
            animatorLink.SetTrigger("Death");
            audioLink.PlayOneShot(hitSound);
            moveToGoal = false;
            Invoke("DestroySelf", 1.5f);
        }
    }

    public void EnemyAttackSound()
    {
        audioLink.PlayOneShot(attackSound);
    }


    private void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
        Destroy(flipperLink.gameObject);
    }

    private IEnumerator IdleTimer()
    {
        moveToGoal = false;
        yield return new WaitForSeconds(idleTime);
        moveToGoal = true;
    }

    private IEnumerator AttackDelayTimer()
    {
        yield return new WaitForSeconds(attackTimeDelay);
        attack = true;


    }

    private void changePlayerStatis()
    {
     playerActive = !playerActive;
    }

private void OnEnable()
{
    PlayerControls.PlayerActiveUpdate += changePlayerStatis;
}


}
