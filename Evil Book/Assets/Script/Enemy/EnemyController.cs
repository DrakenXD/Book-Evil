using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*  
  





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

                    PosLocal = target.position;


                    if (Vector2.Distance(transform.position, target.position) <= DistanciaParaSeguir)
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


 



            
    

  

    
    
  
    #endregion
    */


                     public EnemyStats stats;  //status do inimigo   
    [SerializeField] protected EnemyState enemystate;  //estado do inimigo  
    [SerializeField] protected int life;  //vida atualizada do inimigo
    [SerializeField] protected float speed;  //velocidade atualizada do inimigo

    [SerializeField] protected GameObject Bullet;  //gameObject de bala
    [SerializeField] protected Transform pointShot;  //posição em que o tiro vai sair

    [Header("          Effects")]
    [SerializeField] protected GameObject[] effectblood; //efeitos de sangue

    [Header("          Patrol")]
    [SerializeField] protected Transform[] pointPatrol; //pontos em que o inimigo vai patrulhar
    [SerializeField] protected int IndexPoint;  //numero dos pontos
    [SerializeField] protected float timeIdle;  //tempo total que ele vai ficar parado no local 
    protected float t_idle;  //tempo atualizado


    [Header("          Attention")]
    [SerializeField] protected Vector2 PosLocation; //ponto em que o inimigo avistou algo
    [SerializeField] protected bool somethingdetected; //detector de player
    [SerializeField] protected float timeStoplooking;  //tempo que ele vai ficar parado no local
    protected float t_stoplooking; //tempo atualizado

    [Header("          Distancias")]
    [SerializeField] protected float Observationdistance; //distancia para ver o player
    [SerializeField] protected float FollowTargetDistance; //distancia para seguir o player 
    [SerializeField] protected float AttackDistance; //distancia para atacar

    [Header("          Condições")]
    [SerializeField] protected bool IsDead;  //verificando se morreu
    [SerializeField] protected bool IsWatching;  //verificando se achou algo   
    [SerializeField] protected bool IsFollowing;  //verificando se está seguindo 
    [SerializeField] protected bool IsPatrolling;  //verificando se está em modo patrulha
    [SerializeField] protected bool IsAttacking;  //verificando se está em modo ataque


    [SerializeField] protected private GameObject G_Attention; //Imagem indicando que o bixo vai atacar

    [Header("          Follow Target")]
    [SerializeField] private Transform target;  //posição do player


    [Header("          Components Unity")]
    [SerializeField] private Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;


        life = stats.MaxLife;

        speed = stats.MaxSpeed;

        t_idle = timeIdle;
        t_stoplooking = timeStoplooking;



    }




    private void Update()
    {
        Controller();
       

        switch (enemystate)
        {
            case EnemyState.Patrol:

                Patrol();
                break;
            case EnemyState.Follow:

                FollowTarget();
                break;
            case EnemyState.SeeLocation:

                SeeLocation();
                break;
            case EnemyState.Attack:
                G_Attention.SetActive(true);
                Attack();
                break;
        }
    }

    protected void Controller()
    {
        if (IsDead)
        {

        }
        else
        {
            if (Vector2.Distance(target.position,transform.position) <= AttackDistance)
            {
                enemystate = EnemyState.Attack;
            }
            else
            {
                if (Vector2.Distance(target.position, transform.position) <= FollowTargetDistance)
                {
                    enemystate = EnemyState.Follow;
                }
                else
                {
                    if (Vector2.Distance(target.position, transform.position) <= Observationdistance)
                    {
                        enemystate = EnemyState.SeeLocation;

                        if (!IsWatching)
                        {
                            PosLocation = new Vector2(target.position.x, transform.position.y);
                            
                            IsWatching = true;
                        }
                            
                      
                    }
                    else
                    {
                        if(!IsWatching) enemystate = EnemyState.Patrol;
                    }
                }
            }
        }
    }



    protected void Patrol()
    {
        if (Vector2.Distance(transform.position, pointPatrol[IndexPoint].position) < 2f)
        {
            if (t_idle <= 0)
            {
                IndexPoint++;

                if (IndexPoint > pointPatrol.Length - 1) IndexPoint = 0;

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
            transform.position = Vector2.MoveTowards(transform.position, pointPatrol[IndexPoint].position, speed * Time.deltaTime);


            if (transform.position.x <= pointPatrol[IndexPoint].position.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (transform.position.x >= pointPatrol[IndexPoint].position.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }

            anim.SetBool("Andar", true);
        }
    }
    protected void FollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        anim.SetBool("Andar", true);

        if (transform.position.x <= target.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x >= target.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    protected void SeeLocation()
    {
        if (Vector2.Distance(transform.position, PosLocation) <= .5f)
        {
            // parado no local suspeito

            anim.SetBool("Andar", false);

            if (t_stoplooking <= 0)
            {
                IsWatching = false;
                t_stoplooking = timeStoplooking;


            }
            else
            {

                t_stoplooking -= Time.deltaTime;
            }

        }
        else
        {
            // andando até o local suspeito     

            anim.SetBool("Andar", true);

            transform.position = Vector2.MoveTowards(transform.position, PosLocation, speed * Time.deltaTime);
        }

        if (transform.position.x <= PosLocation.x)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x >= PosLocation.x)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    protected void Attack()
    {
        anim.SetBool("Andar", false);

        anim.SetBool("Atacar", true);



        if (transform.position.x <= target.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x >= target.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    public virtual void Shoot()
    {
        GameObject clone = Instantiate(Bullet, pointShot.position, Quaternion.identity);
        clone.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 5;
        clone.gameObject.GetComponent<Transform>().localEulerAngles = transform.localEulerAngles;
        clone.gameObject.GetComponent<BulletController>().damage = stats.damage;
    }




    protected enum EnemyType
    {

    }
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

        Debug.Log(life + "/" + dmg);

        FindObjectOfType<UIEnemyController>().BarLife(life, stats.MaxLife);

        var porcentagemDaVida = stats.MaxLife * ((double)MagicController.PorcenInstantKill / 100);


        FindObjectOfType<UIEnemyController>().BarVisible(true);

        if (life <= porcentagemDaVida)
        {
            FindObjectOfType<UIEnemyController>().BarMark(life, stats.MaxLife);
        }
        else
        {
            FindObjectOfType<UIEnemyController>().BarMark((float)porcentagemDaVida, 100);
        }

        EffectBlood(0);





        if (life <= 0)
        {

            FindObjectOfType<UIEnemyController>().BarVisible(false);

            IsDead = true;

            if (MagicController.getBook)
            {
                EffectBlood(1);

            }

            Destroy(gameObject);




        }


    }
    public void DisableAnimAttack()
    {
        IsAttacking = false;
        anim.SetBool("Atacar", false);

    }
    public int GetLife()
    {
        return life;
    }
    public void EffectBlood(int i)
    {
        GameObject cloneEffect = Instantiate(effectblood[i], transform.position, Quaternion.identity);
        Destroy(cloneEffect, 20f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,Observationdistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, FollowTargetDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }

}
