using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> enemies = new List<EnemyController>();
    public float healthAdd;
    public static EnemySpawner instance;

    public List<EnemyController> activeEnemies = new List<EnemyController>();

    public float spawnTime = 2f;

    public int killCount;

    private int spawnCount = 0;
    public bool lantiCount = false;

    public float speedAdder = default;

    private void Awake()
    {
        instance = this;
        if (lantiCount) killCount = 858;
    }

    public void SpawnEnemy()
    {
        InvokeRepeating("SpawnEnemiesRecurring", 0, spawnTime);
    }

    public void SpawnEnemiesRecurring()
    {
        if(spawnCount % 500 == 0 && spawnCount > 0)
        {
            Dictionary<Vector3, Node> usedNodes = new Dictionary<Vector3, Node>();

            for (int i = 0; i < 80; i++)
            {
                EnemyController randEnemy = enemies[Random.Range(0, enemies.Count)];
                //Vector3 spawnPos = transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 3f;
                //Node randNode = AStarManager.instance.NearestNode(spawnPos);

                Node randNode = AStarManager.instance.nodes[Random.Range(0, AStarManager.instance.nodes.Count)];
                if (Vector2.Distance(randNode.transform, Controller.playerInstance.transform.position) < 5)
                {
                    i--;
                    continue;
                }
                if (usedNodes.ContainsKey(randNode.transform))
                {
                    i--;
                    continue;
                }

                EnemyController inst = Instantiate(randEnemy, randNode.transform, Quaternion.identity, transform);
                inst.Init(randNode);
                activeEnemies.Add(inst);
                usedNodes.Add(randNode.transform, randNode);
            }

            spawnCount++;
            return;
        }


        int active = default;
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            if(activeEnemies[i] != null)
            {
                active++;
            }
            if(active >= 200)
            {
                return;
            }
        }

        int randVal = Mathf.FloorToInt(UnityEngine.Random.value * 75.99f);

        bool hoardSpawn = randVal == 0;

        if (hoardSpawn)
        {
            Debug.Log("spawning hoard");
            Dictionary<Vector3, Node> usedNodes = new Dictionary<Vector3, Node>();

            EnemyController randEnemy = enemies[Random.Range(0, enemies.Count)];
            for (int i = 0; i < Random.Range(30,50); i++)
            {
                Node randNode = AStarManager.instance.nodes[Random.Range(0, AStarManager.instance.nodes.Count)];
                if (Vector2.Distance(randNode.transform, Controller.playerInstance.transform.position) < 5)
                {
                    i--;
                    continue;
                }
                if (usedNodes.ContainsKey(randNode.transform))
                {
                    i--;
                    continue;
                }

                EnemyController inst = Instantiate(randEnemy, randNode.transform, Quaternion.identity, transform);
                inst.Init(randNode);
                activeEnemies.Add(inst);
                usedNodes.Add(randNode.transform, randNode);
            }

            spawnCount++;
            return;
        }

        if(spawnCount % 25 == 0 && spawnCount > 0) 
        {
            Dictionary<Vector3, Node> usedNodes = new Dictionary<Vector3, Node>();

            for(int i = 0; i < 30; i++)
            {
                EnemyController randEnemy = enemies[Random.Range(0, enemies.Count)];
                //Vector3 spawnPos = transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * 3f;
                //Node randNode = AStarManager.instance.NearestNode(spawnPos);

                Node randNode = AStarManager.instance.nodes[Random.Range(0, AStarManager.instance.nodes.Count)];
                if(Vector2.Distance(randNode.transform, Controller.playerInstance.transform.position) < 5)
                {
                    i--;
                    continue;
                }
                if (usedNodes.ContainsKey(randNode.transform))
                {
                    i--;
                    continue;
                }

                EnemyController inst = Instantiate(randEnemy, randNode.transform, Quaternion.identity, transform);
                inst.Init(randNode);
                activeEnemies.Add(inst);
                usedNodes.Add(randNode.transform, randNode);
            }

            spawnCount++;
            return;
        }

        EnemyController randomEnemy = enemies[Random.Range(0, enemies.Count)];

        Node randomNode = RandomNodeNearPlayer();
        EnemyController enemyInstance = Instantiate(randomEnemy, randomNode.transform, Quaternion.identity, transform);
        enemyInstance.Init(randomNode);
        activeEnemies.Add(enemyInstance);
        spawnCount++;
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
