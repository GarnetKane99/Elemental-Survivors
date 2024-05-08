using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public HealthManager healthMgr;
    public EnemyData enemyData;
    public float health;
    public List<Node> path = new List<Node>();
    public Node currentNode;
    public bool canMove = false;
    float sped = default;

    public void Init(Node cur, float speedOffset = default)
    {
        health = enemyData.health + EnemySpawner.instance.healthAdd;
        currentNode = cur;
        canMove = true;
        healthMgr.Init(enemyData.health + EnemySpawner.instance.healthAdd);
        sped = speedOffset;
    }

    private void Update()
    {
        if (!canMove) { return; }

        if (GameManager.instance.pauseFromUpgrade) { return; }

        Engage();
        CreatePath();
    }

    public void OnDestroy()
    {
        if (EXPManager.instance == null) { return; }
    }

    public void Engage()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NearestNode(Controller.playerInstance.transform.position));
        }
    }

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            int x = path.Count - 1;
            transform.position = Vector2.MoveTowards(transform.position, path[x].transform, ((enemyData.speed + sped)/2) * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform) < 0.75f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }

    public void GetDamaged(Vector2 dir, float dmg)
    {
        health -= TrueDamage(dmg);

        if(health <= 0)
        {
            EXPManager.instance.DropEXP(transform.position, (int)healthMgr.maxHealth);

            EnemySpawner.instance.killCount++;

            HighScoreArea.instance.killCount.text = "Kill Count: " + EnemySpawner.instance.killCount;
            
            Destroy(this.gameObject);
            return;
        }

        healthMgr.Damaged(TrueDamage(dmg));

        StartCoroutine(FlashRoutine());
    }

    public float TrueDamage(float dmg)
    {
        float rawDmg = dmg;
        if(enemyData.type == types.Fire)
        {
            if(GameManager.instance.selectedPlayerInstance.type == types.Water)
            {
                rawDmg *= 2;
            }
            if(GameManager.instance.selectedPlayerInstance.type == types.Grass)
            {
                rawDmg /= 2;
            }
        }
        else if (enemyData.type == types.Electric)
        {
            if(GameManager.instance.selectedPlayerInstance.type == types.Water)
            {
                rawDmg -= 2;
            }
            if(GameManager.instance.selectedPlayerInstance.type == types.Fire)
            {
                rawDmg *= 2;
            }
        }
        else if(enemyData.type == types.Grass)
        {
            if(GameManager.instance.selectedPlayerInstance.type == types.Fire)
            {
                rawDmg *= 2;
            }
            if(GameManager.instance.selectedPlayerInstance.type == types.Water)
            {
                rawDmg -= 3;
            }
        }
        else if(enemyData.type == types.Water)
        {
            if(GameManager.instance.selectedPlayerInstance.type == types.Electric)
            {
                rawDmg *= 3;
            }
            if(GameManager.instance.selectedPlayerInstance.type == types.Grass)
            {
                rawDmg *= 2;
            }
            if(GameManager.instance.selectedPlayerInstance.type == types.Fire)
            {
                rawDmg /= 2;
            }
        }
        return rawDmg > 0 ? rawDmg : 1;
    }

    float flashDuration = 0.5f;
    float flashInterval = .15f;
    public SpriteRenderer spriteRenderer;

    private IEnumerator FlashRoutine()
    {
        // Loop for the specified flash duration
        float timer = 0f;
        while (timer < flashDuration)
        {
            // Tween the alpha value from 1 to 0 and back to 1
            spriteRenderer.DOFade(0f, flashInterval / 2f)
                .SetEase(Ease.Flash, 1)
                .OnComplete(() => spriteRenderer.DOFade(1f, flashInterval / 2f));

            // Increment timer by the interval
            timer += flashInterval;

            // Wait for the specified interval
            yield return new WaitForSeconds(flashInterval);
        }

        // Ensure the sprite is fully visible after the flash duration
        spriteRenderer.DOFade(1f, 0);
    }

    IEnumerator ResetRB(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(0.25f);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
