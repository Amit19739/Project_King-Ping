using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasic : MonoBehaviour
{
    private Rigidbody2D rb2d;                                               //Reference of rigidbody
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float speed = 2.5f;

    private bool resetJump = false;
    private bool grounded = false;
    private bool m_FacingRight = true;                  //For Flip player Left to Right or Right to left

    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;

    public Transform attackPoint;
    public Transform groundCheck;

    public float attackRange = 0.5f;
    public float radiusRange;

    public LayerMask enemyLayers;
    public LayerMask whatIsGround;
    [SerializeField] private LayerMask groundLayer;                         //Reference of Layermask to check player is grouded 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
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

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusRange, whatIsGround);
    }

    void Movement()
    {     
        float horizontalMove = Input.GetAxisRaw("Horizontal");                      // horizontal input for left/right

        grounded = isGrounded();
        
        if(horizontalMove > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && m_FacingRight)
        {
            Flip();
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)                 //Jump input and check if player is grounded or not
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
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
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

        foreach(Collider2D enemy in hitEnemies)
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
}