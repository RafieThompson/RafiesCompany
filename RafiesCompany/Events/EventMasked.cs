using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class MaskedEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new MaskedEvent();
        }
    }
    class MaskedEvent : BanditEvent
    {
        AnimationCurve oldAnimationCurve;
        List<int> rarities = new List<int>();
        int oldMaxPowerCount;
        int oldMaxCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;
        int oldMaskRarity;

        public override string GetEventName()
        {
            return "WE COME FOR YOU.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            oldMaxPowerCount = newLevel.maxEnemyPowerCount;
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new UnityEngine.Keyframe(0f, 1f), new Keyframe(1f, 7f));
            newLevel.maxEnemyPowerCount += 5;
            newLevel.minScrap += 15;
            newLevel.maxScrap += 20;

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)
                {
                    oldMaskRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 100;

                    oldMaxCount = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.MaskedEventMaskedMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            newLevel.maxEnemyPowerCount = oldMaxPowerCount;
            newLevel.minScrap = oldMinScrapCount;
            newLevel.maxScrap = oldMaxScrapCount;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)
                {
                    newLevel.Enemies[i].rarity = oldMaskRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaxCount;
                }
            }
        }

        public override bool IsValid(ref SelectableLevel newLevel)
        {
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}