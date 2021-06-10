using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigWithBomb_Script : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float playerDetectionRange;
    [SerializeField] private float followRange;
    [SerializeField] private float stopingDistance;
    private float nextAttackTime = 0f;

    public float attackRate;

    private bool e_facingRight= true;
    private bool waitBeforeFlip = false;
    private bool playerOnTop;
    private bool followPlayer = false;

    private Animator enemyAnimator;


    public Transform ledgewallDetection;
    public Transform playerDetection;
    private Transform target;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public LayerMask whoIsPlayer;

    private int maxHealth = 100;
    private int currentHealth;

    private bool b_PlayerIsHere;

    public HealthBar healthBar;

    PlayerBasic playerBasic;

    void Start()
    {
        playerBasic = FindObjectOfType<PlayerBasic>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

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


        //Check player is in range or not
        RaycastHit2D playerIsHere = Physics2D.Raycast(ledgewallDetection.position, ledgewallDetection.right, 3f, whoIsPlayer);
        Debug.DrawRay(ledgewallDetection.position, ledgewallDetection.right, Color.yellow);
        {
            if (playerIsHere.collider != null)
            {
                b_PlayerIsHere = true;
                if (Vector2.Distance(transform.position, target.position) <= followRange)
                {
                    Debug.Log("In range");

                    if (Time.time >= nextAttackTime)
                    {
                        EnemyAttack();
                        enemyAnimator.SetTrigger("Attack");
                        nextAttackTime = Time.time + 1f / attackRate;
                    }

                    
                    if (Vector2.Distance(transform.position, target.position) > stopingDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
                        Debug.Log("Player is here");
                    }
                }
            }
            else
            {
                b_PlayerIsHere = false;
            }
        }

        if (followPlayer)
        {
            if (Vector2.Distance(transform.position, target.position) > stopingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
                Debug.Log("Player is here");
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
        if(b_PlayerIsHere == false)
        {
            e_facingRight = !e_facingRight;
            transform.Rotate(0, 180, 0);
        }
        //e_facingRight = !e_facingRight;
        //transform.Rotate(0, 180, 0);
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
        //Gizmos.DrawWireSphere(playerDetection.position, playerDetectionRange);
        Gizmos.DrawWireSphere(transform.position, followRange);
    }


    //public void HammerAttackDirection()
    //{
    //    if(playerBasic.p_FacingRight == true && e_facingRight == true)
    //    {
    //        attackDirection = AttackDirection.left;
    //        Debug.Log("Attack From left");
    //    }
    //    else if (playerBasic.p_FacingRight == true && e_facingRight == false)
    //    {
    //        attackDirection = AttackDirection.left;
    //        Debug.Log("Attack From left");
    //    }
    //    else if (playerBasic.p_FacingRight == false && e_facingRight == false)
    //    {
    //        attackDirection = AttackDirection.right;
    //        Debug.Log("Attack From right");
    //    }
    //    else if (playerBasic.p_FacingRight == false && e_facingRight == true)
    //    {
    //        attackDirection = AttackDirection.right;
    //        Debug.Log("Attack From right");
    //    }
    //}

}
