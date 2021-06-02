using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool m_FacingRight = true;                  //For Flip player Left to Right or Right to left

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && isGrounded() == true)                    //Attack input when the player is grounded
        {
            PlayerAttack();
            playerAnim.Attack();
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

        rb2d.velocity = new Vector2(horizontalMove * speed, rb2d.velocity.y);       //current velocity = new velocity

        playerAnim.Move(horizontalMove);
    }

    bool isGrounded()
    {
        //check the player if is grounded with the help of Raycast 2d
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

    IEnumerator ResetJump()
    {
        //Reset the jump of player after wait foe one sec
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    public void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, radiusRange);
    }
}