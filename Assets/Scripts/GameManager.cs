using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelData levelDataInstance;

    public static GameManager instance;
    public PlayerData selectedPlayerInstance = null;

    public Controller player = null;

    public bool pauseFromUpgrade = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);

        instance = this;
    }


    public void LoadAdditiveGameScene()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Additive);

        //Camera.main.transform.DOMove()
    }

    public void MoveCamToPlayer(Controller playerInstance)
    {
        AStarManager.instance.Init();
        player = playerInstance;

        Camera.main.transform.DOMove(new Vector3(
            player.transform.position.x
            ,player.transform.position.y, 
            -10)
            , 4.0f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                HighScoreArea.instance.startTimer = true;
                EnemySpawner.instance.SpawnEnemy();
                //player.EnableMovement();
                StartCoroutine(StartGame());
            });
    }

    public IEnumerator StartGame()
    {
        RoundUI.inst.DoMove();
        Camera.main.transform.parent = player.transform;
        yield return new WaitForSeconds(2);
        player.EnableMovement();
    }
}
