using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//probably should be called character data or smth

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    public Sprite character;
    public string playerName;
    public string characterDescription;

    public float speed;
    public float damage;
    public float defence;
    public float rating;
}
