using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Button upgrade;
    public Upgrades upgradeType;
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDescription;

    private void Awake()
    {
        upgrade.onClick.AddListener(DoUpgrade);
    }

    public void Init(Upgrades upgrade)
    {
        upgradeType = null;
        upgradeType = upgrade;
        upgradeName.text = upgrade.upgradeType.ToString();
        if(upgrade.upgradeType != Upgrades.UpgradeTypes.newWeapon)
            upgradeDescription.text = upgrade.upgradeRarity.ToString() + "\n" + upgrade.upgradeType.ToString() + " +" + upgrade.upgradeVal.ToString();
    }

    public void DoUpgrade()
    {
        switch (upgradeType.upgradeType)
        {
            case Upgrades.UpgradeTypes.newWeapon:
                IncreaseEnemyHealth();
                IncreaseEnemyHealth();
                DecreaseSpawnTime();
                break;
            case Upgrades.UpgradeTypes.ammo:
                DecreaseSpawnTime();
                IncreaseEnemyHealth();
                Controller.playerInstance.ammo += (int)upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.cooldown:
                DecreaseSpawnTime();
                DecreaseSpawnTime();
                Controller.playerInstance.cooldown -= upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.hitLifetime:
                IncreaseEnemyHealth();
                Controller.playerInstance.hitLifetime += (int)upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.bulletLifetime:
                Controller.playerInstance.bulletLifetime += upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.pickupRadius:
                DecreaseSpawnTime();
                EXPManager.instance.pickupRadius += upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.attackSpeed:
                Controller.playerInstance.attackSpeed += (int)upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.speed:
                DecreaseSpawnTime();
                Controller.playerInstance.speed += upgradeType.upgradeVal;
                break;
            case Upgrades.UpgradeTypes.damage:
                IncreaseEnemyHealth();
                Controller.playerInstance.damage += upgradeType.upgradeVal;
                break;
        }

        EXPManager.instance.ResetXP();
    }

    public void DecreaseSpawnTime()
    {
        EnemySpawner.instance.spawnTime -= 0.1f;
        EnemySpawner.instance.spawnTime = Mathf.Clamp(EnemySpawner.instance.spawnTime, 0.5f, 5.0f);
    }

    public void IncreaseEnemyHealth()
    {
        for (int i = 0; i < EnemySpawner.instance.enemies.Count; i++)
        {
            EnemySpawner.instance.enemies[i].health += 2;
        }
    }
}
