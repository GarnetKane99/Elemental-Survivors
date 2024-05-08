using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreArea : MonoBehaviour
{
    public TextMeshProUGUI killCount;
    public TextMeshProUGUI timeSurvived;
    private float timer;
    public bool startTimer;
    public static HighScoreArea instance;

    public bool lantiScore = false;

    private void Awake()
    {
        instance = this;
        killCount.text = "Kill Count: 0";
        timeSurvived.text = $"Time Survived: 00:00:00";

        if (lantiScore) { timer = 1920; }
    }

    private void Update()
    {
        if (GameManager.instance.pauseFromUpgrade) { return; }
        if (!startTimer) { return; }

        timer += Time.deltaTime;

        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);
        int ms = Mathf.FloorToInt((timer * 1000f) % 1000f);

        timeSurvived.text = $"Time Survived: " + string.Format("{0:00}:{1:00}:{2:00}", min, sec, ms);
    }
}
