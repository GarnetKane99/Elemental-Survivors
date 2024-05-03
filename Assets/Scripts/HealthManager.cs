using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider scrollBar;
    public float maxHealth = default;

    public void Init(float val)
    {
        //scrollBar.size = val;
        scrollBar.maxValue = val;
        scrollBar.value = val;
        maxHealth = val;
    }

    public void Damaged(float dmg)
    {
        scrollBar.value -= dmg;//(dmg/10) * maxHealth;
        //scrollBar.size
    }

    public void Regen(float amt)
    {
        //scrollBar.size += amt;
        //scrollBar.size = Mathf.Clamp(scrollBar.v, 0, maxHealth);
    }
}
