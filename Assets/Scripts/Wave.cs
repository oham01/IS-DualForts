using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Each wave array contains entries constitue the enemy type, and count for this wave
[System.Serializable]
public class WaveEntry
{
    public GameObject enemy;
    public int count;
}

[System.Serializable]
public class Wave
{
    public List<WaveEntry> enemies = new List<WaveEntry>();
    public float rate;
}
