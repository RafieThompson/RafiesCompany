using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class HoardingEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new HoardingEvent();
        }
    }

    class HoardingEvent : BanditEvent
    {
        //AnimationCurve oldAnimationCurve;
        List<int> rarities = new List<int>();
        int oldMaxCount;
        int oldMaxPowerCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;
        int oldRarity;
          
        public override string GetEventName()
        {
            return "WARNING.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            oldMaxPowerCount = newLevel.maxEnemyPowerCount;
            //oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            newLevel.maxEnemyPowerCount += 5;
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0, 1f), new Keyframe(1f, 7f));
            newLevel.minScrap += 5;
            newLevel.maxScrap += 10;


            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                //newLevel.Enemies[i].rarity = 0;
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<HoarderBugAI>() != null)
                {
                    oldRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 100;

                    oldMaxCount = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.HoardingBugsEventHoardingMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.maxEnemyPowerCount = oldMaxPowerCount;
            //newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            newLevel.minScrap = oldMinScrapCount;
            newLevel.maxScrap = oldMaxScrapCount;

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                //newLevel.Enemies[i].rarity = rarities[i];
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<HoarderBugAI>() != null)
                {
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaxCount;
                    newLevel.Enemies[i].rarity = oldRarity;
                }
            }
        }

        public override bool IsValid(ref SelectableLevel newLevel)
        {
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<HoarderBugAI>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
