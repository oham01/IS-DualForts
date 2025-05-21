using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] waypoints;

    // Populate the waypoints array with the transforms of each waypoint
    void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for(int i = 0; i < waypoints.Length ; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
