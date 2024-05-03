using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> enemies = new List<EnemyController>();
    public static EnemySpawner instance;

    public List<EnemyController> activeEnemies = new List<EnemyController>();

    public float spawnTime = 2f;

    private void Awake()
    {
        instance = this; 
    }

    public void SpawnEnemy()
    {
        InvokeRepeating("SpawnEnemiesRecurring", 0, spawnTime);
    }

    public void SpawnEnemiesRecurring()
    {
        EnemyController randomEnemy = enemies[Random.Range(0, enemies.Count)];

        Node randomNode = RandomNodeNearPlayer();
        EnemyController enemyInstance = Instantiate(randomEnemy, randomNode.transform, Quaternion.identity);
        enemyInstance.Init(randomNode);
        activeEnemies.Add(enemyInstance);
    }

    /*public void Update()
    {
        if (GameManager.instance.pauseFromUpgrade) { }
    }*/

    public void StopSpawn()
    {
        CancelInvoke();
    }

    public Node RandomNodeNearPlayer()
    {
        bool found = false;
        Node rand = null;
        while (!found)
        {
            Node n = AStarManager.instance.nodes[Random.Range(0, AStarManager.instance.nodes.Count)];
            if(Vector2.Distance(n.transform, Controller.playerInstance.transform.position) > 3.0f && Vector2.Distance(n.transform, Controller.playerInstance.transform.position) < 10.0f)
            {
                rand = n;
                found = true;
            }
        }
        return rand;
    }
}
