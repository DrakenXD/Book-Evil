using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saint_Sword : MagicScript
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("acertou");

            anim.SetTrigger("LightDisable");

            Destroy(gameObject, 5);

            Destroy(magicObject);
        }
    }
}
