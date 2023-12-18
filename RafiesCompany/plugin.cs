using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RafiesCompany.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany
{
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
        }
    }
}
