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
    private float healthPoints, manaPoints;
    private float maxHealthPoints, maxManaPoints;
    private int experience;

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

        this.healthPoints = 100.0f;
        this.manaPoints = 20.0f;
        this.maxHealthPoints = 150.0f;
        this.maxManaPoints = 50.0f;
        this.experience = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.InGame())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0))
            {
                this.Jump(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                this.Jump(true);
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
            float currentSpeed = (this.runningSpeed - 0.5f) * this.healthPoints / 100.0f;
            //this.Running();
            if (rigibody.velocity.x < currentSpeed)
            {
                rigibody.velocity = new Vector2(currentSpeed, rigibody.velocity.y);
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

    void Jump(bool isSuperJump)
    {
        if (this.IsTouchingTheGround())
        {
            if (isSuperJump && this.manaPoints >= 5)
            {
                rigibody.AddForce(Vector2.up * this.jumpForce * 1.2f, ForceMode2D.Impulse);
                this.manaPoints -= 5;
            }
            else
            {
                rigibody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
            }
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

    public void CollectHealth(float points)
    {
        this.healthPoints += points;
        if (this.healthPoints >= this.maxHealthPoints)
        {
            this.healthPoints = this.maxHealthPoints;
        }
    }

    public void CollectMana(float points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= this.maxManaPoints)
        {
            this.manaPoints = this.maxManaPoints;
        }
    }
}
