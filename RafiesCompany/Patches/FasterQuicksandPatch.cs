using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Patches
{
    [HarmonyPatch(typeof(QuicksandTrigger))]
    internal class FasterQuicksandPatch
    {
        [HarmonyPatch("movementHinderance")]
        [HarmonyPostfix]

        static void MovementHinderancePatch(ref float ___movementHinderance)
        {
            ___movementHinderance = 3.0f;
        }

        [HarmonyPatch("sinkingSpeedMultiplier")]
        [HarmonyPostfix]
        static void SinkingSpeedMultiplierPatch(ref float ___sinkingSpeedMultiplier)
        {
            ___sinkingSpeedMultiplier = 1.0f;
        }


    }
}
