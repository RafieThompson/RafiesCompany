using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Events
{
    class JetpackDeliveryCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new JetpackDelivery();
        }
    }

    class JetpackDelivery : BanditEvent
    {
        public override string GetEventName()
        {
            return "DLSS1071-ARTIFICER Send their regards...";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            int quantity = configs.JetpackEventJetpackCount.Value;
            int jetPackId = 9;

            Terminal terminal = UnityEngine.Object.FindObjectOfType<Terminal>();
            for (int i = 0; i < quantity; i++)
            {
                terminal.orderedItemsFromTerminal.Add(jetPackId);
            }
        }
    }
}
