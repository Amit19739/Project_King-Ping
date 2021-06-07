using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public float plateformSpeed;
    public Transform startPosition,endPosition;
    public Transform startingPoint;

    Vector3 nextPosition;


    // Start is called before the first frame update
    void Start()
    {
        nextPosition = startingPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == startPosition.position)
        {
            nextPosition = endPosition.position;
        }
        if(transform.position == endPosition.position)
        {
            nextPosition = startPosition.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, plateformSpeed * Time.deltaTime);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(startPosition.position, endPosition.position);
    //}
}
