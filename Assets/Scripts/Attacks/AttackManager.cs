using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Controller owner;
    public Bullet bulletInstance;
    public AbilityData abilityInstance;
    public bool canAttack = false;

    public void Init(AbilityData ability, Controller con)
    {
        abilityInstance = ability;
        owner = con;
        canAttack = true;
    }
}
