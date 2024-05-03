using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Scriptable Objects/Ability Data")]
public class AbilityData : ScriptableObject
{
    public float cooldown;
    public float lifeTime;
    public float speed;
    public float dmg;
    public float ammo;
    public int hitCount;
    //public GameObject bulletInstance;
}
