using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public GameObject magicObject;

    public void MagicAttack()
    {
        Transform EnemyPos = GameObject.FindGameObjectWithTag("Enemy").transform;

        Instantiate(magicObject, new Vector3(EnemyPos.position.x, EnemyPos.position.y + 5, 1), Quaternion.identity);
        
    }
}
