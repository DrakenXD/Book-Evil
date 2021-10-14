using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Saint_Sword : MagicScript 
{
    

    float speed;
    private void Update()
    {
        if (speed <= 2f) speed += 1f * Time.deltaTime;
        else speed += 4f * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,-100,transform.position.z),speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var porcentagemDaVida = collision.gameObject.GetComponent<EnemyController>().stats.MaxLife * ((double) MagicController.PorcenInstantKill / 100);

            if (collision.gameObject.GetComponent<EnemyController>().GetLife() <= porcentagemDaVida)
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(999);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(stats.damage);
            }

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
