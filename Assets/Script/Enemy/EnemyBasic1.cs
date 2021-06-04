using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic1 : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private float attackRange;

    private bool facingRight= true;

    private Animator enemyAnimator;

    public Transform ledgewallDetection;
    public Transform playerDetection;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        EnemyMovement();
    }

    void EnemyMovement()
    {
        //Enemy Move Right
        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);

        //Check for ledges
        RaycastHit2D groundInfo = Physics2D.Raycast(ledgewallDetection.position, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(ledgewallDetection.position, Vector2.down, Color.green);
        if (groundInfo.collider == null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }

        //check for walls
        RaycastHit2D wallsInfo = Physics2D.Raycast(ledgewallDetection.position, ledgewallDetection.right, 0.5f, groundLayer);
        Debug.DrawRay(ledgewallDetection.position, ledgewallDetection.right, Color.blue);
        if(wallsInfo.collider != null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }

        //check player on top
        RaycastHit2D playerOnTop = Physics2D.Raycast(playerDetection.position, Vector2.up, 0.5f, playerLayer);
        Debug.DrawRay(playerDetection.position, Vector2.up, Color.red);
        {
            if (playerOnTop.collider != null)
            {
                Debug.Log("Player on top");
                EnemyTakeDamage1(100);
            }
        }
    }


    void EnemyAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(ledgewallDetection.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("we hit " + player.name);
            player.GetComponent<PlayerBasic>().TakeDamage(20);
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
            
    }

    public void EnemyTakeDamage1(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        enemyAnimator.SetTrigger("Hit");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemey Died");
        enemyAnimator.SetBool("IsDead", true);
        Destroy(gameObject,1f);
        this.enabled = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ledgewallDetection.position, attackRange);
    }
}
