using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Events
{
    internal static class BanditEventList
    {
        private static List<BanditEventCreator> events = new List<BanditEventCreator>();

        public static List<BanditEventCreator> GetEvents()
        {
            return events;
        }

        public static void AddEvent(BanditEventCreator BanditEvent)
        {
            events.Add(BanditEvent);
        }

        internal static void AddBaseEvents(ModConfig configs)
        {
            if (configs.EnableFlowermanCoilEvent.Value)
            {
                AddEvent(new BrackenCoilEventCreator());
            }
            if (configs.EnableHoardingBugsEvent.Value)
            {
                AddEvent(new HoardingEventCreator());
            }
            if (configs.EnableJetpackEvent.Value)
            {
                AddEvent(new JetpackDeliveryCreator());
            }
            if (configs.EnableLandmineEvent.Value)
            {
                AddEvent(new LandmineEventCreator());
            }
            if (configs.EnableLittleGirlEvent.Value)
            {
                AddEvent(new LittleGirlEventCreator());
            }
            if (configs.EnableRandomDeliveryEvent.Value)
            {
                AddEvent(new RandomDeliveryEventCreator());
            }
            if (configs.EnableSnareFleaEvent.Value)
            {
                AddEvent(new SnareFleaEventCreator());
            }
            if (configs.EnableSpiderEvent.Value)
            {
                AddEvent(new SpiderEventCreator());
            }
            if (configs.EnableThumperEvent.Value)
            {
                AddEvent(new ThumperEventCreator());
            }
            if (configs.EnableThunderEvent.Value)
            {
                AddEvent(new ThunderEventCreator());
            }
            if (configs.EnableTurretEvent.Value)
            {
                AddEvent(new TurretEventCreator());
            }
            if (configs.EnableJesterEvent.Value)
            {
                AddEvent(new JesterEventCreator());
            }
            if (configs.EnableMaskedEvent.Value)
            {
                AddEvent(new MaskedEventCreator());
            }
            if (configs.EnableNutcrackerEvent.Value)
            {
                AddEvent(new NutcrackerEventCreator());
            }
            if (configs.EnableEclipseEvent.Value)
            {
                AddEvent(new EclipseEventCreator());
            }
            if (configs.EnableKinEvent.Value)
            {
                AddEvent(new KinEventCreator());
            }
        }
    }
}
