using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;
    public float jumpForce = 60f;
    public float runningSpeed = 1.5f;
    public LayerMask groundLayer;//Detectar la capa del suelo
    private Rigidbody2D rigibody;
    public Animator animator;
    private Vector3 startPosition;

    void Awake()
    {
        sharedInstance = this;
        rigibody = GetComponent<Rigidbody2D>();
        this.startPosition = this.transform.position;

    }
    // Start is called before the first frame update
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        this.transform.position = this.startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.InGame())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0))
            {
                this.Jump();
            }
            animator.SetBool("isGrounded", this.IsTouchingTheGround());
        }
    }
    bool InGame()
    {
        return GameManager.sharedInstance.currentGameState == GameState.inGame;
    }
    void FixedUpdate()
    {
        if (this.InGame())
        {
            //this.Running();
            if (rigibody.velocity.x < this.runningSpeed)
            {
                rigibody.velocity = new Vector2(this.runningSpeed, rigibody.velocity.y);
            }
        }
    }

    private void Running()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (rigibody.velocity.x < this.runningSpeed)
            {
                rigibody.velocity = new Vector2(this.runningSpeed, rigibody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (rigibody.velocity.x > -this.runningSpeed)
            {
                rigibody.velocity = new Vector2(-this.runningSpeed, rigibody.velocity.y);
            }
        }
    }

    void Jump()
    {
        if (this.IsTouchingTheGround())
        {
            rigibody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
    }

    //Trazar una linea debajo del jugador para saber si esta tocando el suelo    
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer))
        {
            return true;
        }
        return false;
    }
    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);

        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0.0f);
        if (currentMaxScore < this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxscore", this.GetDistance());
        }
    }

    public float GetDistance()
    {
        // Optional -> this.transform.position.x - this.startPosition.x;
        return Vector2.Distance(new Vector2(this.startPosition.x, 0), new Vector2(this.transform.position.x, 0));
    }
}
