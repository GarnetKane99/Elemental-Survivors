using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Scrollbar scrollBar;
    public float maxHealth = default;

    public void Init(float val)
    {
        //scrollBar.size = val;
        scrollBar.size = 1;
        maxHealth = val;
    }

    public void Damaged(float dmg)
    {
        scrollBar.size -= (dmg/10) * maxHealth;
        //scrollBar.size
    }

    public void Regen(float amt)
    {
        scrollBar.size += amt;
        //scrollBar.size = Mathf.Clamp(scrollBar.v, 0, maxHealth);
    }
}
