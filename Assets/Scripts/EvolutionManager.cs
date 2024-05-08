using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EvolutionManager", menuName = "Scriptable Objects/Evolution Data")]
public class EvolutionManager : ScriptableObject
{
    public bool evolvedState;
    public string evolutionBaseID;
    public List<Evolution> evolutions;
}
[System.Serializable]
public class Evolution
{
    public string evolutionID;
    public string evolutionDef;
    public bool evolved;
    public types typeAddition;
    public RuntimeAnimatorController newSprite;
    public Sprite img;
    public EvolutionManager evolvesTo;
    public PlayerData upgradeStats;
}