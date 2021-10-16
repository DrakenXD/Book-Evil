using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] protected Transform pointAttack;
    [SerializeField] protected LayerMask A_layer;
    [SerializeField] protected float A_radius;
    [SerializeField] protected int damage;
    public void Attack()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(pointAttack.position,A_radius,A_layer);


        foreach (Collider2D hit in hitInfo)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointAttack.position,A_radius);
    }


}
