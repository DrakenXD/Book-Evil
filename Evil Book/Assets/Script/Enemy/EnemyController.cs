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

    [Header("          Attetion")]
    [SerializeField] private GameObject Attetion;
    [SerializeField] private Vector2 local;
    [SerializeField] private float DistanceAttetion;
    [SerializeField] private bool somethingdetected;
    [SerializeField] private float timeStoplooking;
    private float t_stoplooking;


    [Header("          Follow Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float DistanceFollowTarget;

    [Header("          Components Unity")]
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        life = stats.MaxLife;
        speed = stats.MaxSpeed;

        t_idle = timeIdle;

    }

    private void Update()
    {

        Conditions();









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


                    if (transform.position.x <= PointPatrol[indexPoint].position.x)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (transform.position.x >= PointPatrol[indexPoint].position.x)
                    {
                        transform.localEulerAngles = new Vector3(0, 180, 0);
                    }

                    anim.SetBool("Andar", true);
                }
                break;
            case EnemyState.Follow:

                if (Vector2.Distance(transform.position, target.position) < DistanceFollowTarget)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
               
                    if (transform.position.x <= target.position.x)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (transform.position.x >= target.position.x)
                    {
                        transform.localEulerAngles = new Vector3(0, 180, 0);
                    }
                }

                break;
            case EnemyState.SeeLocation:

                transform.position = Vector2.MoveTowards(transform.position, local, speed * Time.deltaTime);

                if (transform.position.x <= local.x)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.position.x >= local.x)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }

                anim.SetBool("Andar", true);



                break;
        }
    }

    private void Conditions()
    {
        if (Vector2.Distance(transform.position, target.position) < DistanceAttetion && Vector2.Distance(transform.position, target.position) > DistanceFollowTarget)
        {
            t_stoplooking = timeStoplooking;

            if (local.x == 0)
            {
                local = target.position;
            }

            enemystate = EnemyState.SeeLocation;

            Attetion.SetActive(true);

            somethingdetected = true;
        }
        else
        {

            if (!somethingdetected)
            {
                if (Vector2.Distance(transform.position, target.position) < DistanceFollowTarget)
                {
                    enemystate = EnemyState.Follow;

                }
                else
                {
                    enemystate = EnemyState.Patrol;

                }
            }
            else
            {
                if (Vector2.Distance(transform.position, local) < 1f)
                {
                    if (t_stoplooking <= 0)
                    {
                        Attetion.SetActive(false);

                        somethingdetected = false;

                        anim.SetBool("Andar", false);

                        local = new Vector2(0,0);

                        t_stoplooking = timeStoplooking;
                    }
                    else t_stoplooking -= Time.deltaTime;
                }
            }
           
        }


       
    }

    private enum EnemyState
    {
        Patrol,
        Follow,
        SeeLocation,
    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0) Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DistanceAttetion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceFollowTarget);
    }
}
