using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public int health;
    public Sprite sprite;
    public float size;

    public Sprite[] sprites;
}
