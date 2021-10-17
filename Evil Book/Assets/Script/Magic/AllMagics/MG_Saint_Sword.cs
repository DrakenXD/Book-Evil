using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Saint_Sword : MagicScript 
{
    
    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject effectDestroy;
    [SerializeField] float speed;
    bool Hitenemy = false;

    private void Start()
    {
        col.enabled = false;
    }
    private void Update()
    {
        if (speed <= .1f) speed += .3f * Time.deltaTime;
        else
        {
            if (!Hitenemy)
            {
                col.enabled = true;
                if (speed <= 1f) speed += .6f * Time.deltaTime;
            }
        }
            

        if(!Hitenemy)transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,-100,transform.position.z),speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            

            Hitenemy = true;

            speed = 0;

            transform.position = Vector3.MoveTowards(transform.position, transform.position, speed);

            collision.gameObject.GetComponent<EnemyController>().TakeDamage(999);

            anim.SetTrigger("LightDisable");

          
        }

        Invoke("AnimLightDisable", 2);
        Invoke("Effect",1);


        Destroy(gameObject, 3f);

        col.enabled = false;
    }

    private void Effect()
    {
        GameObject cloneEffect = Instantiate(effectDestroy, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(cloneEffect, 3);
    }

    private void AnimLightDisable()
    {
       

        anim.SetTrigger("LightDisable");
    }
}
