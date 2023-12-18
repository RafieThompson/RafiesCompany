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
            float movementHinderance = ModConfig.MovementHinderance.Value;
            float sinkingSpeedMultiplier = ModConfig.SinkingSpeedMultiplier.Value;

            if (__instance.sinkingLocalPlayer)
            {
                // Modify movementHinderance and sinkingSpeedMultiplier when sinkingLocalPlayer is true
                __instance.movementHinderance = movementHinderance;
                __instance.sinkingSpeedMultiplier = sinkingSpeedMultiplier;

                mls.LogInfo($"MovementHinderance changed to: {__instance.movementHinderance}");
                mls.LogInfo($"SinkingSpeedMultiplier changed to: {__instance.sinkingSpeedMultiplier}");

            }
        }
    }
}





