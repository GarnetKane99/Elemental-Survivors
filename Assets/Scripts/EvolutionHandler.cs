using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolutionHandler : MonoBehaviour
{
    public Button upgrade;
    public Evolution evolutionData;
    public Image upgradeImage;
    public TextMeshProUGUI upgradeType;
    public TextMeshProUGUI upgradeDescription;

    private void Awake()
    {
        upgrade.onClick.AddListener(EvolveUnit);
    }

    public void Init(Evolution data)
    {
        evolutionData = data;
        upgradeType.text = data.evolutionID;
        upgradeDescription.text = data.evolutionDef;
        if (data.img != null)
            upgradeImage.sprite = data.img;
        else
            upgradeImage.sprite = null;
    }

    public void EvolveUnit()
    {
        if(evolutionData.newSprite != null)
            Controller.playerInstance.GetComponent<Animator>().runtimeAnimatorController = evolutionData.newSprite;
        if(evolutionData.upgradeStats != null)
            Controller.playerInstance.playerData = evolutionData.upgradeStats;
        
        GameManager.instance.selectedPlayerInstance.type |= evolutionData.typeAddition;
        evolutionData.evolved = true;
        EXPManager.instance.HideAndEvolve();
    }
}
