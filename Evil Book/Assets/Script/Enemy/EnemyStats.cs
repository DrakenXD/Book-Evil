using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/NewStats", fileName ="NewStats")]
public class EnemyStats : ScriptableObject
{
    public int MaxLife;
    public int MaxDamage;
    public int damage;
    public float MaxSpeed;
    
}
