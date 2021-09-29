using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected EnemyStats stats;
    [SerializeField] protected EnemyState enemystate;
    [SerializeField] protected int life;
    [SerializeField] protected float speed;



    [Header("          Patrol")]
    [SerializeField] protected Transform[] PontoParaAndar;
    [SerializeField] protected int NumPonto;
    [SerializeField] protected float timeIdle;
    protected float t_idle;

    [Header("          Attetion")]
    [SerializeField] protected private GameObject G_Atencao;
    [SerializeField] protected Vector2 PosLocal;  
    [SerializeField] protected bool somethingdetected;
    [SerializeField] protected float timeStoplooking;
    protected float t_stoplooking;

    [Header("          Distancias")] 
    [SerializeField] protected float DistanciaDeObservacao;
    [SerializeField] protected float DistanciaParaSeguir;
    [SerializeField] protected float DistanciaParaAtacar;

    [Header("          Condições")]
    [SerializeField] protected bool Morto;
    [SerializeField] protected bool Observando;
    [SerializeField] protected bool Seguindo;
    [SerializeField] protected bool patrulhar;
    [SerializeField] protected bool Atacando;


    [Header("          Follow Target")]
    [SerializeField] private Transform target;
   

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
        Condicoes();

        switch (enemystate)
        {
            case EnemyState.Patrol:
                if (Vector2.Distance(transform.position, PontoParaAndar[NumPonto].position) < 2f)
                {
                    if (t_idle <= 0)
                    {
                        NumPonto++;

                        if (NumPonto > PontoParaAndar.Length - 1) NumPonto = 0;

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
                    transform.position = Vector2.MoveTowards(transform.position, PontoParaAndar[NumPonto].position, speed * Time.deltaTime);


                    if (transform.position.x <= PontoParaAndar[NumPonto].position.x)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (transform.position.x >= PontoParaAndar[NumPonto].position.x)
                    {
                        transform.localEulerAngles = new Vector3(0, 180, 0);
                    }

                    anim.SetBool("Andar", true);
                }
                break;
            case EnemyState.Follow:

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                if (transform.position.x <= target.position.x)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.position.x >= target.position.x)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }

                break;
            case EnemyState.SeeLocation:
                if (Vector2.Distance(transform.position, PosLocal) <= 2f)
                {
                    anim.SetBool("Andar", false);

                    if (t_stoplooking <= 0)
                    {
                        Observando = false;
                    }
                    else t_stoplooking -= Time.deltaTime;
                }
                else
                {
                    anim.SetBool("Andar", true);

                    transform.position = Vector2.MoveTowards(transform.position, PosLocal, speed * Time.deltaTime);
                }



                

                if (transform.position.x <= PosLocal.x)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.position.x >= PosLocal.x)
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }

                anim.SetBool("Andar", true);

                break;
            case EnemyState.Attack:
                anim.SetBool("Atacar", true);
                break;
        }
    }

    private void Condicoes()
    {
        if (Morto)
        {

        }
        else
        {
            if (Vector2.Distance(transform.position, target.position) <= DistanciaParaAtacar)
            {
                Atacando = true;

                Observando = false;

                

                enemystate = EnemyState.Attack;
            }
            else
            {
                if (Vector2.Distance(transform.position, target.position) <= DistanciaDeObservacao && 
                    Vector2.Distance(transform.position, target.position) >= DistanciaParaAtacar)
                {
                    Observando = true;

                    patrulhar = false;

                    enemystate = EnemyState.SeeLocation;

                    if (PosLocal.x == 0) PosLocal = target.position;


                    if (Vector2.Distance(transform.position, target.position) >= DistanciaParaAtacar)
                    {
                        Seguindo = true;

                        if (PosLocal.x != 0) PosLocal = new Vector2(0,0);

                        enemystate = EnemyState.Follow;
                    }
                    else
                    {
                        Seguindo = false;
                       
                        
                    }
                }
                else
                {
                  
                    if (!Observando)
                    {
                        if (PosLocal.x != 0) PosLocal = new Vector2(0, 0);

                        enemystate = EnemyState.Patrol;

                        

                        Seguindo = false;

                        patrulhar = true;
                    }
              
                }

              
             
            }

            





        }


    }

    public void DisableAttack()
    {
        Atacando = false;
        anim.SetBool("Atacar", false);

    }

    /*
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
    */

    protected enum EnemyState
    {
        Patrol,
        Follow,
        SeeLocation,
        Attack,
    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        if (life <= 0) Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DistanciaDeObservacao);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanciaParaSeguir);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanciaParaAtacar);
    }
}
