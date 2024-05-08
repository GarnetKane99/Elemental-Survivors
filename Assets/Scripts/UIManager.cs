using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI level;

    public Button endGame;

    public GameObject endGamePopup;

    private void Awake()
    {
        instance = this;
        endGame.onClick.AddListener(() =>
        {
            Camera.main.transform.parent = GameManager.instance.transform;
            MenuManager.instance.DefaultSettings();
            SceneManager.UnloadSceneAsync("MainGame");
        });
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
