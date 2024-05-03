using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeManager", menuName = "Scriptable Objects/Upgrade Manager")]
public class LevelManager : ScriptableObject
{
    //public 
    public List<Upgrades> upgradeList = new List<Upgrades>();

    public List<float> upgradeXPRequirement;
}
