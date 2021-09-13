using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public static bool canMove;

    [Header("          Components Unity")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    

    // Update is called once per frame
    void Update()
    {
        Movement();
        Magics();
    }


    public void Magics()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Magic1", true);
        
        }
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


