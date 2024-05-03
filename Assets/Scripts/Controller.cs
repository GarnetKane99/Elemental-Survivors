using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerData playerData;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public bool canMove = false;
    public static Controller playerInstance;

    public List<AbilityData> ability;

    public List<AttackManager> attackList = new List<AttackManager>();
    public List<AttackManager> attackInstances = new List<AttackManager>();

    private void Awake()
    {
        playerInstance = this;
    }

    public void Init(PlayerData data)
    {
        playerData = data;
        spriteRenderer.sprite = data.character;
        for(int i = 0; i < attackList.Count; i++)
        {
            AttackManager a = Instantiate(attackList[i], transform);
            a.Init(ability[i]);
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.AddForce(new Vector2(horizontal, vertical).normalized * playerData.speed);
    }

    public void IncreaseXP(int val)
    {
        EXPManager.instance.IncreaseEXP(val);
    }
}
