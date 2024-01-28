//using RafiesCompany.Other;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace RafiesCompany.Events
//{
//    class ShotgunDeliveryCreator : BanditEventCreator
//    {
//        public override BanditEvent Create()
//        {
//            return new ShotgunDelivery();
//        }
//    }

//    class ShotgunDelivery : BanditEvent
//    {
//        public override string GetEventName()
//        {
//            return "TODAY IS YOUR LUCKY DAY!";
//        }

//        private int ShotgunItemId
//        {
//            get
//            {
//                string shotgunItemName = "Shotgun";
//                Item shotgunItem = StartOfRound.Instance.allItemsList.itemsList.FirstOrDefault(item => item.name == shotgunItemName);

//                if (shotgunItem == null)
//                {
//                    RafiesCompanyBase.mls.LogWarning($"Shotgun item not found in the list of items.");
//                    return -1; // or any other default value representing "not found"
//                }
//                else
//                {
//                    // Assign a specific ID (e.g., 10) to the shotgun item.
//                    int shotgunItemId = 10;

//                    RafiesCompanyBase.mls.LogInfo($"Shotgun item found with ID: {shotgunItemId}");
//                    return shotgunItemId;
//                }
//            }
//        }

//        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
//        {
//            int quantity = configs.ShotgunEventShotgunCount.Value;

//            Terminal terminal = UnityEngine.Object.FindObjectOfType<Terminal>();
//            for (int i = 0; i < quantity; i++)
//            {
//                terminal.orderedItemsFromTerminal.Add(ShotgunItemId);
//            }
//        }
//    }
//}
