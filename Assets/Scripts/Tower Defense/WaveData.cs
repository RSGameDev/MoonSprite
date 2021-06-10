using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyData> enemies;
    public float spawnCooldown;

}
