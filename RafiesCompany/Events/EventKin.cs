using GameNetcodeStuff;
using HarmonyLib;
using RafiesCompany.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RafiesCompany.Events
{
    class KinEventCreator : BanditEventCreator
    {
        public override BanditEvent Create()
        {
            return new KinEvent();
        }
    }

    class KinEvent : BanditEvent
    {
        public override string GetEventName()
        {
            return "THE COMPANY ADVISES YOU TO NOT DIE.";
        }

        public override void OnLoadNewLevel(ref SelectableLevel newLevel, ModConfig configs)
        {
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayerServerRpc")]
        static void KinPostfix(int playerId, bool spawnBody, Vector3 bodyVelocity, int causeOfDeath, int deathAnimation)
        {
            RafiesCompanyBase.mls.LogInfo($"Player killed, kin event is active: ");

            // Access the timeOfDay property from the RafiesCompanyBase
            var timeOfDay = RafiesCompanyBase.instance.GetComponent<TimeOfDay>();

            // Set ship to leave early when a player is killed
            timeOfDay.votedShipToLeaveEarlyThisRound = true;
            timeOfDay.SetShipLeaveEarlyServerRpc();

            // Notify players in the chat about the ship leaving early
            HUDManager.Instance.AddTextToChatOnServer(
                "<color=red>A MEMBER OF YOUR TEAM HAS DIED. THE SHIP WILL NOW LEAVE EARLY.</color>");
        }
    }






}
