using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov_Test : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public Vector3 DirFromAngle(float angleInDagrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDagrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDagrees * Mathf.Deg2Rad), Mathf.Cos(angleInDagrees * Mathf.Deg2Rad), 0);
    }
}
