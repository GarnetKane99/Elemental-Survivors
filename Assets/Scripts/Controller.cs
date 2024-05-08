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
    public float maxHealth;

    public float dmgCooldown = .25f;
    public bool hitStop = false;

    private void Awake()
    {
        playerInstance = this;
#if UNITY_EDITOR
        isEditor = true;
#endif
    }

    public void Init(PlayerData data)
    {
        playerData = data;
        maxHealth = data.health;
        health = data.health;
        speed = data.speed;
        //speed = data.speed;

        GetComponent<Animator>().runtimeAnimatorController = data.controller;

        healthMgr.Init(health);

        spriteRenderer.sprite = data.character;
        for(int i = 0; i < attackList.Count; i++)
        {
            AttackManager a = Instantiate(attackList[i], transform);
            a.Init(ability[i], this);
            attackInstances.Add(a);
        }
    }

    public void NewAbility(PlayerData data)
    {
        playerData = data;
    }

    public void EnableMovement()
    {
        canMove = true;
    }
    bool isEditor = false;
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
                dmgCooldown = 0.45f;
                hitStop = false;
            }
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (isEditor)
            rb.AddForce(new Vector2(horizontal, vertical).normalized * speed);
        else
            rb.AddForce(new Vector2(horizontal, vertical).normalized * (speed * 10));
    }

    public void IncreaseXP(int val)
    {
        EXPManager.instance.IncreaseEXP(val);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != "Enemy") { return; }
        if(hitStop) { return; }

        hitStop = true;
        TakeDamage((int)TrueDamage(collision.GetComponent<EnemyController>().enemyData, (int)collision.GetComponent<EnemyController>().enemyData.dmg));
    }

    public void TakeDamage(int dmg)
    {
        //dmg = (int)TrueDamage(dmg);
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

    public void RegenHealth(float val)
    {
        health += (val * maxHealth) / 100;
        Debug.Log($"regenning health: {val * maxHealth / 100}");
        health = Mathf.Clamp(health, 0, maxHealth);

        healthMgr.Regen(val * maxHealth / 100);
    }

    public void IncreaseMaxHealth(float val)
    {
        Debug.Log($"increasing health: {maxHealth} to {maxHealth + val}");
        maxHealth += val;
        health += val;
        healthMgr.IncreaseMaxHealth(val);
    }

    public float TrueDamage(EnemyData e, float dmg)
    {
        float rawDmg = dmg;

        if (playerData.type == types.Fire)
        {
            if (e.type == types.Water)
            {
                rawDmg *= 2;
            }
            if (e.type == types.Grass)
            {
                rawDmg /= 2;
            }
        }
        if (playerData.type == types.Electric)
        {
            if (e.type == types.Water)
            {
                rawDmg -= 2;
            }
            if (e.type == types.Fire)
            {
                rawDmg *= 2;
            }
        }
        if (playerData.type == types.Grass)
        {
            if (e.type == types.Fire)
            {
                rawDmg *= 2;
            }
            if (e.type == types.Water)
            {
                rawDmg -= 3;
            }
        }
        if (playerData.type == types.Water)
        {
            if (e.type == types.Electric)
            {
                rawDmg *= 3;
            }
            if (e.type == types.Fire)
            {
                rawDmg /= 2;
            }
        }

        return rawDmg;
    }
}
