using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_Script : MonoBehaviour
{
    [SerializeField] private float enemyBaseSpeed;
    [SerializeField] private float playerDetectionRange;

    [SerializeField] private Animator enemyBaseAnim;
    [SerializeField] private Transform ledgewallDetection;

    private bool e_facingRight = true;
    private bool e_playerIsHere;

    public LayerMask whatIsGround;
    public LayerMask whoIsPlayer;

    public Transform throwPoint;
    public GameObject bombPrefab;
    public float speed;


    private float nextAttackTime = 0f;
    public float attackRate;


    void Start()
    {
        enemyBaseAnim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        EnemyBaseMove();
        PlayerInRange();
    }

    void EnemyBaseMove()
    {
        transform.Translate(Vector2.right * enemyBaseSpeed * Time.deltaTime);
        enemyBaseAnim.SetFloat("Running", enemyBaseSpeed);

        //Check for Ledge
        RaycastHit2D groundInfo = Physics2D.Raycast(ledgewallDetection.position, Vector2.down, 1f, whatIsGround);
        Debug.DrawRay(ledgewallDetection.position, Vector2.down, Color.green);
        if (groundInfo.collider == null)
        {
            Flip();
        }

        //check for walls
        RaycastHit2D wallsInfo = Physics2D.Raycast(ledgewallDetection.position, ledgewallDetection.right, 0.5f, whatIsGround);
        Debug.DrawRay(ledgewallDetection.position, ledgewallDetection.right, Color.red);
        if (wallsInfo.collider != null)
        {
            Flip();
        }
    }

    void PlayerInRange()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, playerDetectionRange, whoIsPlayer);

        foreach (Collider2D player in hitPlayer)
        {

            Debug.Log("Hey " + player.name);

            e_playerIsHere = true;
            if (e_playerIsHere)
            {
                enemyBaseSpeed = 0;
            }
            else
            {
                enemyBaseSpeed = 1.5f;
            }


            //if (Time.time >= nextAttackTime)
            //{
            //    Debug.Log("Now attack Time");
            //    Attack();
            //    nextAttackTime = Time.time + 1f / attackRate;
            //}
        }
    }

    public void Attack()
    {
        Debug.Log("Attack!!!!!!!!!!!");
        GameObject newbomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
        newbomb.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        enemyBaseAnim.SetTrigger("Attack");
    }

    public void Flip()
    {
        e_facingRight = !e_facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }
}
