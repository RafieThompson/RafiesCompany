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

        public override string GetEventName()
        {
            return "You feel the burden of time...";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            //oldAnimationCurve = newLevel.enemySpawnChanceThroughoutDay;
            //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0, 500f));

            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                rarities.Add(newLevel.Enemies[i].rarity);
                newLevel.Enemies[i].rarity = 0;

                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<JesterAI>() != null)
                {
                    newLevel.Enemies[i].rarity = 999;

                    oldMaxCount = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.JesterEventJesterMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            //newLevel.enemySpawnChanceThroughoutDay = oldAnimationCurve;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                newLevel.Enemies[i].rarity = rarities[i];

                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<JesterAI>() != null)
                {
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
