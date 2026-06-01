using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="itemscriptable")]
public class items : ScriptableObject
{
    public string itemname;
    public enum WeaponType
    {
        None, sword, bow,
    }
    public float attackDamage;
    public float defencepoint;
    public float otherfloat;
    public float otherint;
}

