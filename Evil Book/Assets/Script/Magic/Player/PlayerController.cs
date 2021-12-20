using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public static bool canMove;
  


    [SerializeField] private WeaponController weaponcontroller;

    [Header("            GroundCheck")]
    [SerializeField] private bool isGrounded;
    [SerializeField] LayerMask G_layer;
    [SerializeField] private float G_Distance;
    [SerializeField] private Transform G_Position;

 
    [Header("          Components Unity")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private void Start()
    {
        SetAnimations();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
       
        AttackNormal();

        JumpController();
    }

    public void TakeDamage(int dmg)
    {
        stats.life -= dmg;

        
    }

    private void AttackNormal()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Atacar",true);
        }
    }
 
    public void AttackDisable()
    {
        anim.SetBool("Atacar", false);
    }
   
    private void JumpController()
    {
        isGrounded = Physics2D.OverlapCircle(G_Position.position,G_Distance,G_layer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }

    }
    public void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if(x != 0)
        {
            anim.SetBool("Move", true);

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



    public void SetAnimations()
    {     
        if(weaponcontroller != null)
        {
            for (int i = 0; i < weaponcontroller.overriderController.Length; i++) 
            {
                anim.runtimeAnimatorController = weaponcontroller.overriderController[i].overriderController;
            }        
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(G_Position.position, G_Distance);
    }
}


