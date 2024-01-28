using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Events
{
    class EclipseEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new EclipseEvent();
        }
    }

    class EclipseEvent : BanditEvent
    {
        LevelWeatherType oldType;
        bool oldOverrideWeather;
        int oldMinScrapCount;
        int oldMaxScrapCount;

        public override string GetEventName()
        {
            return "WEATHER ANOMALY DETECTED.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            oldMinScrapCount = newLevel.minScrap;
            oldMaxScrapCount = newLevel.maxScrap;
            oldType = newLevel.overrideWeatherType;
            oldOverrideWeather = newLevel.overrideWeather;

            newLevel.currentWeather = LevelWeatherType.Eclipsed;
            newLevel.overrideWeather = false;
            newLevel.minScrap += 1;
            newLevel.maxScrap += 5;
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            newLevel.currentWeather = oldType;
            newLevel.overrideWeather = oldOverrideWeather;
            newLevel.minScrap = oldMaxScrapCount;
            newLevel.maxScrap = oldMaxScrapCount;
        }
    }
}
