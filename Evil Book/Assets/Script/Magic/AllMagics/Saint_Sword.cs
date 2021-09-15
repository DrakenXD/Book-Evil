using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saint_Sword : MagicScript
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("LightDisable");

            Destroy(magicObject);
        }
        Invoke("AnimLightDisable", 2);

        Destroy(magicObject,3f);
    }

    private void AnimLightDisable()
    {
        anim.SetTrigger("LightDisable");
    }
}
