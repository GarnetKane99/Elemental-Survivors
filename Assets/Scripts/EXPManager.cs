using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : MonoBehaviour
{
    public static EXPManager instance;

    public List<EXPController> experienceTypes = new List<EXPController>();

    public Slider xpSlider;
    public LevelManager lvlMgr;
    public int curLvl = 0;

    private void Awake()
    {
        instance = this;
        xpSlider.maxValue = lvlMgr.upgradeXPRequirement[curLvl];
        xpSlider.value = 0;
    }

    public void DropEXP(Vector3 pos, int val)
    {
        EXPController xp = Instantiate(experienceTypes[0], pos, Quaternion.identity);
        xp.Init(val);
    }

    public void IncreaseEXP(int val)
    {
        xpSlider.value += val;
        if(xpSlider.value == xpSlider.maxValue)
        {
            //do stuff
            ResetXP();
        }
    }

    public void ResetXP()
    {
        curLvl++;
        xpSlider.value = 0;
        xpSlider.maxValue = lvlMgr.upgradeXPRequirement[curLvl];
    }
}
