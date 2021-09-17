using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public static bool canMove;
    public static bool getBook;


    [SerializeField] private WeaponController weaponcontroller;
    [SerializeField] private Transform pointEffect;
    [Header("          Time To Get Book")]
    [SerializeField] private float SetTimeBook=1f;
    private float GetTimeBook; 

    [Header("          Components Unity")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    

    // Update is called once per frame
    void Update()
    {
        Movement();
        Magics();
        AttackNormal();
    }

    private void AttackNormal()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            anim.SetTrigger("Atacar");
            
        }
    }
    public void ActivateEffect()
    {
        GameObject clone = Instantiate(weaponcontroller.effects, pointEffect.position, pointEffect.rotation);
        Destroy(clone, .3f);
    }

    public void Magics()
    {
        if (GetTimeBook <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                GetTimeBook = SetTimeBook;

                if (!getBook)
                {
                    ActivateTransformCat();
                }
                else if (getBook)
                {
                    getBook = false;
                    anim.SetBool("Livro", false);
                    FindObjectOfType<CatController>().TransformCatAnimDisable();
                }
            }
        }
        else if(GetTimeBook > 0) GetTimeBook -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && getBook)
        {
            anim.SetBool("Magic1", true);
        }
    }

    private void ActivateTransformCat()
    {
        FindObjectOfType<CatController>().TransformCatAnimActivate();
    }
    public void GetBook()
    {
        getBook = true;
        
    }
    public void GetBookAnimActivate()
    {
        anim.SetBool("Livro", true);
    }
    public void Magia1Active()
    {
        FindObjectOfType<MagicController>().MagicAttack();
    }
    public void Magia1Disable()
    {
        anim.SetBool("Magic1", false);
    }
    public void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if(x != 0)
        {
            anim.SetBool("Move",true);

            if (x <= -0.01)
            {
                transform.localEulerAngles = new Vector3(0,180,0);
            }else if (x >= 0.01)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            anim.SetBool("Move", false);


        }


        rb.velocity = new Vector2(x * stats.speed, rb.velocity.y);
    }


}


