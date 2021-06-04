﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasic : MonoBehaviour
{                                               
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float radiusRange;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform groundChecker;

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rb2d;

    private PlayerAnimation playerAnim;

    private bool playerIsOnGrounded = false;
    private bool resetJump = false;
    private bool m_FacingRight = true;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    GameUiManager gameUiManager;

    public GameObject enemy;

    void Start()
    {
        gameUiManager = FindObjectOfType<GameUiManager>();

        rb2d = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        Movement();

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //Do attack and also play attack animation
        if (Input.GetMouseButtonDown(0) && isGrounded() == true)
        {
            PlayerAttack();
            playerAnim.Attack();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20);
        }
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");                      // horizontal input for left/right

        playerIsOnGrounded = isGrounded();

        if (horizontalMove > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && m_FacingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)                 //Jump input and check if player is grounded or not
        {
            Debug.Log("Jump");
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            StartCoroutine(ResetJump());
            playerAnim.Jump(true);
        }

        if(isGrounded() == false)
        {
            Debug.Log("IS GROUNDED FALSE");
            playerAnim.Jump(true);
        }

        rb2d.velocity = new Vector2(horizontalMove * speed, rb2d.velocity.y);       //current velocity = new velocity

        playerAnim.Move(horizontalMove);
    }

    bool isGrounded()
    {
        //check the player is on ground or not
        playerIsOnGrounded = Physics2D.OverlapCircle(groundChecker.position, radiusRange, whatIsGround);

        if (playerIsOnGrounded == true)
        {
            if (resetJump == false)
            {
                playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    //When attack key down player do attack
    public void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
            enemy.GetComponent<EnemyBasic>().TakeDamage(30);
        }

        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        playerAnim.Hit();

        if (currentHealth <= 0)
        {
            playerAnim.Death();
            gameUiManager.gameOverMenu.SetActive(false);
            this.enabled = false;
        }
    }


    IEnumerator ResetJump()
    {
        //Reset the jump of player after wait foe one sec
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    // To see selected  Gizmo in the scene view and adjust the radius or value of selected object
    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}