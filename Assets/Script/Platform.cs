using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform detection;
    public float platformSpeed;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.down * platformSpeed * Time.deltaTime);

        RaycastHit2D downInfo = Physics2D.Raycast(detection.position, Vector2.down, 1f, layerMask);
        //Debug.DrawRay(detection.position, Vector2.down, Color.green);
        if (downInfo.collider != null)
        {
            Debug.Log("Kuch Bhe");
            platformSpeed *= -1;
        }

        RaycastHit2D upInfo = Physics2D.Raycast(detection.position, Vector2.up, 1f, layerMask);
        //Debug.DrawRay(detection.position, Vector2.up, Color.green);
        if (upInfo.collider != null)
        {
            Debug.Log("Tum Bhe");
            platformSpeed *= -1;
        }
    }
}
