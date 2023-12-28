//using HarmonyLib;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BepInEx.Logging;
//using GameNetcodeStuff;
//using UnityEngine;

//namespace RafiesCompany.Patches
//{
//    [HarmonyPatch(typeof(QuicksandTrigger))]
//    internal class QuicksandTriggerPatch
//    {
//        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Patches.FasterQuicksandPatch");
//        private static float lastLogTime = 0f;
//        private static float logInterval = 1f;

//        [HarmonyPatch("OnTriggerStay")]
//        [HarmonyPostfix]
//        static void ModifyQuicksandEffects(QuicksandTrigger __instance)
//        {
//            // Access the static fields directly using the class name
//            float movementHinderance = RafiesCompanyBase.modConfig.MovementHinderance.Value;
//            float sinkingSpeedMultiplier = RafiesCompanyBase.modConfig.SinkingSpeedMultiplier.Value;

//            if (__instance.sinkingLocalPlayer)
//            {
//                __instance.movementHinderance = movementHinderance;
//                __instance.sinkingSpeedMultiplier = sinkingSpeedMultiplier;

//                if (Time.time - lastLogTime >= logInterval)
//                {
//                    mls.LogInfo($"MovementHinderance changed to: {__instance.movementHinderance}");
//                    mls.LogInfo($"SinkingSpeedMultiplier changed to: {__instance.sinkingSpeedMultiplier}");

//                    lastLogTime = Time.time;
//                }
//            }
//        }
//    }
//}





