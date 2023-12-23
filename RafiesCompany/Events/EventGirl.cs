using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        int oldRarity;
        int oldGirlMax;
        int oldMaskMax;
        public override string GetEventName()
        {
            return "Even the strongest mind can falter...";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<DressGirlAI>() != null)
                {
                    oldRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 100;

                    oldGirlMax = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.LittleGirlEventGirlMax.Value;
                }
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)  
                {
                    oldRarity = newLevel.Enemies[i].rarity;
                    newLevel.Enemies[i].rarity = 100;

                    oldMaskMax = newLevel.Enemies[i].enemyType.MaxCount;
                    newLevel.Enemies[i].enemyType.MaxCount = configs.LittleGirlEventMaskMax.Value;
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<DressGirlAI>() != null)
                {
                    newLevel.Enemies[i].rarity = oldRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldGirlMax;
                }
                else if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<MaskedPlayerEnemy>() != null)
                {
                    newLevel.Enemies[i].rarity = oldRarity;
                    newLevel.Enemies[i].enemyType.MaxCount = oldMaskMax;
                }
            }
        }
    }
}
