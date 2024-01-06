//using BepInEx.Logging;
//using HarmonyLib;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RafiesCompany.Other;

//namespace RafiesCompany.Patches
//{
//    [HarmonyPatch(typeof(TimeOfDay))]
//    internal class ModifyQuotaPatch
//    {
//        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Patches.ModifyQuotaPatch");

//        [HarmonyPatch(typeof(TimeOfDay), "Awake")]
//        [HarmonyPrefix]

//        static void RandomiseQuotaPatch(TimeOfDay __instance)
//        {
//            if (RafiesCompanyBase.modConfig.RandomiseQuota.Value)
//            {
//                RandomiseQuota();
//            }
//        }

//        private static void RandomiseQuota()
//        {
//            float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);

//            if (randomValue < 0.99f)
//            {
//                int randomVariation = UnityEngine.Random.Range(-50, 50);
//                TimeOfDay.Instance.quotaVariables.startingQuota += randomVariation;

//                mls.LogInfo($"Randomised quota by {randomVariation}, new rate: {TimeOfDay.Instance.quotaVariables.startingQuota}");
//                if (randomVariation > 0)
//                {
//                    string increaseMessage = GetRandomMessage(QuotaMessages.IncreaseMessages);
//                    HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. The quota has increased {increaseMessage}");
//                }
//                else
//                {
//                    string decreaseMessage = GetRandomMessage(QuotaMessages.DecreaseMessages);
//                    HUDManager.Instance.AddTextToChatOnServer($"ATTENTION. The quota has decreased {decreaseMessage}");
//                }

//            }
//            else
//            {
//                mls.LogInfo($"Random value: {randomValue}, No randomisation, current quota: {TimeOfDay.Instance.quotaVariables.startingQuota}");
//            }
//        }
//        private static string GetRandomMessage(string[] messages)
//        {
//            int randomIndex = UnityEngine.Random.Range(0, messages.Length);
//            return messages[randomIndex];
//        }
//    }
//}

