using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("Update")]  /*nameof(PlayerControllerB.(FINDHERE)*/
        [HarmonyPostfix]
        static void infiniteSprintPatch(ref float ___sprintMeter)
        {
            bool unlimitedSprint = ModConfig.SprintMeter.Value;
            if (unlimitedSprint)
            {
                ___sprintMeter = 1f;
            }
        }

    }
}
