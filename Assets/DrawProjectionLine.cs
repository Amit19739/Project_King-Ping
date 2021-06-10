using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjectionLine : MonoBehaviour
{
    Cannon_Fire cannon_Fire;
    LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 50;

    // distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;

    void Start()
    {
        cannon_Fire = GetComponent<Cannon_Fire>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.positionCount = (int)numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector2 startingPosition = cannon_Fire.firePoint.position;
        Vector2 startingVelocity = cannon_Fire.firePoint.right * cannon_Fire.speed;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }

        lineRenderer.SetPositions(points.ToArray());
    }
}
