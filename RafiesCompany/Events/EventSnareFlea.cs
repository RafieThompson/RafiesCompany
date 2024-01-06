using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class SnareFleaEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new SnareFleaEvent();
        }
    }

    class SnareFleaEvent : BanditEvent
    {
        //AnimationCurve oldAnimationCurve;
        List<int> rarities = new List<int>();
        int oldMaxPowerCount;
        int oldMaxCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;
        int oldSnareFleaRarity;

        public override string GetEventName()
        {
            return "WARNING.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            //oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            oldMaxPowerCount = newLevel.maxEnemyPowerCount;
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new UnityEngine.Keyframe(0f, configs.SnareFleaSpawnCurve1.Value), new Keyframe(1f, configs.SnareFleaSpawnCurve2.Value));
            newLevel.maxEnemyPowerCount += 5;
            newLevel.minScrap += 5;
            newLevel.maxScrap += 10;

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                //newLevel.Enemies[i].rarity = 0;
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<CentipedeAI>() != null)
                {
                    oldSnareFleaRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 100;

                    oldMaxCount = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.SnareFleaEventSnareFleaMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            //newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            newLevel.maxEnemyPowerCount = oldMaxPowerCount;
            newLevel.minScrap = oldMinScrapCount;
            newLevel.maxScrap = oldMaxScrapCount;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                //newLevel.Enemies[i].rarity = rarities[i];
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<CentipedeAI>() != null)
                {
                    newLevel.Enemies[i].rarity = oldSnareFleaRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaxCount;
                }
            }
        }

        public override bool IsValid(ref SelectableLevel newLevel)
        {
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<CentipedeAI>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
