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
using System.Runtime.CompilerServices;
using System.Configuration;
using RafiesCompany;

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
        internal static ManualLogSource mls;
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
                //harmony.PatchAll(typeof(QuicksandTriggerPatch));
                harmony.PatchAll(typeof(ModifyBuyingRatePatch));
                //harmony.PatchAll(typeof(ModifyQuotaPatch));

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
        [HarmonyPostfix]
        static void QuotaAdjuster(TimeOfDay __instance)
        {
            RafiesCompanyBase.mls.LogInfo("QuotaAdjuster method is called.");
            // starting Quota modifier
            if (modConfig.StartingQuotaModifier.Value)
            {
                float randomValue = UnityEngine.Random.Range(0.00f, 1.00f);

                if (randomValue < 0.50f)
                {
                    int randomVariation = UnityEngine.Random.Range(-10, 150);
                    __instance.quotaVariables.startingQuota += randomVariation;

                    RafiesCompanyBase.mls.LogInfo($"Randomised quota by {randomVariation}, new rate: {TimeOfDay.Instance.quotaVariables.startingQuota}");
                    if (randomVariation > 0)
                    {
                        HUDManager.Instance.AddTextToChatOnServer($"<color=red>ATTENTION. The quota has increased by {randomVariation}. Good luck!</color>");
                    }
                    else
                    {
                        HUDManager.Instance.AddTextToChatOnServer($"<color=red>ATTENTION. The quota has decreased by {randomVariation}.</color>");
                    }

                }
                else
                {
                    RafiesCompanyBase.mls.LogInfo($"Random value: {randomValue}, No randomisation, current quota: {TimeOfDay.Instance.quotaVariables.startingQuota}");
                }
            }

            // starting credits modifier
            if (modConfig.RandomiseStartingCredits.Value)
            {
                float randomValue = UnityEngine.Random.Range(0.00f, 1.00f);
                if (randomValue < 0.10f)
                {
                    int randomVariation = UnityEngine.Random.Range(-50, 50);
                    __instance.quotaVariables.startingCredits += randomVariation;

                    RafiesCompanyBase.mls.LogInfo($"Randomised starting credits by {randomVariation}, new amount: {TimeOfDay.Instance.quotaVariables.startingCredits}");
                    if (randomVariation > 0)
                    {
                        HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. The company has deemed your team to be a valuable asset. Here are some extra starting credits!");
                    }
                    else
                    {
                        HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. We are reallocating some of your starting credits to a more successful team. Good luck!");
                    }
                }
                else
                {
                    RafiesCompanyBase.mls.LogInfo($"Random value: {randomValue}, No modifier, current credit amount: {TimeOfDay.Instance.quotaVariables.startingCredits}");
                }
            }
            // quota base increase modifier
            if (modConfig.QuotaModifier.Value)
            {
                __instance.quotaVariables.baseIncrease += modConfig.QuotaIncrease.Value;
            }
            // deadline modifier
            if (modConfig.DeadlineModifier.Value)
            {
                float randomValue = UnityEngine.Random.Range(0.01f, 0.99f);
                if (randomValue < 0.10f)
                {
                    int randomVariation = UnityEngine.Random.Range(1, 2);
                    __instance.quotaVariables.deadlineDaysAmount += randomVariation;
                    HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. The company has decided to give you more time to complete your quota.");
                    RafiesCompanyBase.mls.LogInfo($"Modified deadline by {randomVariation}, the new deadline is: {TimeOfDay.Instance.quotaVariables.deadlineDaysAmount}");
                }
                else
                {
                    RafiesCompanyBase.mls.LogInfo($"Random Value: {randomValue}, No deadline modifier, current deadline: {TimeOfDay.Instance.quotaVariables.deadlineDaysAmount}");
                }
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
                if (newLevel.currentWeather == LevelWeatherType.Eclipsed)
                {
                    newLevel.minScrap += 5;
                    newLevel.maxScrap += 15;

                    HUDManager.Instance.AddTextToChatOnServer($"DANGER: This moon is eclipsed, but higher value scrap lies inside!");
                    RafiesCompanyBase.mls.LogInfo($"Increased minScrap because the weather is Eclipse. New minScrap: {newLevel.minScrap}");
                }
                // Add credits
                Terminal terminal = FindObjectOfType<Terminal>();

                if (modConfig.PassiveCreditsModifier.Value)
                {
                    float randomValue = UnityEngine.Random.Range(0.01f, 0.99f);
                    if (randomValue < 0.10f)
                    {
                        int randomVariation = UnityEngine.Random.Range(10, 50);
                        terminal.groupCredits += randomVariation;
                        HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. An accounting error means you have more credits in your account. DO NOT SPEND THESE.");
                        RafiesCompanyBase.mls.LogInfo($"Modified passive credits by {randomVariation}, new passive credit amount is: {terminal.groupCredits}");
                    }
                    else
                    {
                        RafiesCompanyBase.mls.LogInfo($"Random Value: {randomValue}, No passive credit modifier, current passive credits: {terminal.groupCredits}");
                    }
                    //terminal.groupCredits += modConfig.PassiveCredits.Value;
                }

                int counter = 0;
                do
                {
                    gameEvent = eventCreator.GetRandomEventWithFixedChance(modConfig.FixedChance.Value);
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
                HUDManager.Instance.AddTextToChatOnServer($"<color=red>{gameEvent.GetEventName()}</color>");
            }

            gameEvent.OnLoadNewLevel(ref newLevel, modConfig);
            lastLevel = newLevel;

            return true;
        }
        static void ChangeLevelDifficulty(ref SelectableLevel newLevel)
        {
            if (difficultyModifiedLevels.Contains(newLevel.levelID))
                return;

            if (modConfig.EnableScrapModification.Value)
            {
                newLevel.minScrap += modConfig.MinScrapModifier.Value;
                newLevel.maxScrap += modConfig.MaxScrapModifier.Value;
                newLevel.minTotalScrapValue += modConfig.MinScrapValueModifier.Value;
                newLevel.maxTotalScrapValue += modConfig.MaxScrapValueModifier.Value;
            }


            if (modConfig.EnableHazardModification.Value)
            {
                foreach (var item in newLevel.spawnableMapObjects)
                {
                    if (item.prefabToSpawn.GetComponentInChildren<Turret>() != null)
                    {
                        item.numberToSpawn = new AnimationCurve(new Keyframe(0f, modConfig.TurretSpawnCurve1.Value), new Keyframe(1f, modConfig.TurretSpawnCurve2.Value));
                    }
                    else if (item.prefabToSpawn.GetComponentInChildren<Landmine>() != null)
                    {
                        item.numberToSpawn = new AnimationCurve(new Keyframe(0f, modConfig.MineSpawnCurve1.Value), new Keyframe(1f, modConfig.MineSpawnCurve2.Value));
                    }
                }
            }

            if (modConfig.EnableEnemyModification.Value)
            {
                //newLevel.enemySpawnChanceThroughoutDay = new AnimationCurve(new Keyframe(0, modConfig.InsideEnemySpawnCurve1.Value), new Keyframe(0.5f, modConfig.InsideEnemySpawnCurve2.Value));
                //newLevel.daytimeEnemySpawnChanceThroughDay = new AnimationCurve(new Keyframe(0, modConfig.DaytimeEnemySpawnCurve1.Value), new Keyframe(0.5f, modConfig.DaytimeEnemySpawnCurve2.Value));
                //newLevel.outsideEnemySpawnChanceThroughDay = new AnimationCurve(new Keyframe(0, modConfig.OutsideEnemySpawnCurve1.Value), new Keyframe(20f, modConfig.OutsideEnemySpawnCurve2.Value), new Keyframe(21f, modConfig.OutsideEnemySpawnCurve3.Value));

                newLevel.maxEnemyPowerCount += modConfig.MaxInsideEnemyPowerModifier.Value;
                newLevel.maxOutsideEnemyPowerCount += modConfig.MaxOutsideEnemyPowerModifier.Value;
                newLevel.maxDaytimeEnemyPowerCount += modConfig.MaxDaytimeEnemyPowerModifier.Value;
            }

            difficultyModifiedLevels.Add(newLevel.levelID);
        }
        
    }
}