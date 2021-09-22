using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    //SCRIPT PARA ATIVAR AS MAGIAS 

    public MagicStats magic;

    public void MagicAttack()
    {
        Transform EnemyPos = GameObject.FindGameObjectWithTag("Enemy").transform;

        GameObject clone = Instantiate(magic.MagicPrefab, new Vector3(EnemyPos.position.x, EnemyPos.position.y + 5, 0), Quaternion.identity);

        Destroy(clone, 10f);
    }
}
