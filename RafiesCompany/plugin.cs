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

        public static void InitConfigEntries(ConfigFile configFile)
        {
            MovementHinderance = configFile.Bind(SectionGeneral, "MovementHinderance", 1.6f, "Defines how much movement speed is slowed in quicksand.");
            SinkingSpeedMultiplier = configFile.Bind(SectionGeneral, "SinkingSpeedMultiplier", 0.15f, "The sinking speed multiplier in quicksand.");
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
                mls.LogInfo("RafiesCompanyBase patch applied successfully");
            }
            catch (Exception ex)
            {
                mls.LogError($"Failed to patch RafiesCompanyBase: {ex}");
            }

            try
            {
                harmony.PatchAll(typeof(PlayerControllerBPatch));
                mls.LogInfo("PlayerControllerBPatch applied successfully");
            }
            catch (Exception ex)
            {
                mls.LogError($"Failed to patch PlayerControllerBPatch: {ex}");
            }

            try
            {
                harmony.PatchAll(typeof(QuicksandTriggerPatch));
                mls.LogInfo("QuicksandTriggerPatch applied successfully");
            }
            catch (Exception ex)
            {
                mls.LogError($"Failed to patch QuicksandTriggerPatch: {ex}");
            }

                ModConfig.InitConfigEntries(Config);
        }

        void Update()
        {
            // Access the configured values
            float currentMovementHinderance = ModConfig.MovementHinderance.Value;
            float currentSinkingSpeedMultiplier = ModConfig.SinkingSpeedMultiplier.Value;

            // Use the values in your mod logic
            // ...

            // For debugging or information purposes, you can log the values
            mls.LogInfo($"Current Movement Hinderance: {currentMovementHinderance}");
            mls.LogInfo($"Current Sinking Speed Multiplier: {currentSinkingSpeedMultiplier}");
        }
    }
}
