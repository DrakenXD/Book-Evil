using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    [SerializeField] protected MagicStats stats;
    [SerializeField] protected Kind_Of_Magic kind_of_magic;

    [Header("         ComponentsUnity")]
    [SerializeField] protected Animator anim;

    public virtual void CreateMagic(Vector3 NewEnemyPos)
    {
        //infomarções
    }

}
