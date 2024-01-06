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
        /*AnimationCurve oldAnimationCurve;*/ // If u want to change spawnfrequency

        // List to store the original rarities of enemies
        List<int> rarities = new List<int>();

        // Variables
        int oldCoilMax;
        int oldFlowerMax;
        int oldMaxPowerCount;
        int oldMinScrapCount;
        int oldMaxScrapCount;
        int oldCoilRarity;
        int oldFlowerRarity;

        // Returns the name of the event
        public override string GetEventName()
        {
            return "WARNING.";
        }

        // Called when a new level is loaded
        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            //oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            oldMaxPowerCount = newLevel.maxEnemyPowerCount;
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 7f));
            newLevel.maxEnemyPowerCount += 10;
            newLevel.minScrap += 10;
            newLevel.maxScrap += 15;

            // Loop through all enemies in the new level
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                // Store the original rarity of each enemy
                rarities.Add(newLevel.Enemies[i].rarity);
                // set rarity of enemies to 0
                //newLevel.Enemies[i].rarity = 0;
                // Check if the enemy is of type FlowermanAI, change to whatever enemy you need to patch
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<FlowermanAI>() != null)
                {
                    oldFlowerRarity = newLevel.Enemies[i].rarity;
                    // Set the rarity to a specific value 0 = never spawns, 100 = guaranteed
                    newLevel.Enemies[i].rarity = 999;

                    // Store the original maximum count
                    oldFlowerMax = newLevel.Enemies[i].enemyType.MaxCount;

                    // Set the new maximum count based on the configuration
                    newLevel.Enemies[i].enemyType.MaxCount = configs.FlowermanCoilEventFlowermanMax.Value;
                }

                // Check if the enemy is of type SpringManAI
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    oldCoilRarity = newLevel.Enemies[i].rarity;
                    // Set the rarity to a specific value 0 = never spawns, 100 = guaranteed
                    newLevel.Enemies[i].rarity = 999;

                    // Store the original maximum count
                    oldCoilMax = newLevel.Enemies[i].enemyType.MaxCount;

                    // Set the new maximum count based on the configuration
                    newLevel.Enemies[i].enemyType.MaxCount = configs.FlowermanCoilEventCoilHeadMax.Value;
                }
            }
        }

        // Called to clean up changes when the level is unloaded or the event is over
        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.maxScrap = oldMaxScrapCount;
            newLevel.minScrap = oldMinScrapCount;
            //newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            newLevel.maxEnemyPowerCount = oldMaxPowerCount;
            // Loop through all enemies in the new level
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                // Restore the original rarity of each enemy
                //newLevel.Enemies[i].rarity = rarities[i];

                // Check if the enemy is of type FlowermanAI
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<FlowermanAI>() != null)
                {
                    // Restore the original maximum count
                    newLevel.Enemies[i].enemyType.MaxCount = oldFlowerMax;
                    newLevel.Enemies[i].rarity = oldFlowerRarity;
                }

                // Check if the enemy is of type SpringManAI
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    // Restore the original maximum count
                    newLevel.Enemies[i].enemyType.MaxCount = oldCoilMax;
                    newLevel.Enemies[i].rarity = oldCoilRarity;
                }
            }
        }

        // Called to check if the event is valid for the current level
        public override bool IsValid(ref SelectableLevel newLevel)
        {
            // Flags to check if FlowermanAI and SpringManAI are found
            bool flowerManFound = false;
            bool springManFound = false;

            // Loop through all enemies in the new level
            foreach (var enemy in newLevel.Enemies)
            {
                // Check if the enemy is of type FlowermanAI
                if (enemy.enemyType.enemyPrefab.GetComponent<FlowermanAI>() != null)
                {
                    flowerManFound = true;
                }
                // Check if the enemy is of type SpringManAI
                else if (enemy.enemyType.enemyPrefab.GetComponent<SpringManAI>() != null)
                {
                    springManFound = true;
                }
            }

            // Return true if both FlowermanAI and SpringManAI are found, otherwise return false
            return flowerManFound && springManFound;
        }
    }
}
