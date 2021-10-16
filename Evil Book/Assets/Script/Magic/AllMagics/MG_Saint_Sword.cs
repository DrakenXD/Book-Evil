using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Saint_Sword : MagicScript 
{
    
    [SerializeField] private Collider2D col;
    float speed;

    private void Start()
    {
        col.enabled = false;
    }
    private void Update()
    {
        if (speed <= .5f) speed += 1f * Time.deltaTime;
        else
        {
            if (!col.enabled) col.enabled = true;
            speed += 4f * Time.deltaTime;
        }
            

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,-100,transform.position.z),speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            collision.gameObject.GetComponent<EnemyController>().TakeDamage(999);

            anim.SetTrigger("LightDisable");

            Destroy(gameObject);
        }

        Invoke("AnimLightDisable", 2);

        Destroy(gameObject, 3f);
    }

    private void AnimLightDisable()
    {
        anim.SetTrigger("LightDisable");
    }
}
