using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class JesterEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new JesterEvent();
        }
    }

    class JesterEvent : BanditEvent
    {
        //AnimationCurve oldAnimationCurve;
        List<int> rarities = new List<int>();
        int oldMaxCount;
        int oldMaxPowerCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;
        int oldJesterRarity;

        public override string GetEventName()
        {
            return "ANOMALY DETECTED.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            //oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            oldMaxPowerCount = newLevel.maxEnemyPowerCount;
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0, 500f));
            newLevel.maxEnemyPowerCount += 5;
            newLevel.minScrap += 5;
            newLevel.maxScrap += 10;

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                //newLevel.Enemies[i].rarity = 0;

                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<JesterAI>() != null)
                {
                    oldJesterRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 100;

                    oldMaxCount = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.JesterEventJesterMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.maxEnemyPowerCount = oldMaxPowerCount;
            newLevel.minScrap = oldMinScrapCount;
            newLevel.maxScrap = oldMaxScrapCount;
            //newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                //newLevel.Enemies[i].rarity = rarities[i];

                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<JesterAI>() != null)
                {
                    newLevel.Enemies[i].rarity = oldJesterRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaxCount;
                }
            }
        }

        public override bool IsValid(ref SelectableLevel newLevel)
        {
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<JesterAI>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
