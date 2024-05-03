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

    public void Init(Node cur)
    {
        health = enemyData.health;
        currentNode = cur;
        canMove = true;
        healthMgr.Init(enemyData.health);
    }

    private void Update()
    {
        if (!canMove) { return; }
        Engage();
        CreatePath();
    }

    public void OnDestroy()
    {
        if (EXPManager.instance == null) { return; }
        EXPManager.instance.DropEXP(transform.position, (int)healthMgr.maxHealth);
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
            transform.position = Vector2.MoveTowards(transform.position, path[x].transform, enemyData.speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform) < 0.75f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }

    public void GetDamaged(Vector2 dir, float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        healthMgr.Damaged(dmg);

        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        //rend.DOColor(Color.clear, 0.1f)
        //    .


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb== null) { return; }

        StartCoroutine(FlashRoutine());

        //rb.AddForce(dir * 2.0f, ForceMode2D.Impulse);
        //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //StartCoroutine(ResetRB(rb));
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
