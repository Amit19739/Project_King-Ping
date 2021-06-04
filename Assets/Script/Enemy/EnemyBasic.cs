using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] private float enemySpeed;

    private bool facingLeft = true;

    private RaycastHit2D leftWall;
    private RaycastHit2D rightWall;

    private Animator enemyAnimator;

    public Transform colliderDetection;
    public Transform playerDetection;
    public LayerMask groundLayer;

    public float circleRange;
    public float attackRange;

    private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    public LayerMask playerLayer;

    //private bool playerOnTop;

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
        //Player move left
        transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);

        //Check for ledges
        RaycastHit2D groundInfo = Physics2D.Raycast(colliderDetection.position, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(colliderDetection.position, Vector2.down, Color.red);
        if (groundInfo.collider == null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }

        //check the player is on enemy or not if yes then take damage
        //playerOnTop = Physics2D.OverlapCircle(playerDetection.position, circleRange, playerLayer);
        //{
        //    if (playerOnTop == true)
        //    {
        //        Debug.Log("Player on top");
        //        EnemyTakeDamage(100);
        //        playerOnTop = false;
        //    }
        //}

        RaycastHit2D playerOnTop = Physics2D.Raycast(playerDetection.position, Vector2.up, 0.5f, playerLayer);
        Debug.DrawRay(playerDetection.position, Vector2.up, Color.red);
        {
            if (playerOnTop.collider != null)
            {
                Debug.Log("Player on top");
                EnemyTakeDamage(100);
            }
        }



        //check for walls
        leftWall = Physics2D.Raycast(colliderDetection.position, Vector2.left, 0.5f, groundLayer);
        if (leftWall.collider != null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }

        rightWall = Physics2D.Raycast(colliderDetection.position, Vector2.right, 0.5f, groundLayer);
        if (rightWall.collider != null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }


        //Detect player and then follow
        RaycastHit2D playerInfo = Physics2D.Raycast(colliderDetection.position, colliderDetection.right , 0.5f, playerLayer);
        Debug.DrawRay(colliderDetection.position, colliderDetection.right, Color.yellow);
        if (playerInfo.collider != null)
        {
            Debug.Log("Player Hit!!!!!");
            enemyAnimator.SetTrigger("Attack");
            EnemyAttack();
        }
    }


    void EnemyAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(colliderDetection.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("we hit " + player.name);
            player.GetComponent<PlayerBasic>().TakeDamage(20);
        }
    }

    public void Flip()
    {
        //if(facingLeft == true)
        //{
        //    transform.eulerAngles = new Vector3(0, -180, 0);
        //    facingLeft = false;
        //}
        //else
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //    facingLeft = true;
        //}

        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
            
    }



    public void EnemyTakeDamage(int damage)
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
        //Gizmos.DrawWireSphere(playerDetection.position, circleRange);
        Gizmos.DrawWireSphere(colliderDetection.position, attackRange);
    }
}
