using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Button startGame;
    public Button controlsButton;
    public Button controllsBack;

    public GameObject mainMenu;
    public GameObject characterMenu;
    public GameObject levelMenu;
    public GameObject controlsMenu;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

        startGame.onClick.AddListener(StartGame);

        controlsButton.onClick.AddListener(DisplayControls);
        controllsBack.onClick.AddListener(DefaultSettings);

        DefaultSettings();
    }

    public void DisplayControls()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void DefaultSettings()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
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
        GameManager.instance.selectedPlayerInstance = ScriptableObjectExtension.Clone(playerSelected);

        GameManager.instance.selectedPlayerInstance.evolution = ScriptableObjectExtension.Clone(GameManager.instance.selectedPlayerInstance.evolution);

        characterMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void LevelSelected(LevelData levelSelected)
    {
        GameManager.instance.pauseFromUpgrade = false;
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
