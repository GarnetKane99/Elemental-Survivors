using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Bullet bulletInstance;
    public AbilityData abilityInstance;
    public bool canAttack = false;

    public void Init(AbilityData ability)
    {
        abilityInstance = ability;
        canAttack = true;
    }
}
