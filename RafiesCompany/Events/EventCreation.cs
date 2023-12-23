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

        private int customCurrentIndex = 0;
        private int currentIndex = 0;

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
            int randomValue = random.Next(0, 10);
            if (randomValue < eventProbability)
            {
                return new EventNone();
            }

            return GetRandomEvent();
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
