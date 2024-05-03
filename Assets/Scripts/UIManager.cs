using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI level;

    public GameObject endGamePopup;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateTexts()
    {
        damage.text = (Controller.playerInstance.playerData.damage + Controller.playerInstance.damage).ToString();
        speed.text = Controller.playerInstance.speed.ToString();
        level.text = EXPManager.instance.curLvl.ToString();
    }

    public void DisplayEndGame()
    {
        endGamePopup.SetActive(true);
    }

}
