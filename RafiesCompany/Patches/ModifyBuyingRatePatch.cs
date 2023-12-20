using BepInEx.Logging;
using HarmonyLib;
using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]
    internal class ModifyBuyingRatePatch
    {
        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Patches.ModifyBuyingRatePatch");

        [HarmonyPatch(nameof(TimeOfDay.SetBuyingRateForDay))]
        [HarmonyPostfix]
        static void SetBuyingRatePatch(TimeOfDay __instance)
        {
            if (ModConfig.CompanyBuyingRate.Value)
            {
                RandomiseBuyingRate();
                StartOfRound.Instance.SyncCompanyBuyingRateServerRpc();
            }
        }
        private static void RandomiseBuyingRate()
        {
            float randomValue = UnityEngine.Random.value;

            // 15% chance to raise or lower the buying rate
            if (randomValue < 0.15f)
            {
                // range of buying rate alteration, between -20% and 20%
                float randomVariation = UnityEngine.Random.Range(-0.20f, 0.20f);
                StartOfRound.Instance.companyBuyingRate += randomVariation;

                mls.LogInfo($"Randomized buying rate by {randomVariation}, new rate: {StartOfRound.Instance.companyBuyingRate}");
                if (randomVariation > 0.00f)
                {
                    string increaseMessage = GetRandomMessage(CompanyBuyingRateMessages.IncreaseMessages);
                    HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. Today's buying rate has increased {increaseMessage}");
                }
                else
                {
                    string decreaseMessage = GetRandomMessage(CompanyBuyingRateMessages.DecreaseMessages);
                    HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. Today's buying rate has decreased {decreaseMessage}");
                }
            }
            else
            {
                mls.LogInfo($"Random value: {randomValue}, No randomization, current rate: {StartOfRound.Instance.companyBuyingRate}");
            }
        }
        private static string GetRandomMessage(string[] messages)
        {
            int randomIndex = UnityEngine.Random.Range(0, messages.Length);
            return messages[randomIndex];
        }
    }
}































//            [HarmonyPatch(typeof(Terminal))]
//        internal class TerminalCompanyBuyingRatePatch
//        {
//            [HarmonyPatch("TextPostProcess")]
//            [HarmonyPostfix]
//            static void TextPostProcessPatch(ref string modifiedDisplayText, TerminalNode node)
//            {
//                if (ModConfig.CompanyBuyingRate.Value)
//                {
//                    modifiedDisplayText = modifiedDisplayText.Replace("[companyBuyingPercent]", $"{Mathf.RoundToInt(sharedBuyingRate * 100f)}%");
//                }
//            }
//        }

//        [HarmonyPatch(typeof(StartOfRound))]

//        internal class StartOfRoundCompanyBuyingRatePatch
//        {
//            [HarmonyPatch("ResetShip")]
//            [HarmonyPrefix]
//            static void ResetShipPatch()
//            {
//                if (ModConfig.CompanyBuyingRate.Value)
//                {
//                    StartOfRound.Instance.companyBuyingRate = sharedBuyingRate;
//                    mls.LogInfo($"companybuyingrate applied in ResetShip: {sharedBuyingRate}");
//                }
//            }

//            [HarmonyPatch("SyncCompanyBuyingRateServerRpc")]
//            [HarmonyPrefix]
//            static void SyncCompanyBuyingRateServerRpcPatch(ref float sharedBuyingRate)
//            {
//                if (ModConfig.CompanyBuyingRate.Value)
//                {
//                    StartOfRound.Instance.companyBuyingRate = sharedBuyingRate;
//                }
//            }

//            [HarmonyPatch("SyncCompanyBuyingRateClientRpc")]
//            [HarmonyPrefix]
//            static void SyncCompanyBuyingRateClientRpcPatch(ref float sharedBuyingRate)
//            {
//                if (ModConfig.CompanyBuyingRate.Value)
//                {
//                    StartOfRound.Instance.companyBuyingRate = sharedBuyingRate;
//                }
//            }
//        }

//        [HarmonyPatch(typeof(DepositItemsDesk))]
//        internal class DepositItemsDeskCompanyBuyingRatePatch
//        {
//            [HarmonyPatch("SellItemsOnServer")]
//            [HarmonyPrefix]
//            static void SellItemsOnServerPatch()
//            {
//                if (ModConfig.CompanyBuyingRate.Value)
//                {
//                    StartOfRound.Instance.companyBuyingRate = ModifyBuyingRatePatch.sharedBuyingRate;
//                }
//            }
//            [HarmonyPatch("SellItemsClientRpc")]
//            [HarmonyPrefix]
//            static void SellItemsOnClientPatch()
//            {
//                if (ModConfig.CompanyBuyingRate.Value)
//                {
//                    StartOfRound.Instance.companyBuyingRate = ModifyBuyingRatePatch.sharedBuyingRate;
//                }

//            }

//        }





//        static float CalculateCustomBuyingRate()
//        {
//            float randomVariation = UnityEngine.Random.Range(0.9f, 1.1f);

//            float modifiedBuyingRate = StartOfRound.Instance.companyBuyingRate * randomVariation;

//            return modifiedBuyingRate;
//        }


//    }
//}









//        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Patches.ModifyBuyingRatePatch");

//        [HarmonyPatch("SetBuyingRateForDay")]
//        [HarmonyPostfix]
//        static void ModifyCompanyBuyingRate(ref float ___companyBuyingRate)
//        {
//            float minRandomValue = 0.95f; // 95%
//            float maxRandomValue = 1.05f; // 105%

//            float randomMultiplier = UnityEngine.Random.Range(minRandomValue, maxRandomValue);
//            bool buyingrate = ModConfig.CompanyBuyingRate.Value;
//            if (buyingrate)
//            {
//                ___companyBuyingRate *= randomMultiplier;
//                mls.LogInfo($"buyingrate changed to: {___companyBuyingRate}");
//            }

//        }


//    }
//}
