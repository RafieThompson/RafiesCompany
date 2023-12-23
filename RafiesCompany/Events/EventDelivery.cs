using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class RandomDeliveryEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new RandomDeliveryEvent();
        }
    }

    class RandomDeliveryEvent : BanditEvent
    {
        public override string GetEventName()
        {
            return "Something sparkles in the sky...";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            int randItemCount = UnityEngine.Random.Range(2, 9);
            for (int i = 0; i < randItemCount; i++)
            {
                int randomItemID = UnityEngine.Random.Range(0, 6);
                Terminal terminal = UnityEngine.Object.FindObjectOfType<Terminal>();
                terminal.orderedItemsFromTerminal.Add(randomItemID);
            }
        }
    }
}
