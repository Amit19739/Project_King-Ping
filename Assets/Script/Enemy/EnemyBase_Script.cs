using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_Script : MonoBehaviour
{
    [SerializeField] private float enemyBaseSpeed;
    [SerializeField] private float waitTimeBeforeFlip;

    [SerializeField] private Animator enemyBaseAnim;

    private bool e_facingRight = true;
    private bool waitBeforeFlip = false;

    public Transform ledgewallDetection;

    public LayerMask whatIsGround;



    void Start()
    {
        enemyBaseAnim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (enemyBaseAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        EnemyBaseMove();
    }

    void EnemyBaseMove()
    {
        transform.Translate(Vector2.right * enemyBaseSpeed * Time.deltaTime);


        RaycastHit2D groundInfo = Physics2D.Raycast(ledgewallDetection.position, Vector2.down, 1f, whatIsGround);
        Debug.DrawRay(ledgewallDetection.position, Vector2.down, Color.green);
        if (groundInfo.collider == null)
        {
            Debug.Log("Ground Null");
            enemyBaseAnim.SetTrigger("Idle");
            StartCoroutine(WaitBeforeFlip());
        }

        //check for walls
        RaycastHit2D wallsInfo = Physics2D.Raycast(ledgewallDetection.position, ledgewallDetection.right, 0.5f, whatIsGround);
        Debug.DrawRay(ledgewallDetection.position, ledgewallDetection.right, Color.red);
        if (wallsInfo.collider != null)
        {
            Debug.Log("Wall Is Here");
            enemyBaseAnim.SetTrigger("Idle");
            StartCoroutine(WaitBeforeFlip());
        }
    }

    public void Flip()
    {
        e_facingRight = !e_facingRight;
        transform.Rotate(0, 180, 0);
    }

    IEnumerator WaitBeforeFlip()
    {
        waitBeforeFlip = true;
        yield return new WaitForSeconds(waitTimeBeforeFlip);
        Flip();
        waitBeforeFlip = false;
    }
}
