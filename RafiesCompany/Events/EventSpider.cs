using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class SpiderEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new SpiderEvent();
        }
    }

    class SpiderEvent : BanditEvent 
    {
        //AnimationCurve oldAnimationCurve;
        List<int> rarities = new List<int>();
        int oldMaxCount;
        int oldMaxPowerCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;

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
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0, 500f));
            newLevel.maxEnemyPowerCount += 20;
            newLevel.minScrap += 15;
            newLevel.maxScrap += 20;

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                newLevel.Enemies[i].rarity = 0;
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<SandSpiderAI>() != null)
                {
                    newLevel.Enemies[i].rarity = 999;

                    oldMaxCount = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.SpiderEventSpiderMax.Value;
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
                newLevel.Enemies[i].rarity = rarities[i];
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<SandSpiderAI>() != null)
                {
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaxCount;
                }
            }
        }

        public override bool IsValid(ref SelectableLevel newLevel)
        {
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<SandSpiderAI>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
