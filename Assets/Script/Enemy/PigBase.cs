using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBase : MonoBehaviour
{
    [SerializeField] private float pigSpeed;

    private bool movingLeft = true;

    private RaycastHit2D leftWall;
    private RaycastHit2D rightWall;

    public Transform colliderDetection;
    public LayerMask groundLayer;

    void Start()
    {
        
    }

    void Update()
    {
        PigMovement(); 
    }

    void PigMovement()
    {
        //Player move left
        transform.Translate(Vector2.left * pigSpeed * Time.deltaTime);

        //Check for ledges
        RaycastHit2D groundInfo = Physics2D.Raycast(colliderDetection.position, Vector2.down, 1f, groundLayer.value);
        Debug.DrawRay(colliderDetection.position, Vector2.down, Color.red);
        if (groundInfo.collider == null)
        {
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }

        //Player Detected wall collider if not detect move opposite direction
        //if(groundInfo.collider != null && movingLeft == true)
        //{
        //    leftWall = Physics2D.Raycast(new Vector2(colliderDetection.position.x, colliderDetection.position.y), Vector2.left, 0.5f);
        //    Debug.DrawRay(colliderDetection.position, Vector2.left, Color.red);
        //    if (leftWall.collider != null)
        //    {
        //        //transform.eulerAngles = new Vector3(0, -180, 0);
        //        movingLeft = false;
        //    }
        //}
        //else
        //{
        //    rightWall = Physics2D.Raycast(new Vector2(colliderDetection.position.x, colliderDetection.position.y), Vector2.right, 0.5f);
        //    Debug.DrawRay(colliderDetection.position, Vector2.right, Color.red);
        //    if (rightWall.collider != null)
        //    {
        //        //transform.eulerAngles = new Vector3(0, 0, 0);
        //        movingLeft = true;
        //    }
        //}
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
       movingLeft = !movingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
