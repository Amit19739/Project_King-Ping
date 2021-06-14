using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Fov_Test))]

public class FovE_Test : Editor
{
    void OnSceneGUI()
    {
        Fov_Test fov_Test = (Fov_Test)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov_Test.transform.position, Vector3.forward, Vector3.right, 360, fov_Test.viewRadius);
        Vector3 viewAngleA = fov_Test.DirFromAngle(-fov_Test.viewAngle / 2, false);
        Vector3 viewAngleB = fov_Test.DirFromAngle(fov_Test.viewAngle / 2, false);

        Handles.DrawLine(fov_Test.transform.position, fov_Test.transform.position + viewAngleA * fov_Test.viewRadius);
        Handles.DrawLine(fov_Test.transform.position, fov_Test.transform.position + viewAngleB * fov_Test.viewRadius);
    }
}
