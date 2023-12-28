using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class TurretEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new TurretEvent();
        }
    }

    class TurretEvent : BanditEvent
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
                if (item.prefabToSpawn.GetComponentInChildren<Turret>() != null)
                {
                    oldCurve = item.numberToSpawn;
                    item.numberToSpawn = new AnimationCurve(new Keyframe(0f, 50f), new Keyframe(1f, 20));
                }
            }
        }

        public override void OnLoadNewLevelCleanup(ref SelectableLevel newLevel)
        {
            foreach (var item in newLevel.spawnableMapObjects)
            {
                if (item.prefabToSpawn.GetComponentInChildren<Turret>() != null)
                {
                    item.numberToSpawn = oldCurve;
                }
            }
        }
    }
}
