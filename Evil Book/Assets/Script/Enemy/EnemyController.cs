using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;

    [SerializeField] private int life;

    // Start is called before the first frame update
    void Start()
    {
        life = stats.MaxLife;
    }

    



    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0) Destroy(gameObject);
    }
}
