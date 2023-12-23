using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class BrackenCoilEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new BrackenCoilEvent();
        }
    }

    class BrackenCoilEvent : BanditEvent
    {
        //AnimationCurve oldAnimationCurve;
        List<int> rarities = new List<int>();
        int oldCoilMax;
        int oldFlowerMax;

        public override string GetEventName()
        {
            return "You feel a tingle down your spine...";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            //oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new UnityEngine.Keyframe(0, 500f));

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<FlowermanAI>() != null)
                {
                    newLevel.Enemies[i].rarity = 999;

                    oldFlowerMax = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.FlowermanCoilEventFlowermanMax.Value;
                }
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    newLevel.Enemies[i].rarity = 999;

                    oldCoilMax = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.FlowermanCoilEventCoilHeadMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            //newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                newLevel.Enemies[i].rarity = rarities[i];
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<FlowermanAI>() != null)
                {
                    newLevel.Enemies[i].enemyType.MaxCount = oldFlowerMax;
                }
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    newLevel.Enemies[i].enemyType.MaxCount = oldCoilMax;
                }
            }
        }

        public override bool IsValid(ref SelectableLevel newLevel)
        {
            bool flowerManFound = false;
            bool springManFound = false;
            foreach (var enemy in newLevel.Enemies)
            {
                if (enemy.enemyType.enemyPrefab.GetComponent<FlowermanAI>() != null)
                {
                    flowerManFound = true;
                }
                else if (enemy.enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    springManFound = true;
                }
            }
            if (flowerManFound && springManFound)
            {
                return true;
            }
            return false;
        }
    }
}
