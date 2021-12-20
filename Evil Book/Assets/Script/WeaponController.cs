using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/new",fileName ="WeaponNew")]
public class WeaponController : ScriptableObject
{
    public Sprite S_icon;
    public GameObject effects;

    public OverriderAnimations[] overriderController;
   
}

[System.Serializable]
public class OverriderAnimations
{
    public string nameAnim;
    public AnimatorOverrideController overriderController;
}


