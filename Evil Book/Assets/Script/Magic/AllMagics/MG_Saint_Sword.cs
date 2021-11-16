using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MG_Saint_Sword : MagicScript 
{
    
    [SerializeField] private Collider2D col;
    [SerializeField] private Collider2D stopMagicCol;
    [SerializeField] private GameObject effectDestroy;
    [SerializeField] float speed;
    [SerializeField] bool Hitenemy = false;
    [SerializeField] bool Hitground = false;

    private void Start()
    {
        col.enabled = false;
    }
    private void Update()
    {
        if (!Hitenemy)
        {
            col.enabled = true;
            if (speed <= 10f) speed += .5f * Time.deltaTime;
        }


        if (!Hitground) transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 100,transform.position.z),speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<CameraShake>().Shake(3f);

            Hitenemy = true;

            speed = 0;

            

            collision.gameObject.GetComponent<EnemyController>().TakeDamage(999);

            anim.SetTrigger("LightDisable");

            stopMagicCol.enabled = true;
        }

        if (collision.gameObject.CompareTag("Ground") && Hitenemy)
        {
            Hitground = true;

            Invoke("AnimLightDisable", 2);
            Invoke("Effect", .9f);

            Destroy(gameObject, 3f);
        }

        


      

        
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
