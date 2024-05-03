using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool canMove = false;
    public Vector3 target;
    AbilityData aData;
    public float lifetime;
    public int hitTargets;
    
    public void GoToTarget(AbilityData ability)
    {
        aData = ability;
        lifetime = aData.lifeTime;
        hitTargets = ability.hitCount;
        target = FindNearestEnemy();
        canMove = true;
    }

    public void Update()
    {
        if (!canMove) { return; }
        if(target == null) { Destroy(this.gameObject); return; }
        lifetime -= Time.deltaTime;

        if(lifetime <= 0 || hitTargets <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

       //Vector3 dir = (target.position - transform.position).normalized;

        transform.Translate(target * aData.speed * Time.deltaTime);
    }

    public Vector3 FindNearestEnemy()
    {
        Transform curTransform = null;
        float minDist = float.MaxValue;

        if(EnemySpawner.instance == null) { Destroy(this.gameObject); return Vector3.zero; }
        if(EnemySpawner.instance.activeEnemies.Count == 0) { Destroy(this.gameObject); return Vector3.zero; }

        for(int i = 0; i < EnemySpawner.instance.activeEnemies.Count; i++)
        {
            if (EnemySpawner.instance.activeEnemies[i] == null) { continue; }

            if (Vector2.Distance(EnemySpawner.instance.activeEnemies[i].transform.position, transform.position) < minDist)
            {
                minDist = Vector2.Distance(EnemySpawner.instance.activeEnemies[i].transform.position, transform.position);
                curTransform = EnemySpawner.instance.activeEnemies[i].transform;
            }
        }

        return (curTransform.position-transform.position).normalized;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy") { return; }

        collision.GetComponent<EnemyController>().GetDamaged(target, aData.dmg);
        hitTargets--;
    }
}
