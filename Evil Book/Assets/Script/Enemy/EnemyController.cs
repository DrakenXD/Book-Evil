using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    [SerializeField] private EnemyState enemystate;
    [SerializeField] private int life;
    [SerializeField] private float speed;

    [Header("          Patrol")]
    [SerializeField] private Transform[] PointPatrol;
    [SerializeField] private int indexPoint;
    [SerializeField] private float timeIdle;
    private float t_idle;

    [Header("          Components Unity")]
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        life = stats.MaxLife;
        speed = stats.MaxSpeed;

        t_idle = timeIdle;
    }

    private void Update()
    {
        switch (enemystate)
        {
            case EnemyState.Patrol:
                if (Vector2.Distance(transform.position, PointPatrol[indexPoint].position) < 2f)
                {
                    if (t_idle <= 0)
                    {
                        indexPoint++;

                        if (indexPoint > PointPatrol.Length -1) indexPoint = 0;

                        t_idle = timeIdle;

                        if (transform.position.x <= PointPatrol[indexPoint].position.x)
                        {
                            transform.localEulerAngles = new Vector3(0, 0, 0);
                        }else if (transform.position.x >= PointPatrol[indexPoint].position.x)
                        {
                            transform.localEulerAngles = new Vector3(0, 180, 0);
                        }
                     
                    }
                    else
                    {
                        

                        anim.SetBool("Andar", false);

                        t_idle -= Time.deltaTime;
                    } 
                        
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, PointPatrol[indexPoint].position, speed * Time.deltaTime);

                    anim.SetBool("Andar", true);
                }
                break;
            case EnemyState.Follow:

                break;
        }
    }

    private enum EnemyState
    {
        Patrol,
        Follow,
    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0) Destroy(gameObject);
    }
}
