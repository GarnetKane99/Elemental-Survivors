using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public HealthManager healthMgr;
    public PlayerData playerData;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public bool canMove = false;
    public static Controller playerInstance;

    public List<AbilityData> ability;

    public List<AttackManager> attackList = new List<AttackManager>();
    public List<AttackManager> attackInstances = new List<AttackManager>();

    public float speed;
    public float cooldown;
    public int hitLifetime;
    public int ammo;
    public int attackSpeed;
    public float bulletLifetime;
    public float damage;
    public float health;

    private float dmgCooldown = .1f;
    public bool hitStop = false;

    private void Awake()
    {
        playerInstance = this;
    }

    public void Init(PlayerData data)
    {
        playerData = data;
        health = data.health;
        speed = data.speed;

        healthMgr.Init(health);

        spriteRenderer.sprite = data.character;
        for(int i = 0; i < attackList.Count; i++)
        {
            AttackManager a = Instantiate(attackList[i], transform);
            a.Init(ability[i], this);
            attackInstances.Add(a);
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) { return; }
        if (GameManager.instance.pauseFromUpgrade) { return; }

        if (hitStop)
        {
            dmgCooldown -= Time.deltaTime;
            if(dmgCooldown <= 0)
            {
                dmgCooldown = 0.1f;
                hitStop = false;
            }
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.AddForce(new Vector2(horizontal, vertical).normalized * speed);
    }

    public void IncreaseXP(int val)
    {
        EXPManager.instance.IncreaseEXP(val);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != "Enemy") { return; }
        if(hitStop) { return; }

        TakeDamage((int)collision.GetComponent<EnemyController>().enemyData.dmg);
        hitStop = true;
    }

    public void TakeDamage(int dmg)
    {
        healthMgr.Damaged(dmg);

        health -= dmg;

        if(health <= 0)
        {
            EnemySpawner.instance.StopSpawn();
            GameManager.instance.pauseFromUpgrade = true;
            UIManager.instance.DisplayEndGame();
            //end game
        }
    }
}
