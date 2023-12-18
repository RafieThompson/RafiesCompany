using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;

namespace RafiesCompany.Patches
{
    [HarmonyPatch(typeof(QuicksandTrigger))]
    internal class QuicksandTriggerPatch
    {
        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Patches.FasterQuicksandPatch");
        private static bool hasLoggedSinking = false;

        [HarmonyPatch("OnTriggerStay")]
        [HarmonyPostfix]
        static void ModifyQuicksandEffects(QuicksandTrigger __instance)
        {
            if (__instance.sinkingLocalPlayer)
            {
                // Modify movementHinderance and sinkingSpeedMultiplier when sinkingLocalPlayer is true
                __instance.movementHinderance = 5.0f;
                __instance.sinkingSpeedMultiplier = 0.99f;

                mls.LogInfo($"MovementHinderance changed to: {__instance.movementHinderance}");
                mls.LogInfo($"SinkingSpeedMultiplier changed to: {__instance.sinkingSpeedMultiplier}");

                hasLoggedSinking = true;
            }
            else if (!__instance.sinkingLocalPlayer)
            {
                hasLoggedSinking = false;
            }
        }
    }
}





