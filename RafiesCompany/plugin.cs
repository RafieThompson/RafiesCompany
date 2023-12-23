using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RafiesCompany.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using RafiesCompany.Events;
using RafiesCompany.Other;
using UnityEngine;

namespace RafiesCompany
{
    [BepInPlugin(modGUID, modName, modVersion)]

    public class RafiesCompanyBase : BaseUnityPlugin
    {
        public static RafiesCompanyBase instance;
        private const string modGUID = "Bandit.RafiesCompany";
        private const string modName = "Rafie's Company";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        internal ManualLogSource mls;
        public static ModConfig modConfig = new ModConfig();
        public static bool loaded;



        public static BanditEvent gameEvent = null;
        internal static EventCreator eventCreator = new EventCreator();

        public static SelectableLevel lastLevel = null;

        public static List<int> difficultyModifiedLevels = new List<int>();


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            modConfig.InitConfigEntries();


            mls.LogInfo("Rafie's Company initialized");
            BanditEventList.AddBaseEvents(modConfig);

            try
            {
                harmony.PatchAll(typeof(RafiesCompanyBase));
                harmony.PatchAll(typeof(PlayerControllerBPatch));
                harmony.PatchAll(typeof(QuicksandTriggerPatch));
                harmony.PatchAll(typeof(ModifyBuyingRatePatch));

                mls.LogInfo("All patches applied successfully");
            }
            catch (Exception ex)
            {
                mls.LogError($"Failed to patch: {ex}");
            }
        }

        public void OnDestroy()
        {

        }

        void Update()
        {
        }

        [HarmonyPatch(typeof(TimeOfDay), "Awake")]
        [HarmonyPrefix]
        static void QuotaAjuster(TimeOfDay __instance)
        {
            if (modConfig.EnableQuotaModification.Value)
            {
                __instance.quotaVariables.startingQuota = modConfig.StartingQuota.Value;
            }

            if (modConfig.EnableCreditModification.Value)
            {
                __instance.quotaVariables.startingCredits = modConfig.StartingCredits.Value;
                __instance.quotaVariables.baseIncrease = modConfig.QuotaIncrease.Value;
            }

            if (modConfig.EnableDeadlineModification.Value)
            {
                __instance.quotaVariables.deadlineDaysAmount = modConfig.DeadlineDays.Value;
            }
        }

        [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.LoadNewLevel))]
        [HarmonyPrefix]
        static bool LoadNewLevel(ref SelectableLevel newLevel)
        {
            //ChangeLevelDifficulty(ref newLevel);

            // Clean and get the event for the game.
            if (gameEvent != null)
            {
                gameEvent.OnLoadNewLevelCleanup(ref lastLevel);
            }

            if (newLevel.sceneName == "CompanyBuilding")
            {
                gameEvent = new EventNone();
            }
            else
            {
                // Add credits
                Terminal terminal = FindObjectOfType<Terminal>();

                if (modConfig.EnableCreditModification.Value)
                {
                    terminal.groupCredits += modConfig.PassiveCredits.Value;
                }

                int counter = 0;
                do
                {
                    gameEvent = eventCreator.GetRandomEventWithWeight(modConfig.EventProbability.Value);
                    //gameEvent = eventCreator.GetEventInCustomOrder();

                    counter++;
                }
                while (!gameEvent.IsValid(ref newLevel) && counter < 4); //fail safe

                if (counter >= 4)
                {
                    gameEvent = new EventNone();
                }
            }

            if (modConfig.EventHidden.Value)
            {
                HUDManager.Instance.AddTextToChatOnServer($"<color=red>Level event:</color> <color=green>?</color>");
            }
            else
            {
                HUDManager.Instance.AddTextToChatOnServer($"<color=red>Level event:</color> <color=green>{gameEvent.GetEventName()}</color>");
            }

            gameEvent.OnLoadNewLevel(ref newLevel, modConfig);

            lastLevel = newLevel;

            return true;
        }
        //static void ChangeLevelDifficulty(ref SelectableLevel newLevel)
        //{
        //    if (difficultyModifiedLevels.Contains(newLevel.levelID))
        //        return;

        //    if (modConfig.EnableScrapModification.Value)
        //    {
        //        newLevel.minScrap += modConfig.MinScrapModifier.Value;
        //        newLevel.maxScrap += modConfig.MaxScrapModifier.Value;
        //        newLevel.minTotalScrapValue += modConfig.MinScrapValueModifier.Value;
        //        newLevel.maxTotalScrapValue += modConfig.MaxScrapValueModifier.Value;
        //    }


        //    if (modConfig.EnableHazardModification.Value)
        //    {
        //        foreach (var item in newLevel.spawnableMapObjects)
        //        {
        //            if (item.prefabToSpawn.GetComponentInChildren<Turret>() != null)
        //            {
        //                item.numberToSpawn = new AnimationCurve(new Keyframe(0f, modConfig.TurretSpawnCurve1.Value), new Keyframe(1f, modConfig.TurretSpawnCurve2.Value));
        //            }
        //            else if (item.prefabToSpawn.GetComponentInChildren<Landmine>() != null)
        //            {
        //                item.numberToSpawn = new AnimationCurve(new Keyframe(0f, modConfig.MineSpawnCurve1.Value), new Keyframe(1f, modConfig.MineSpawnCurve2.Value));
        //            }
        //        }
        //    }

        //    if (modConfig.EnableEnemyModification.Value)
        //    {
        //        newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0, modConfig.InsideEnemySpawnCurve1.Value), new Keyframe(0.5f, modConfig.InsideEnemySpawnCurve2.Value));
        //        newLevel.daytimeEnemySpawnChanceThroughDay = new AnimationCurve(new Keyframe(0, modConfig.DaytimeEnemySpawnCurve1.Value), new Keyframe(0.5f, modConfig.DaytimeEnemySpawnCurve2.Value));
        //        newLevel.outsideEnemySpawnChanceThroughDay = new AnimationCurve(new Keyframe(0, modConfig.OutsideEnemySpawnCurve1.Value), new Keyframe(20f, modConfig.OutsideEnemySpawnCurve2.Value), new Keyframe(21f, modConfig.OutsideEnemySpawnCurve3.Value));

        //        newLevel.maxEnemyPowerCount += modConfig.MaxInsideEnemyPowerModifier.Value;
        //        newLevel.maxOutsideEnemyPowerCount += modConfig.MaxOutsideEnemyPowerModifier.Value;
        //        newLevel.maxDaytimeEnemyPowerCount += modConfig.MaxDaytimeEnemyPowerModifier.Value;
        //    }

        //    difficultyModifiedLevels.Add(newLevel.levelID);
        // }
    }
}