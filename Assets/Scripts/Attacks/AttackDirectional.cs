using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirectional : AttackManager
{
    float cooldown;
    // Update is called once per frame
    void Update()
    {
        if (!canAttack) { return; }
        if (GameManager.instance.pauseFromUpgrade) { return; }

        cooldown -= Time.deltaTime;

        if(cooldown <= 0)
        {
            //SpawnBullet();
            StartCoroutine(DoAmmoShots());
            cooldown = Cooldown();
        }
    }

    public void SpawnBullet()
    {
        Bullet b = Instantiate(bulletInstance, transform.position, Quaternion.identity);
        b.GoToTarget(abilityInstance);
    }
    public IEnumerator DoAmmoShots()
    {
        for(int i = 0; i < abilityInstance.ammo + Controller.playerInstance.ammo; i++)
        {
            if (GameManager.instance.pauseFromUpgrade) { yield break; }

            if(i > 0)
                yield return new WaitForSeconds(((abilityInstance.ammo + Controller.playerInstance.ammo) / Cooldown()) - 0.15f);

            SpawnBullet();
        }
    }

    public float Cooldown()
    {
        return abilityInstance.cooldown - owner.cooldown;
    }
}
