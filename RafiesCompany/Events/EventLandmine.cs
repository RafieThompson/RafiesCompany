using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class LandmineEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new LandmineEvent();
        }
    }

    class LandmineEvent : BanditEvent
    {
        private AnimationCurve oldCurve;

        public override string GetEventName()
        {
            return "WARNING: INCREASED DEFENCE LEVELS DETECTED.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
            foreach (var item in newLevel.spawnableMapObjects)
            {
                if (item.prefabToSpawn.GetComponentInChildren<Landmine>() != null)
                {
                    oldCurve = item.numberToSpawn;
                    item.numberToSpawn = new AnimationCurve(new UnityEngine.Keyframe(0f, 60f), new UnityEngine.Keyframe(1f, 70f));
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            foreach (var item in newLevel.spawnableMapObjects)
            {
                if (item.prefabToSpawn.GetComponentInChildren<Landmine>() != null)
                {
                    item.numberToSpawn = oldCurve;
                }
            }
        }
    }
}
