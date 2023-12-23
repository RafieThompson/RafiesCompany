using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Events
{
    public class EventNone : BanditEvent
    {
        public override string GetEventName()
        {
            return "None";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig config)
        {
        }
    }
}
