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
    public LayerMask groundLayer;

    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
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

        //check for walls
        leftWall = Physics2D.Raycast(colliderDetection.position, Vector2.left, 0.5f, groundLayer);
        Debug.DrawRay(colliderDetection.position, Vector2.left, Color.green);
        if (leftWall.collider != null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }

        rightWall = Physics2D.Raycast(colliderDetection.position, Vector2.right, 0.5f, groundLayer);
        Debug.DrawRay(colliderDetection.position, Vector2.right, Color.blue);
        if (rightWall.collider != null)
        {
            enemyAnimator.SetTrigger("Idle");
            Flip();
        }
    }

    public void Flip()
    {
        if(facingLeft == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingLeft = true;
        }
    }
}
