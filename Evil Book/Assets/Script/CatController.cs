using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceToPlayer;

    private Transform playerPos;

    [Header("          Components Unity")]
    [SerializeField] private Animator anim;
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

       
    }


    public void DeitarAtivarAnim()
    {
        anim.SetBool("Deitado", true);
    }
   
}
