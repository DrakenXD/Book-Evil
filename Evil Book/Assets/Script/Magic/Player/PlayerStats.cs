using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Stats" , menuName = "Player/newStats")]
public class PlayerStats : ScriptableObject
{
    public int Maxlife;
    public int life;
    public float Maxspeed;
    public float speed;
    public float Jump;
    public int Maxdamage;
    public int damage;

    private void OnEnable()
    {
        life = Maxlife;
        speed = Maxspeed;
        damage = Maxdamage;
    }
}
