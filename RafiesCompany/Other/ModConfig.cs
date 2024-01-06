using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafiesCompany.Other
{
    public class ModConfig
    {

        //public ConfigEntry<float> MovementHinderance { get; set; }
        //public ConfigEntry<float> SinkingSpeedMultiplier { get; set; }

        public ConfigEntry<bool> SprintMeter { get; set; }
        public ConfigEntry<bool> CompanyBuyingRateModifier { get; set; }
        public ConfigEntry<bool> RandomiseQuota { get; set; }
        public ConfigEntry<bool> DeadlineModifier { get; set; }
        public ConfigEntry<int> DeadlineDays { get; set; }
        public ConfigEntry<bool> StartingQuotaModifier { get; set; }
        public ConfigEntry<bool> QuotaModifier { get; set; }
        public ConfigEntry<int> QuotaIncrease { get; set; }
        public ConfigEntry<bool> PassiveCreditsModifier { get; set; }
        public ConfigEntry<bool> RandomiseStartingCredits { get; set; }
        //public ConfigEntry<int> PassiveCredits { get; set; }
        public ConfigEntry<bool> EnableScrapModification { get; set; }
        public ConfigEntry<int> MinScrapModifier { get; set; }
        public ConfigEntry<int> MaxScrapModifier { get; set; }
        public ConfigEntry<int> MinScrapValueModifier { get; set; }
        public ConfigEntry<int> MaxScrapValueModifier { get; set; }
        public ConfigEntry<bool> EnableEnemyModification { get; set; }
        public ConfigEntry<int> MaxInsideEnemyPowerModifier { get; set; }
        public ConfigEntry<int> MaxOutsideEnemyPowerModifier { get; set; }
        public ConfigEntry<int> MaxDaytimeEnemyPowerModifier { get; set; }
        //public ConfigEntry<float> InsideEnemySpawnCurve1 { get; set; }
        //public ConfigEntry<float> InsideEnemySpawnCurve2 { get; set; }
        //public ConfigEntry<float> OutsideEnemySpawnCurve1 { get; set; }
        //public ConfigEntry<float> OutsideEnemySpawnCurve2 { get; set; }
        //public ConfigEntry<float> OutsideEnemySpawnCurve3 { get; set; }
        //public ConfigEntry<float> DaytimeEnemySpawnCurve1 { get; set; }
        //public ConfigEntry<float> DaytimeEnemySpawnCurve2 { get; set; }
        public ConfigEntry<bool> EnableHazardModification { get; set; }
        public ConfigEntry<float> TurretSpawnCurve1 { get; set; }
        public ConfigEntry<float> TurretSpawnCurve2 { get; set; }
        public ConfigEntry<float> MineSpawnCurve1 { get; set; }
        public ConfigEntry<float> MineSpawnCurve2 { get; set; }
        public ConfigEntry<bool> EventHidden { get; set; }
        public ConfigEntry<int> FixedChance { get; set; }
        public ConfigEntry<bool> EnableFlowermanCoilEvent { get; set; }
        public ConfigEntry<int> FlowermanCoilEventFlowermanMax { get; set; }
        public ConfigEntry<int> FlowermanCoilEventCoilHeadMax { get; set; }
        public ConfigEntry<bool> EnableHoardingBugsEvent { get; set; }
        public ConfigEntry<int> HoardingBugsEventHoardingMax { get; set; }
        public ConfigEntry<bool> EnableJetpackEvent { get; set; }
        public ConfigEntry<int> JetpackEventJetpackCount { get; set; }
        public ConfigEntry<bool> EnableLandmineEvent { get; set; }
        public ConfigEntry<bool> EnableLittleGirlEvent { get; set; }
        public ConfigEntry<int> LittleGirlEventGirlMax {  get; set; }
        public ConfigEntry<int> LittleGirlEventMaskMax { get; set; }
        public ConfigEntry<bool> EnableRandomDeliveryEvent { get; set; }
        public ConfigEntry<bool> EnableSnareFleaEvent { get; set; }
        public ConfigEntry<int> SnareFleaEventSnareFleaMax { get; set; }
        public ConfigEntry<bool> EnableSpiderEvent { get; set; }
        public ConfigEntry<int> SpiderEventSpiderMax { get; set; }
        public ConfigEntry<bool> EnableThumperEvent { get; set; }
        public ConfigEntry<int> ThumperEventThumperMax { get; set; }
        public ConfigEntry<bool> EnableThunderEvent { get; set; }
        public ConfigEntry<bool> EnableTurretEvent { get; set; }
        public ConfigEntry<bool> EnableJesterEvent { get; set; }
        public ConfigEntry<int> JesterEventJesterMax { get; set; }

        public ConfigEntry<bool> EnableMaskedEvent { get; set; }
        public ConfigEntry<int> MaskedEventMaskedMax { get; set; }
        public ConfigEntry<bool> EnableNutcrackerEvent { get; set; }
        public ConfigEntry<int> NutcrackerEventNutcrackerMax { get; set; }
        public ConfigEntry<bool> EnableEclipseEvent { get; set; }
        public ConfigEntry<bool> EnableKinEvent { get; set; }



        public void InitConfigEntries()
        {
            //MovementHinderance = RafiesCompanyBase.instance.Config.Bind<float>(SectionGeneral, "MovementHinderance", 1.6f, "Defines how much movement speed is slowed in quicksand.");
            //SinkingSpeedMultiplier = RafiesCompanyBase.instance.Config.Bind<float>(SectionGeneral, "SinkingSpeedMultiplier", 0.15f, "The sinking speed multiplier in quicksand.");
            SprintMeter = RafiesCompanyBase.instance.Config.Bind<bool>("Misc", "SprintMeter", false, "Unlimited Sprint");
            CompanyBuyingRateModifier = RafiesCompanyBase.instance.Config.Bind<bool>("Modifier", "CompanyBuyingRate", true, "Allows the company buying rate to fluctuate.");
            DeadlineModifier = RafiesCompanyBase.instance.Config.Bind<bool>("Modifier", "DeadlineModifier", true, "Adds a random chance for the deadline to increase.");
            //DeadlineDays = RafiesCompanyBase.instance.Config.Bind<int>("Deadline", "DeadlineDays", 4, "Days until the deadline.");

            StartingQuotaModifier = RafiesCompanyBase.instance.Config.Bind<bool>("Modifier", "StartingQuotaModifier", true, "Add a random chance for the starting quota to fluctuate.");
            //EnableQuotaModification = RafiesCompanyBase.instance.Config.Bind<bool>("Quota", "EnableQuotaModification", false, "False sets to vanilla value.");
            //StartingQuota = RafiesCompanyBase.instance.Config.Bind<int>("Quota", "StartingQuota", 500, "Starting quota.");
            QuotaModifier = RafiesCompanyBase.instance.Config.Bind<bool>("Modifier", "QuotaModifier", true, "Enables setting an increased quota value for each deadline.");
            QuotaIncrease = RafiesCompanyBase.instance.Config.Bind<int>("Modifier", "QuotaIncreaseValue", 250, "How much the quota increases by.");

            PassiveCreditsModifier = RafiesCompanyBase.instance.Config.Bind<bool>("Modifier", "PassiveCreditsModifier", true, "Adds a chance to earn additional credits after each day.");
            RandomiseStartingCredits = RafiesCompanyBase.instance.Config.Bind<bool>("Modifier", "RandomiseStartingCredits", true, "Adds a chance for the starting credit amount to fluctuate.");
            //PassiveCredits = RafiesCompanyBase.instance.Config.Bind<int>("Credits", "PassiveCredits", 50, "Passive credits at the start of every level.");

            EnableScrapModification = RafiesCompanyBase.instance.Config.Bind<bool>("Scraps", "EnableScrapModification", false, "False sets to vanilla value.");
            MinScrapModifier = RafiesCompanyBase.instance.Config.Bind<int>("Scraps", "MinScrapModifier", 0, "Added to the minimum total number of scrap in a level.");
            MaxScrapModifier = RafiesCompanyBase.instance.Config.Bind<int>("Scraps", "MaxScrapModifier", 45, "Added to the maximum total number of scrap in a level.");
            MinScrapValueModifier = RafiesCompanyBase.instance.Config.Bind<int>("Scraps", "MinScrapValueModifier", 0, "Added to the minimum total value of scrap in a level.");
            MaxScrapValueModifier = RafiesCompanyBase.instance.Config.Bind<int>("Scraps", "MaxScrapValueModifier", 800, "Added to the maximum total value of scrap in a level.");

            EnableEnemyModification = RafiesCompanyBase.instance.Config.Bind<bool>("Enemies", "EnableEnemyModification", false, "False sets to vanilla value.");

            MaxInsideEnemyPowerModifier = RafiesCompanyBase.instance.Config.Bind<int>("Enemies", "MaxInsideEnemyPowerModifier", 5, "Added to the maximum enemy power for inside enemies, this controls the maximum level difficulty.");
            MaxOutsideEnemyPowerModifier = RafiesCompanyBase.instance.Config.Bind<int>("Enemies", "MaxOutsideEnemyPowerModifier", 5, "Added to the maximum enemy power for outside enemies, this controls the maximum level difficulty.");
            MaxDaytimeEnemyPowerModifier = RafiesCompanyBase.instance.Config.Bind<int>("Enemies", "MaxDaytimeEnemyPowerModifier", 5, "Added to the maximum enemy power for daytime enemies, this controls the maximum level difficulty.");

            //InsideEnemySpawnCurve1 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "InsideEnemySpawnCurveValue1", 0.1f, "Spawn curve for inside enemies, start of the day.");
            //InsideEnemySpawnCurve2 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "InsideEnemySpawnCurveValue2", 500.0f, "Spawn curve for inside enemies, midday.");

            //OutsideEnemySpawnCurve1 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "OutsideEnemySpawnCurve1", -30.0f, "Spawn curve for outside enemies, start of the day.");
            //OutsideEnemySpawnCurve2 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "OutsideEnemySpawnCurve2", -30.0f, "Spawn curve for outside enemies, 4 hours before launch.");
            //OutsideEnemySpawnCurve3 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "OutsideEnemySpawnCurve3", 10.0f, "Spawn curve for outside enemies, 3 hours before launch");

            //DaytimeEnemySpawnCurve1 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "DaytimeEnemySpawnCurve1", 7.0f, "Spawn curve for daytime enemies, start of the day.");
            //DaytimeEnemySpawnCurve2 = RafiesCompanyBase.instance.Config.Bind<float>("Enemies", "DaytimeEnemySpawnCurve2", 7.0f, "Spawn curve for daytime enemies, midday.");

            EnableHazardModification = RafiesCompanyBase.instance.Config.Bind<bool>("Hazards", "EnableHazardModification", false, "False sets to vanilla value.");
            TurretSpawnCurve1 = RafiesCompanyBase.instance.Config.Bind<float>("Hazards", "TurretSpawnCurve1", 0.0f, "Spawn curve for turrets, first value. Increase the number of turrets in levels.");
            TurretSpawnCurve2 = RafiesCompanyBase.instance.Config.Bind<float>("Hazards", "TurretSpawnCurve2", 10.0f, "Spawn curve for turrets, second value. Increase the number of turrets in levels.");
            MineSpawnCurve1 = RafiesCompanyBase.instance.Config.Bind<float>("Hazards", "MineSpawnCurve1", 0.0f, "Spawn curve for mines, first value. Increase the number of mines in levels.");
            MineSpawnCurve2 = RafiesCompanyBase.instance.Config.Bind<float>("Hazards", "MineSpawnCurve2", 70.0f, "Spawn curve for mines, second value. Increase the number of mines in levels.");

            EventHidden = RafiesCompanyBase.instance.Config.Bind<bool>("Events", "EventHidden", false, "Set to true to hide events in the chat.");
            FixedChance = RafiesCompanyBase.instance.Config.Bind<int>("Events", "EventChance", 15, "0-100, 0 means no events will happen, 100 means events are guaranteed to happen every day.");

            EnableFlowermanCoilEvent = RafiesCompanyBase.instance.Config.Bind<bool>("FlowermanCoilEvent", "EnableFlowermanCoilEvent", true, "Is the flowerman and coil event active.");
            FlowermanCoilEventFlowermanMax = RafiesCompanyBase.instance.Config.Bind<int>("FlowermanCoilEvent", "FlowermanCoilEventFlowermanMax", 3, "Maximum number of flowerman during the event (capped by power modifier).");
            FlowermanCoilEventCoilHeadMax = RafiesCompanyBase.instance.Config.Bind<int>("FlowermanCoilEvent", "FlowermanCoilEventCoilHeadMax", 3, "Maximum number of coil head during the event (capped by power modifier).");

            EnableHoardingBugsEvent = RafiesCompanyBase.instance.Config.Bind<bool>("HoardingBugsEvent", "EnableHoardingBugsEvent", true, "Is the hoarding bugs event active.");
            HoardingBugsEventHoardingMax = RafiesCompanyBase.instance.Config.Bind<int>("HoardingBugsEvent", "HoardingBugsEventHoardingMax", 10, "Maximum number of hoarding bugs during the event (capped by power modifier).");

            EnableJetpackEvent = RafiesCompanyBase.instance.Config.Bind<bool>("JetpackEvent", "EnableJetpackEvent", true, "Is the hoarding jetpack event active.");
            JetpackEventJetpackCount = RafiesCompanyBase.instance.Config.Bind<int>("JetpackEvent", "JetpackEventJetpackCount", 1, "Number of jetpacks during the jetpack event.");

            EnableLandmineEvent = RafiesCompanyBase.instance.Config.Bind<bool>("LandmineEvent", "EnableLandmineEvent", true, "Is the landmine event active.");

            EnableLittleGirlEvent = RafiesCompanyBase.instance.Config.Bind<bool>("LittleGirlEvent", "EnableLittleGirlEvent", true, "Is the little girl event active.");
            LittleGirlEventGirlMax = RafiesCompanyBase.instance.Config.Bind<int>("LittleGirlEvent", "LittleGirlEventGirlMax", 1, "Maximum number of ghost girls during the event (capped by power modifier).");
            LittleGirlEventMaskMax = RafiesCompanyBase.instance.Config.Bind<int>("LittleGirlEvent", "LittleGirlEventMaskMax", 3, "Maximum number of masked during the event (capped by power modifier).");

            EnableRandomDeliveryEvent = RafiesCompanyBase.instance.Config.Bind<bool>("RandomDeliveryEvent", "EnableRandomDeliveryEvent", true, "Is the random delivery event(Lost delivery) active.");

            EnableSnareFleaEvent = RafiesCompanyBase.instance.Config.Bind<bool>("SnareFleaEvent", "EnableSnareFleaEvent", true, "Is the snare flea event active.");
            SnareFleaEventSnareFleaMax = RafiesCompanyBase.instance.Config.Bind<int>("SnareFleaEvent", "SnareFleaEventSnareFleaMax", 6, "Maximum number of snare flea during the event (capped by power modifier).");

            EnableSpiderEvent = RafiesCompanyBase.instance.Config.Bind<bool>("SpiderEvent", "EnableSpiderEvent", true, "Is the spider event active.");
            SpiderEventSpiderMax = RafiesCompanyBase.instance.Config.Bind<int>("SpiderEvent", "SpiderEventSpiderMax", 5, "Maximum number of spiders during the event (capped by power modifier).");

            EnableThumperEvent = RafiesCompanyBase.instance.Config.Bind<bool>("ThumperEvent", "EnableThumperEvent", true, "Is the thumper event active.");
            ThumperEventThumperMax = RafiesCompanyBase.instance.Config.Bind<int>("ThumperEvent", "ThumperEventThumperMax", 5, "Maximum number of thumper during the event (capped by power modifier).");

            EnableThunderEvent = RafiesCompanyBase.instance.Config.Bind<bool>("ThunderEvent", "EnableThunderEvent", true, "Is the thunder event active.");

            EnableTurretEvent = RafiesCompanyBase.instance.Config.Bind<bool>("TurretEvent", "EnableTurretEvent", true, "Is the turret event active.");

            EnableJesterEvent = RafiesCompanyBase.instance.Config.Bind<bool>("JesterEvent", "EnableJesterEvent", false, "Is the jester event active.");
            JesterEventJesterMax = RafiesCompanyBase.instance.Config.Bind<int>("JesterEvent", "JesterEventJesterMax", 3, "Maximum number of jester during the event (capped by power modifier).");

            EnableMaskedEvent = RafiesCompanyBase.instance.Config.Bind<bool>("MaskedEvent", "EnableMaskedEvent", true, "Is the masked event active.");
            MaskedEventMaskedMax = RafiesCompanyBase.instance.Config.Bind<int>("MaskedEvent", "MaskEventMaskedMax", 4, "Maximum number of masked during the event (capped by power modifier).");

            EnableNutcrackerEvent = RafiesCompanyBase.instance.Config.Bind<bool>("NutcrackerEvent", "EnableNutcrackerEvent", true, "Is the nutcracker event active.");
            NutcrackerEventNutcrackerMax = RafiesCompanyBase.instance.Config.Bind<int>("NutcrackerEvent", "NutcrackerEventNutcrackerMax", 3, "Maximum number of nutcrackers during the event (capped by power modifier).");

            EnableEclipseEvent = RafiesCompanyBase.instance.Config.Bind<bool>("EclipseEvent", "EnableEclipseEvent", true, "Is the eclipse event active.");

            EnableKinEvent = RafiesCompanyBase.instance.Config.Bind<bool>("KinEvent", "EnableKinEvent", true, "Is the kin event active. If one player dies the ship votes to leave early.");

        }
    }
}