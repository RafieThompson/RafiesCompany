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
            if (RafiesCompanyBase.modConfig.CompanyBuyingRateModifier.Value)
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

                mls.LogInfo($"Randomised buying rate by {randomVariation}, new rate: {StartOfRound.Instance.companyBuyingRate}");
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

                // this is just here for reference
                List<Item> allItems = StartOfRound.Instance.allItemsList.itemsList;
                foreach (var item in allItems)
                {
                    mls.LogInfo($"Found item: {item.name}");
                }
            }
        }
        private static string GetRandomMessage(string[] messages)
        {
            int randomIndex = UnityEngine.Random.Range(0, messages.Length);
            return messages[randomIndex];
        }
    }
}
