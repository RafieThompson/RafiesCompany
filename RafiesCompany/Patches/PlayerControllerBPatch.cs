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
            // Access the static field directly using the class name
            bool unlimitedSprint = RafiesCompanyBase.modConfig.SprintMeter.Value;

            if (unlimitedSprint)
            {
                ___sprintMeter = 1f;
            }
        }

    }
}
