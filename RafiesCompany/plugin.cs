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

namespace RafiesCompany
{
    public class ModConfig
    {
        public const string SectionGeneral = "General";

        public static ConfigEntry<float> MovementHinderance { get; private set; }
        public static ConfigEntry<float> SinkingSpeedMultiplier { get; private set; }

        public static ConfigEntry<Boolean> SprintMeter { get; private set; }
        public static ConfigEntry<Boolean> CompanyBuyingRate { get; private set; }

        public static void InitConfigEntries(ConfigFile configFile)
        {
            MovementHinderance = configFile.Bind(SectionGeneral, "MovementHinderance", 1.6f, "Defines how much movement speed is slowed in quicksand.");
            SinkingSpeedMultiplier = configFile.Bind(SectionGeneral, "SinkingSpeedMultiplier", 0.15f, "The sinking speed multiplier in quicksand.");
            SprintMeter = configFile.Bind(SectionGeneral, "SprintMeter", true, "Unlimited Sprint");
            CompanyBuyingRate = configFile.Bind(SectionGeneral, "CompanyBuyingRate", true, "Add random modifier to company buying rate");
        }
    }

    [BepInPlugin(modGUID, modName, modVersion)]

    public class RafiesCompanyBase :BaseUnityPlugin
    {
        private const string modGUID = "Bandit.RafiesCompany";
        private const string modName = "Rafie's Company";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static RafiesCompanyBase Instance;

        internal ManualLogSource mls;
        public bool CurrentSprintMeter => ModConfig.SprintMeter.Value;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Rafie's Company initialized");

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


            ModConfig.InitConfigEntries(Config);
        }

        void Update()
        {
            // Access the configured values
            float currentMovementHinderance = ModConfig.MovementHinderance.Value;
            float currentSinkingSpeedMultiplier = ModConfig.SinkingSpeedMultiplier.Value;
            bool currentSprintMeter = ModConfig.SprintMeter.Value;
            bool currentModifyBuyingRate = ModConfig.CompanyBuyingRate.Value;   

            // For debugging or information purposes, you can log the values
            mls.LogInfo($"Current Movement Hinderance: {currentMovementHinderance}");
            mls.LogInfo($"Current Sinking Speed Multiplier: {currentSinkingSpeedMultiplier}");
            mls.LogInfo($"Unlimited Sprint?: {currentSprintMeter}");
        }
    }
}
