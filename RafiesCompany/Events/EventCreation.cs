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
        List<BanditEventCreator> customEventOrder = new List<BanditEventCreator> { new JesterEventCreator(), new JesterEventCreator() };
        private static ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource("RafiesCompany.Events.EventCreation");
        private int customCurrentIndex = 0;
        private int currentIndex = 0;

        private Dictionary<BanditEventCreator, int> eventWeights = new Dictionary<BanditEventCreator, int>
    {
        {new BrackenCoilEventCreator(), 5 },
        { new RandomDeliveryEventCreator(), 10 },
        { new LittleGirlEventCreator(), 1 },
        { new HoardingEventCreator(), 10 },
        { new JesterEventCreator(), 1 },
        { new JetpackDeliveryCreator(), 4 },
        { new LandmineEventCreator(), 10 },
        { new SnareFleaEventCreator(), 15 },
        { new SpiderEventCreator(), 10 },
        { new ThumperEventCreator(), 5 },
        { new ThunderEventCreator(), 10 },
        { new TurretEventCreator(), 10 },
    };

        public BanditEvent GetRandomEvent()
        {
            List<BanditEventCreator> events = BanditEventList.GetEvents();
            Random random = new Random();
            BanditEventCreator randomEvent = events[random.Next(events.Count)];
            return randomEvent.Create();
        }

        public BanditEvent GetRandomEventWithWeight(int eventProbability)
        {

            Random random = new Random();
            int totalWeight = eventWeights.Values.Sum();
            int randomValue = random.Next(totalWeight);

            // Adjust randomValue based on eventProbability
            randomValue -= eventProbability;

            mls.LogInfo($"Total Weight: {totalWeight}");
            mls.LogInfo($"Adjusted RandomValue: {randomValue + eventProbability}");
            foreach (var eventWeight in eventWeights)
            {
                randomValue -= eventWeight.Value;
                mls.LogInfo($"Checking {eventWeight.Key.GetType().Name} with Weight {eventWeight.Value}. Remaining RandomValue: {randomValue + eventProbability}");

                if (randomValue <= 0)
                {
                    mls.LogInfo($"Selected Event: {eventWeight.Key.GetType().Name}");
                    return eventWeight.Key.Create();
                }
            }

            // If no event is selected, return a default event (EventNone, for example)
            mls.LogInfo("No Event Selected. Choosing default event (EventNone).");
            return new EventNone();
        }


        public BanditEvent GetEventInOrder()
        {
            List<BanditEventCreator> events = BanditEventList.GetEvents();
            BanditEventCreator currentEvent = events[currentIndex];
            currentIndex++;
            if (currentIndex >= events.Count)
            {
                currentIndex = 0;
            }
            return currentEvent.Create();
        }

        public BanditEvent GetEventInCustomOrder()
        {
            BanditEventCreator currentEvent = customEventOrder[customCurrentIndex];
            customCurrentIndex++;
            if (customCurrentIndex >= customEventOrder.Count)
            {
                customCurrentIndex = 0;
            }
            return currentEvent.Create();
        }
    }
}
