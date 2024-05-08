using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : MonoBehaviour
{
    public static EXPManager instance;

    public List<EXPController> experienceTypes = new List<EXPController>();

    public GameObject upgradeBox;
    
    //public List<Upgrades> upgradeTypes;

    public List<UpgradeManager> upgrades;

    public GameObject evolvedBox;
    public List<EvolutionHandler> evolutions;

    public Slider xpSlider;
    public LevelManager lvlMgr;
    public int curLvl = 0;

    public float pickupRadius = 1;

    private void Awake()
    {
        upgradeBox.SetActive(false);
        evolvedBox.SetActive(false);



        instance = this;
        xpSlider.maxValue = lvlMgr.upgradeXPRequirement[curLvl];
        xpSlider.value = 0;
    }

    private void Start()
    {
        UIManager.instance.UpdateTexts();
    }

    public void DropEXP(Vector3 pos, int val)
    {
        EXPController xp = Instantiate(experienceTypes[0], pos, Quaternion.identity, EnemySpawner.instance.transform);
        xp.Init(val);
    }

    public EvolutionManager curEvolution = null;

    public void IncreaseEXP(int val)
    {
        xpSlider.value += val;
        if(xpSlider.value == xpSlider.maxValue)
        {
            //do stuff
            GameManager.instance.pauseFromUpgrade = true;
            EnemySpawner.instance.StopSpawn();


            if(curLvl % 10 == 0 && curLvl > 0 && curLvl < 30)
            {
                evolvedBox.SetActive(true);

                EvolutionManager evolvedState = null;

                if(GameManager.instance.selectedPlayerInstance.evolution.evolvedState == false)
                {
                    evolvedState = GameManager.instance.selectedPlayerInstance.evolution;
                }
                else
                {
                    for(int i = 0; i < GameManager.instance.selectedPlayerInstance.evolution.evolutions.Count; i++)
                    {
                        if (GameManager.instance.selectedPlayerInstance.evolution.evolutions[i].evolved)
                        {
                            evolvedState = GameManager.instance.selectedPlayerInstance.evolution.evolutions[i].evolvesTo;
                            break;
                        }
                    }
                }

                for (int i = 0; i < evolutions.Count; i++)
                {
                    evolutions[i].gameObject.SetActive(true);
                    if(i >= evolvedState.evolutions.Count)
                    {
                        evolutions[i].gameObject.SetActive(false);
                        continue;
                    }
                    curEvolution = evolvedState;
                    evolutions[i].Init(curEvolution.evolutions[i]);
                }

                //for(int i = 0; i < GameManager.instance.selectedPlayerInstance.evolutions.Count; i++)
                //{
                //    if (GameManager.instance.selectedPlayerInstance.evolutions[i].evolvedState) { continue; }

                //    for(int j = 0; j < evolutions.Count; j++)
                //    {
                //        evolutions[j].gameObject.SetActive(true);
                //        if(j > GameManager.instance.selectedPlayerInstance.evolutions[i].evolutions.Count)
                //        {
                //            evolutions[j].gameObject.SetActive(false);
                //            continue;
                //        }

                //        curEvolution = GameManager.instance.selectedPlayerInstance.evolutions[i];

                //        evolutions[j].Init(GameManager.instance.selectedPlayerInstance.evolutions[i].evolutions[j]);
                //    }

                //    break;
                //}

                return;
            }

            upgradeBox.SetActive(true);
            List<Upgrades> usedUpgrades = SelectUpgrades(3);

            for(int i = 0; i < upgrades.Count; i++)
            {
                Upgrades upgradeFound = usedUpgrades[Random.Range(0, usedUpgrades.Count)];
                upgrades[i].Init(upgradeFound);

                usedUpgrades.Remove(upgradeFound);
            }
        }
    }

    public List<Upgrades> SelectUpgrades(int numOptions)
    {
        List<Upgrades> selectedUpgrades = new List<Upgrades>();

        // Calculate total weight
        int totalWeight = 0;
        foreach (Upgrades upgrade in lvlMgr.upgradeList)
        {
            totalWeight += GetRarityWeight(upgrade.upgradeRarity);
        }

        // Select upgrades
        for (int i = 0; i < numOptions; i++)
        {
            // Generate random number
            int randomNumber = UnityEngine.Random.Range(0, totalWeight);

            // Accumulate weights and select upgrade
            int accumulatedWeight = 0;
            foreach (Upgrades upgrade in lvlMgr.upgradeList)
            {
                accumulatedWeight += GetRarityWeight(upgrade.upgradeRarity);
                if (randomNumber < accumulatedWeight)
                {
                    selectedUpgrades.Add(upgrade);
                    break;
                }
            }
        }

        return selectedUpgrades;
    }

    private int GetRarityWeight(Upgrades.UpgradeRarity rarity)
    {
        switch (rarity)
        {
            case Upgrades.UpgradeRarity.Common:
                return 10; // Adjust weights as needed
            case Upgrades.UpgradeRarity.Uncommon:
                return 8;
            case Upgrades.UpgradeRarity.Rare:
                return 5;
            case Upgrades.UpgradeRarity.Master:
                return 2;
            case Upgrades.UpgradeRarity.Legendary:
                return 1;
            default:
                return 0;
        }
    }

    public void HideAndEvolve()
    {
        curEvolution.evolvedState = true;
        evolvedBox.SetActive(false);
        ResetXP();
    }

    public void ResetXP()
    {
        EnemySpawner.instance.SpawnEnemy();
        GameManager.instance.pauseFromUpgrade = false;
        upgradeBox.SetActive(false);
        curLvl++;
        if (curLvl >= lvlMgr.upgradeXPRequirement.Count)
            curLvl--;

        if(curLvl % 10 == 0 || curLvl > 25)
            EnemySpawner.instance.speedAdder += 1;

        xpSlider.value = 0;
        xpSlider.maxValue = lvlMgr.upgradeXPRequirement[curLvl];
        UIManager.instance.UpdateTexts();
    }
}
