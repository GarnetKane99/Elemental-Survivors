using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//probably should be called character data or smth

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    public RuntimeAnimatorController controller;
    public Sprite character;
    public string playerName;
    public string characterDescription;

    public float speed;
    public float damage;
    public float health;
    public float rating;

    public types type;

    public EvolutionManager evolution;

    //public List<EvolutionManager> evolutions;

}

public static class ScriptableObjectExtension
{
    /// <summary>
    /// Creates and returns a clone of any given scriptable object.
    /// </summary>
    public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
    {
        if (scriptableObject == null)
        {
            Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
            return (T)ScriptableObject.CreateInstance(typeof(T));
        }

        T instance = UnityEngine.Object.Instantiate(scriptableObject);
        instance.name = scriptableObject.name; // remove (Clone) from name
        return instance;
    }
}

[System.Flags]
public enum types
{
    Fire = 1,
    Water = 2,
    Electric = 4,
    Grass = 8
}
