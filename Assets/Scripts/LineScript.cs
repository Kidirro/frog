using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    private static LineRenderer _lineStat;
    private void Awake()
    {
        _lineStat = GetComponent<LineRenderer>();
    }

    static public void ShowTrajectory(Vector3 origin, Vector3 end,float speed)
    {
        Vector3[] points = new Vector3[2];
        points[0] = origin;
        points[1] = end;
        _lineStat.positionCount = 2;
        _lineStat.SetPositions(points);
    }

    private void OnDisable()
    {
        _lineStat.positionCount = 0;
    }
}
