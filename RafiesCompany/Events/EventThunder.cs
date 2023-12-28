using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Events
{
    class ThunderEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new ThunderEvent();
        }
    }

    class ThunderEvent : BanditEvent
    {
        LevelWeatherType oldType;
        bool oldOverrideWeather;

        public override string GetEventName()
        {
            return "WEATHER ANOMALY DETECTED.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            oldType = newLevel.overrideWeatherType;
            oldOverrideWeather = newLevel.overrideWeather;

            newLevel.currentWeather = LevelWeatherType.Stormy;
            newLevel.overrideWeather = false;
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.currentWeather = oldType;
            newLevel.overrideWeather = oldOverrideWeather;
        }
    }
}
