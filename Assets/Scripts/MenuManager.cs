using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Button startGame;

    public GameObject mainMenu;
    public GameObject characterMenu;
    public GameObject levelMenu;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

        startGame.onClick.AddListener(StartGame);

        mainMenu.SetActive(true);
        characterMenu.SetActive(false);
        levelMenu.SetActive(false);
    }

    //goes to character select
    public void StartGame()
    {
        mainMenu.SetActive(false);
        characterMenu.SetActive(true);
    }

    //goes to level menu
    public void CharacterSelected(PlayerData playerSelected)
    {
        GameManager.instance.selectedPlayerInstance = playerSelected;

        characterMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void LevelSelected(LevelData levelSelected)
    {
        GameManager.instance.levelDataInstance = levelSelected;
        HideAll();

        GameManager.instance.LoadAdditiveGameScene();
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        characterMenu.SetActive(false);
        levelMenu.SetActive(false);
    }
}
