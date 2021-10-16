using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float speed;

    [SerializeField] private float SetTimeSpeed=1;
    private float GetTimeSpeed;

    [SerializeField] private float distanceToPlayer;
    [SerializeField] private bool transformCat;

    private Transform playerPos;

    [Header("          Components Unity")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    private void Controller()
    {
        float pos = Vector2.Distance(transform.position,playerPos.position);
        if(pos >= distanceToPlayer)
        {
            anim.SetBool("Deitando", false);
            anim.SetBool("Deitado", false);
            anim.SetBool("Andar", true);

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

            if (transform.position.x >= playerPos.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else if (transform.position.x <= playerPos.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }


        }
        else
        {
            anim.SetBool("Andar", false);
            anim.SetBool("Deitando", true);
        }


        if (transformCat)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

            if (GetTimeSpeed <= 0)
            {
                speed += 2;

                GetTimeSpeed = SetTimeSpeed;
            }
            else GetTimeSpeed -= Time.deltaTime;

            if (transform.position.x >= playerPos.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else if (transform.position.x <= playerPos.position.x)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
       
    }

    public void TransformCatAnimActivate()
    {
        transformCat=true;

        anim.SetBool("Transformar", true);
    }

    public void TransformCatAnimDisable()
    {
        sprite.enabled = true;

        transformCat = false;

        anim.SetBool("Transformar", false);
    }

    public void DeitarAtivarAnim()
    {
        anim.SetBool("Deitado", true);
    }

    public void PlayerGetBook()
    {
        FindObjectOfType<MagicController>().GetBook();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && transformCat && MagicController.getBook)
        {
            sprite.enabled = false;
            FindObjectOfType<MagicController>().GetBookAnimActivate();
            speed = MaxSpeed;
        }
    }

}
