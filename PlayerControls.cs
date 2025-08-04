/*Platformer - PlayerControls.cs
 * Version 2.0 by Micah Disney
 * 
 * Description:Script used to implement movement actions and animations.
 * 
 *
 * */

using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rigidLink;
    Vector2 moveInput;
    float moveInputX;
    bool Sprinting;
    float velocityX;
    [SerializeField] float speedModifier = 100;
    [SerializeField] float defaultSpeedModifier = 100;
    Animator animatorLink;
    SpriteRenderer renderLink;
    float velocityY;
    public bool isGrounded = true;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject spawnLocation;
    [SerializeField] AudioClip fallSound;
    AudioSource audioLink;
    Camera cameraLink;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip attackSound2;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip sprintSound;
    [SerializeField] AudioClip walkSound;
    [SerializeField] GameObject flipperLink;

    CapsuleCollider2D capsuleLink;
    PlayerInput inputLink;
    [SerializeField] AudioClip dieSound;
    [SerializeField] float respawnDelay;

    public static event Action PlayerActiveUpdate;

    public int playerLives = 3;
    [SerializeField] AudioClip powerUpSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip WinSound;

    HUD hudLink;
   public bool Mute;

    void Start()
    {
        Application.targetFrameRate = 60;
        rigidLink = GetComponent<Rigidbody2D>();
        animatorLink = GetComponent<Animator>();
        renderLink = GetComponent<SpriteRenderer>();
        audioLink = GetComponent<AudioSource>();
        cameraLink = Camera.main;

        capsuleLink = GetComponent<CapsuleCollider2D>();
        inputLink = GetComponent<PlayerInput>();

        hudLink = FindObjectOfType<HUD>();

        Mute = false;

    }

 
    void Update()
    {
        MoveSideWays();
        animatorLink.SetBool("Grounded", isGrounded);
    }

    private void MoveSideWays()
    {
        velocityX = moveInputX * Time.deltaTime * speedModifier;
        velocityY = rigidLink.velocity.y;
        rigidLink.velocity = new Vector2(velocityX, velocityY);
        animatorLink.SetFloat("Speed", Mathf.Abs(velocityX));
        

        if (velocityX < 0)
        {
            renderLink.flipX = true;
            flipperLink.transform.localScale = new Vector2(-1, 1);
        }
        else if (velocityX > 0)
        {
            renderLink.flipX = false;
            flipperLink.transform.localScale = new Vector2(1, 1);
        }

        else if (velocityX < 0.1)
        {
            animatorLink.SetBool("Sprinting", false);
            speedModifier = defaultSpeedModifier; 
        }

    }

    public void OnMove(InputValue value)
    {
        moveInput = (value.Get<Vector2>());
        moveInputX = moveInput.x;
        
    }
    public void OnSprint(InputValue value)
    {
        if (isGrounded)
        {
            if (velocityX != 0)
            {
                animatorLink.SetBool("Sprinting", true);
                speedModifier = (velocityX + 750);
            }
            else 
            {
                animatorLink.SetBool("Sprinting", false);
                speedModifier = (velocityX + 300);
            }
        }
    }
    public void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rigidLink.AddForce(new Vector2(0, jumpForce));
        }
    }



    public void OnMelee_Attack()
    {
        animatorLink.SetTrigger("Attack");
    }

    public void AttackSound()
    {
        audioLink.PlayOneShot(attackSound);
    }
    public void SprintSound()
    {
        audioLink.PlayOneShot(sprintSound);
    }
    public void WalkSound()
    {
        audioLink.PlayOneShot(walkSound);
    }

    public void AttackSound2()
    {
        audioLink.PlayOneShot(attackSound2);
    }

    public void ReloadSound()
    {
        audioLink.PlayOneShot(reloadSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitTag = collision.gameObject.tag;

        if (hitTag == "Bottom")
        {
            cameraLink.transform.parent = null;
            audioLink.PlayOneShot(fallSound);
            DecreaseLives();
            capsuleLink.enabled = false;
        }

        else if (hitTag == "EnemyAttack")
        {
            PlayerDeath();
            DecreaseLives();
        }

        else if (hitTag == "CheckPoint")
        {
            spawnLocation = collision.gameObject;
        }
        else if (hitTag == "PowerUp")
        {
            IncreaseLives();
            Destroy(collision.gameObject);
        }

        else if (hitTag == "Exit")
        {
            GameWon();
            Mute = true;
        }
    }

    private IEnumerator RespawnTimer()
    {

    yield return new WaitForSeconds(respawnDelay);
        transform.position = spawnLocation.transform.position;
        cameraLink.transform.parent = transform;
        cameraLink.transform.position = transform.position;
        cameraLink.transform.Translate(0, 0, -10);

        rigidLink.velocity = new Vector2(0, 0);
        rigidLink.gravityScale = 1;
        capsuleLink.enabled = true;
        animatorLink.Play("Idle");
        inputLink.enabled = true;



      PlayerActiveUpdate();

}

private void PlayerDeath()
    {
        inputLink.enabled = false;
        capsuleLink.enabled = false;
        rigidLink.gravityScale = 0;
        rigidLink.velocity = new Vector2(0, 0);
        animatorLink.SetTrigger("Die");
        audioLink.PlayOneShot(dieSound);
       


    }

    private void DecreaseLives()
    {

        playerLives--;

        PlayerActiveUpdate();

        if(playerLives > 0)
        {
            StartCoroutine(RespawnTimer());
        }

        else
        {
            GameLost();
        }

        hudLink.RemoveIcon();
    }

    private void GameLost()
    {
        Debug.Log("Game Lost");
        audioLink.PlayOneShot(loseSound);
        hudLink.GameOver();
        Mute = true;
    }

    private void GameWon()
    {
        Debug.Log("Game Won");
        audioLink.PlayOneShot(WinSound);
        inputLink.enabled = false;
        hudLink.GameOver();
    }

    private void IncreaseLives()
    {
        playerLives++;
        audioLink.PlayOneShot(powerUpSound);
       hudLink.AddIcon();
    }


}
