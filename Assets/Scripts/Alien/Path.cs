using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Path : MonoBehaviour
{
    public List<Transform> points;
    [SerializeField] private bool alwaysDrawPath;
    [SerializeField] private bool drawAsLoop;
    [SerializeField] private bool drawNumbers;
    public Color debugColour = Color.white;

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (alwaysDrawPath)
        {
            DrawPath();
        }
    }

    public void DrawPath()
    {
        for (int i = 0; i < points.Count; i++)
        {
            GUIStyle labelStyle = new GUIStyle
            {
                fontSize = 30,
                normal = { textColor = debugColour }
            };

            if (drawNumbers)
                Handles.Label(points[i].position, i.ToString(), labelStyle);

            // Draw Lines Between Points.
            if (i >= 1)
            {
                Gizmos.color = debugColour;
                Gizmos.DrawLine(points[i - 1].position, points[i].position);

                if (drawAsLoop)
                    Gizmos.DrawLine(points[points.Count - 1].position, points[0].position);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (alwaysDrawPath)
            return;
        else
            DrawPath();
    }
#endif
}

