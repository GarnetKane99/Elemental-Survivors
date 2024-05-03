using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapMenuHandler : MonoBehaviour
{
    public LevelData levelData;

    public Button button;
    public Image levelImg;

    public TextMeshProUGUI levelName;

    public void Awake()
    {
        button.onClick.AddListener(() => {
            MenuManager.instance.LevelSelected(levelData);
        });

        Init();
    }

    public void Init()
    {
        levelName.text = levelData.levelName;
    }
}
