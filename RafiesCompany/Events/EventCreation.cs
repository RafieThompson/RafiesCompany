using BepInEx.Logging;
using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Events
{
    public abstract class BanditEventCreator
    {
        public abstract BanditEvent Create();
    }

    public abstract class BanditEvent
    {
        public abstract string GetEventName();
        public abstract void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs);
        public virtual void OnLoadNewLevelCleanup(ref SelectableLevel newLevel) { }

        public virtual bool IsValid(ref SelectableLevel newLevel) { return true; }
    }
    public class EventCreator
    {
        List<BanditEventCreator> customEventOrder = new List<BanditEventCreator> { };
        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Events.EventCreation");
        public BanditEvent GetRandomEventWithFixedChance(int fixedChance)
        {
            Random random = new Random();
            int randomValue = random.Next(100); // Generating a random value between 0 and 99
            mls.LogInfo($"Random Value: {randomValue}, Fixed Chance: {fixedChance}");

            if (randomValue < fixedChance)
            {
                // If the random value is less than the fixed chance, choose a random event
                List<BanditEventCreator> events = BanditEventList.GetEvents();
                BanditEventCreator randomEvent = events[random.Next(events.Count)];
                mls.LogInfo($"Selected Event: {randomEvent.GetType().Name}");
                return randomEvent.Create();
            }

            // If the random value is greater than or equal to the fixed chance, return a default event
            mls.LogInfo("No Event Selected. Choosing default event (EventNone).");
            return new EventNone();
        }

    }

}
