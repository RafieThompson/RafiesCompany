using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class LittleGirlEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new LittleGirlEvent();
        }
    }

    class LittleGirlEvent : BanditEvent
    {
        AnimationCurve oldAnimationCurve; // If u want to change spawnfrequency
        List<int> rarities = new List<int>();
        int oldGirlRarity;
        int oldMaskRarity;
        int oldGirlMax;
        int oldMaskMax;
        int oldMaxPowerCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;
        public override string GetEventName()
        {
            return "DO NOT ENTER.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            oldMaxPowerCount = newLevel.maxEnemyPowerCount;
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            newLevel.maxEnemyPowerCount += 10;
            newLevel.minScrap += 5;
            newLevel.maxScrap += 10;
            newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0f, 0.5f));



            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                //newLevel.Enemies[i].rarity = 0;
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<DressGirlAI>() != null)
                {
                    oldGirlRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 999;

                    oldGirlMax = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.LittleGirlEventGirlMax.Value;
                }
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)  
                {
                    oldMaskRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 999;

                    oldMaskMax = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.LittleGirlEventMaskMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.minScrap = oldMinScrapCount;
            newLevel.maxScrap = oldMaxScrapCount;
            newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            newLevel.maxEnemyPowerCount = oldMaxPowerCount;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                //newLevel.Enemies[i].rarity = rarities[i];
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<DressGirlAI>() != null)
                {
                    newLevel.Enemies[i].rarity = oldGirlRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldGirlMax;
                }
                else if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)
                {
                    newLevel.Enemies[i].rarity = oldMaskRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaskMax;
                }
            }
        }
        public override bool IsValid(ref SelectableLevel newLevel)
        {
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<DressGirlAI>() != null)
                {
                    return true;
                }
                else if (enemy.enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
