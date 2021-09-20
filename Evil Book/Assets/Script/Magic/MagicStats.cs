using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Magic/newStats" , menuName = "MagicStats")]
public class MagicStats : ScriptableObject
{
    public string nameMagic;
    public int damage;
    public Kind_Of_Magic king_of_magic;
    public GameObject MagicPrefab;

}

public enum Kind_Of_Magic
{
    FOLLOW_TARGET,
    LINE,

}
