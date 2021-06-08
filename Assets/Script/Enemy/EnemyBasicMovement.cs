using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMovement : MonoBehaviour
{
    [SerializeField] enum AttackDirection {left, right};
    [SerializeField] AttackDirection attackDirection;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float playerDetectionRange;

    private bool e_facingRight= true;
    private bool waitBeforeFlip = false;
    private bool playerOnTop;

    private Animator enemyAnimator;


    public Transform ledgewallDetection;
    public Transform playerDetection;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    PlayerBasic playerBasic;

    void Start()
    {
        playerBasic = FindObjectOfType<PlayerBasic>();

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
            StartCoroutine(WaitBeforeFlip());      
        }

        //check for walls
        RaycastHit2D wallsInfo = Physics2D.Raycast(ledgewallDetection.position, ledgewallDetection.right, 0.5f, groundLayer);
        Debug.DrawRay(ledgewallDetection.position, ledgewallDetection.right, Color.blue);
        if(wallsInfo.collider != null)
        {
            enemyAnimator.SetTrigger("Idle");
            StartCoroutine(WaitBeforeFlip());
        }

        //check player on top
        playerOnTop = Physics2D.OverlapCircle(playerDetection.position, playerDetectionRange, playerLayer);
        {
            if (playerOnTop == true)
            {
                Debug.Log("Player on top");
                EnemyTakeDamage(100);
                playerOnTop = false;
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
        e_facingRight = !e_facingRight;
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
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject,1.5f);
        this.enabled = false;
    }

    public void HammerAttackDirection()
    {
        if(playerBasic.p_FacingRight == true && e_facingRight == true)
        {
            attackDirection = AttackDirection.left;
            Debug.Log("Attack From left");
        }
        else if (playerBasic.p_FacingRight == true && e_facingRight == false)
        {
            attackDirection = AttackDirection.left;
            Debug.Log("Attack From left");
        }
        else if (playerBasic.p_FacingRight == false && e_facingRight == false)
        {
            attackDirection = AttackDirection.right;
            Debug.Log("Attack From right");
        }
        else if (playerBasic.p_FacingRight == false && e_facingRight == true)
        {
            attackDirection = AttackDirection.right;
            Debug.Log("Attack From right");
        }
    }

    IEnumerator WaitBeforeFlip()
    {
        waitBeforeFlip = true;
        yield return new WaitForSeconds(1.2f);
        Flip();
        waitBeforeFlip = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ledgewallDetection.position, attackRange);
        Gizmos.DrawWireSphere(playerDetection.position, playerDetectionRange);
    }
}
