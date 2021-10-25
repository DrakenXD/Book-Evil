using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats stats;
    [SerializeField] protected EnemyState enemystate;
    [SerializeField] protected int life;
    [SerializeField] protected float speed;

    [SerializeField] protected GameObject Bullet;
    [SerializeField] protected Transform pointShot;

    [Header("          Effects")]
    [SerializeField] protected GameObject[] effectblood;

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

        




        G_Atencao.SetActive(false);

        target = GameObject.FindGameObjectWithTag("Player").transform;
        
       
        life = stats.MaxLife;

        speed = stats.MaxSpeed;

        t_idle = timeIdle;
        t_stoplooking = timeStoplooking;

        

    }

    private void Update()
    {
       
        Condicoes();

        switch (enemystate)
        {
            case EnemyState.Patrol:
                G_Atencao.SetActive(false);
                Patrulhar();
                break;
            case EnemyState.Follow:
                G_Atencao.SetActive(false);
                SeguirAlvo();          
                break;
            case EnemyState.SeeLocation:
                G_Atencao.SetActive(true);
                VerLocal();
                break;
            case EnemyState.Attack:
                G_Atencao.SetActive(false);
                Atacar();
                break;
        }
    }

    protected virtual void SeguirAlvo()
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
    protected virtual void Atacar()
    {
        anim.SetBool("Atacar", true);
        transform.position = new Vector2(transform.position.x,transform.position.y);

        if (transform.position.x <= target.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x >= target.position.x)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    public virtual void LancarBala()
    {
        GameObject clone = Instantiate(Bullet,pointShot.position,Quaternion.identity);
        clone.gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * 5;
        clone.gameObject.GetComponent<BulletController>().damage = stats.damage;
    }

    protected virtual void VerLocal()
    {
        
        if (Vector2.Distance(transform.position, PosLocal) <= 2f)
        {
            // parado no local suspeito

       

            anim.SetBool("Andar", false);

            if (t_stoplooking <= 0)
            {
                Observando = false;
                t_stoplooking = timeStoplooking;


            }
            else {
                
                t_stoplooking -= Time.deltaTime; 
            }

        }
        else
        {
            // andando até o local suspeito

           

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

        
    }
    protected virtual void Patrulhar()
    {
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

    public void DisableAnimAttack()
    {
        Atacando = false;
        anim.SetBool("Atacar", false);

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

        Debug.Log(life+"/" + dmg);

        FindObjectOfType<UIEnemyController>().BarLife(life, stats.MaxLife);
       
        var porcentagemDaVida = stats.MaxLife * ((double)MagicController.PorcenInstantKill / 100);


        FindObjectOfType<UIEnemyController>().BarVisible(true);

        if (life <= porcentagemDaVida)
        {
            FindObjectOfType<UIEnemyController>().BarMark(life, stats.MaxLife);
        }
        else
        {
            FindObjectOfType<UIEnemyController>().BarMark((float)porcentagemDaVida,100);
        }

        EffectBlood(0);


       


        if (life <= 0)
        {

            FindObjectOfType<UIEnemyController>().BarVisible(false);

            Morto = true;

            if (MagicController.getBook)
            {
                anim.SetBool("SaintSword", true);
                Destroy(gameObject, .5f);
            }
            else
            {
                Destroy(gameObject);
            }

            EffectBlood(1);

           
        }

            
    }

    public void EffectBlood(int i)
    {
        GameObject cloneEffect = Instantiate(effectblood[i], transform.position, Quaternion.identity);
        Destroy(cloneEffect, 20f);
    }

    public int GetLife()
    {
        return life;
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
