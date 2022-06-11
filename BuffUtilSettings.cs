using System.Windows.Forms;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;

namespace BuffUtil
{
    public class BuffUtilSettings : ISettings
    {
        public BuffUtilSettings()
        {
            Enable = new ToggleNode(false);
            BloodRage = new ToggleNode(false);
            BloodRageKey = new HotkeyNode(Keys.E);
            BloodRageConnectedSkill = new RangeNode<int>(1, 1, 13);
            BloodRageMaxHP = new RangeNode<int>(100, 0, 100);
            BloodRageMaxMP = new RangeNode<int>(100, 0, 100);

            SteelSkin = new ToggleNode(false);
            SteelSkinKey = new HotkeyNode(Keys.W);
            SteelSkinConnectedSkill = new RangeNode<int>(1, 1, 13);
            SteelSkinMaxHP = new RangeNode<int>(90, 0, 100);

            ImmortalCall = new ToggleNode(false);
            ImmortalCallKey = new HotkeyNode(Keys.T);
            ImmortalCallConnectedSkill = new RangeNode<int>(1, 1, 13);
            ImmortalCallMaxHP = new RangeNode<int>(50, 0, 100);

            MoltenShell = new ToggleNode(false);
            MoltenShellKey = new HotkeyNode(Keys.Q);
            MoltenShellConnectedSkill = new RangeNode<int>(1, 1, 13);
            MoltenShellMaxHP = new RangeNode<int>(50, 0, 100);

            PhaseRun = new ToggleNode(false);
            PhaseRunKey = new HotkeyNode(Keys.R);
            PhaseRunConnectedSkill = new RangeNode<int>(1, 1, 13);
            PhaseRunMaxHP = new RangeNode<int>(90, 0, 100);
            PhaseRunMinMoveTime = new RangeNode<int>(0, 0, 5000);
            PhaseRunMinBVStacks = new RangeNode<int>(0, 0, 10);

            WitheringStep = new ToggleNode(false);
            WitheringStepKey = new HotkeyNode(Keys.R);
            WitheringStepConnectedSkill = new RangeNode<int>(1, 1, 13);
            WitheringStepMaxHP = new RangeNode<int>(90, 0, 100);
            WitheringStepMinMoveTime = new RangeNode<int>(0, 0, 5000);

            BladeFlurry = new ToggleNode(false);
            BladeFlurryMinCharges = new RangeNode<int>(6, 1, 6);
            BladeFlurryUseLeftClick = new ToggleNode(false);
            BladeFlurryWaitForInfused = new ToggleNode(true);

            ScourgeArrow = new ToggleNode(false);
            ScourgeArrowMinCharges = new RangeNode<int>(5, 1, 6);
            ScourgeArrowUseLeftClick = new ToggleNode(false);
            ScourgeArrowWaitForInfused = new ToggleNode(true);

            //my added
            Wrath = new ToggleNode(false);
            WrathKey = new HotkeyNode(Keys.W);
            WrathConnectedSkill = new RangeNode<int>(1, 1, 13);

            Zealotry = new ToggleNode(false);
            ZealotryKey = new HotkeyNode(Keys.W);
            ZealotryConnectedSkill = new RangeNode<int>(1, 1, 13);

            Hatred = new ToggleNode(false);
            HatredKey = new HotkeyNode(Keys.W);
            HatredConnectedSkill = new RangeNode<int>(1, 1, 13);

            Haste = new ToggleNode(false);
            HasteKey = new HotkeyNode(Keys.W);
            HasteConnectedSkill = new RangeNode<int>(1, 1, 13);

            Discipline = new ToggleNode(false);
            DisciplineKey = new HotkeyNode(Keys.W);
            DisciplineConnectedSkill = new RangeNode<int>(1, 1, 13);

            Grace = new ToggleNode(false);
            GraceKey = new HotkeyNode(Keys.W);
            GraceConnectedSkill = new RangeNode<int>(1, 1, 13);

            Determination = new ToggleNode(false);
            DeterminationKey = new HotkeyNode(Keys.W);
            DeterminationConnectedSkill = new RangeNode<int>(1, 1, 13);

            DefianceBanner = new ToggleNode(false);
            DefianceBannerKey = new HotkeyNode(Keys.W);
            DefianceBannerConnectedSkill = new RangeNode<int>(1, 1, 13);

            PurityOfIce = new ToggleNode(false);
            PurityOfIceKey = new HotkeyNode(Keys.W);
            PurityOfIceConnectedSkill = new RangeNode<int>(1, 1, 13);

            PurityOfFire = new ToggleNode(false);
            PurityOfFireKey = new HotkeyNode(Keys.W);
            PurityOfFireConnectedSkill = new RangeNode<int>(1, 1, 13);

            PurityOfLightning = new ToggleNode(false);
            PurityOfLightningKey = new HotkeyNode(Keys.W);
            PurityOfLightningConnectedSkill = new RangeNode<int>(1, 1, 13);

            Vitality = new ToggleNode(false);
            VitalityKey = new HotkeyNode(Keys.W);
            VitalityConnectedSkill = new RangeNode<int>(1, 1, 13);

            Precision = new ToggleNode(false);
            PrecisionKey = new HotkeyNode(Keys.W);
            PrecisionConnectedSkill = new RangeNode<int>(1, 1, 13);

            Clarity = new ToggleNode(false);
            ClarityKey = new HotkeyNode(Keys.W);
            ClarityConnectedSkill = new RangeNode<int>(1, 1, 13);

            VaalDiscipline = new ToggleNode(false);
            VaalDisciplineKey = new HotkeyNode(Keys.W);
            VaalDisciplineConnectedSkill = new RangeNode<int>(1, 1, 13);

            VaalGrace = new ToggleNode(false);
            VaalGraceKey = new HotkeyNode(Keys.W);
            VaalGraceConnectedSkill = new RangeNode<int>(1, 1, 13);

            xyz = new ToggleNode(false);
            xyzKey = new HotkeyNode(Keys.R);
            xyzConnectedSkill = new RangeNode<int>(1, 1, 13);

            xyz2 = new ToggleNode(false);
            xyz2Key = new HotkeyNode(Keys.R);
            xyz2ConnectedSkill = new RangeNode<int>(1, 1, 13);

            VaalMoltenShell = new ToggleNode(false);
            VaalMoltenShellKey = new HotkeyNode(Keys.W);
            VaalMoltenShellConnectedSkill = new RangeNode<int>(1, 1, 13);
           // not used atm VaalMoltenShellMaxHP = new RangeNode<int>(50, 0, 100);
            VaalMoltenShellMaxES = new RangeNode<int>(50, 0, 100);

            // my end

            RequireMinMonsterCount = new ToggleNode(false);
            NearbyMonsterCount = new RangeNode<int>(1, 1, 30);
            NearbyMonsterMaxDistance = new RangeNode<int>(500, 1, 2000);
            DisableInHideout = new ToggleNode(true);
            Debug = new ToggleNode(false);
            SilenceErrors = new ToggleNode(false);
        }

        // my stuff
        #region Wrath
        [Menu("wrath", 11)] public ToggleNode Wrath { get; set; }

        [Menu("wrath  Key", 111, 11)]
        public HotkeyNode WrathKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 112, 11)]
        public RangeNode<int> WrathConnectedSkill { get; set; }
        #endregion

        #region Zealotry
        [Menu("Zealotry", 12)] public ToggleNode Zealotry { get; set; }

        [Menu("Zealotry  Key", 121, 12)] public HotkeyNode ZealotryKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 122, 12)]
        public RangeNode<int> ZealotryConnectedSkill { get; set; }
        #endregion

        #region Hatred
        [Menu("Hatred", 13)] public ToggleNode Hatred { get; set; }

        [Menu("Hatred  Key", 131, 13)] public HotkeyNode HatredKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 132, 13)]
        public RangeNode<int> HatredConnectedSkill { get; set; }
        #endregion

        #region Haste
        [Menu("Haste", 14)] public ToggleNode Haste { get; set; }

        [Menu("Haste  Key", 141, 14)] public HotkeyNode HasteKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 142, 14)]
        public RangeNode<int> HasteConnectedSkill { get; set; }
        #endregion

        #region Discipline
        [Menu("Discipline", 15)] public ToggleNode Discipline { get; set; }

        [Menu("Discipline  Key", 151, 15)] public HotkeyNode DisciplineKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 152, 15)]
        public RangeNode<int> DisciplineConnectedSkill { get; set; }
        #endregion

        #region Grace
        [Menu("Grace", 16)] public ToggleNode Grace { get; set; }

        [Menu("Grace  Key", 161, 16)] public HotkeyNode GraceKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 162, 16)]
        public RangeNode<int> GraceConnectedSkill { get; set; }
        #endregion

        #region Determination
        [Menu("Determination", 17)] public ToggleNode Determination { get; set; }

        [Menu("Determination  Key", 171, 17)] public HotkeyNode DeterminationKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 172, 17)]
        public RangeNode<int> DeterminationConnectedSkill { get; set; }
        #endregion

        #region DefianceBanner
        [Menu("DefianceBanner", 18)] public ToggleNode DefianceBanner { get; set; }

        [Menu("DefianceBanner  Key", 181, 18)] public HotkeyNode DefianceBannerKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 182, 18)]
        public RangeNode<int> DefianceBannerConnectedSkill { get; set; }
        #endregion

        #region PurityOfIce
        [Menu("PurityOfIce", 19)] public ToggleNode PurityOfIce { get; set; }

        [Menu("PurityOfIce  Key", 191, 19)] public HotkeyNode PurityOfIceKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 192, 19)]
        public RangeNode<int> PurityOfIceConnectedSkill { get; set; }
        #endregion

        #region PurityOfLightning
        [Menu("PurityOfLightning", 20)] public ToggleNode PurityOfLightning { get; set; }

        [Menu("PurityOfLightning  Key", 201, 20)] public HotkeyNode PurityOfLightningKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 202, 20)]
        public RangeNode<int> PurityOfLightningConnectedSkill { get; set; }
        #endregion

        #region PurityOfFire
        [Menu("PurityOfFire", 21)] public ToggleNode PurityOfFire { get; set; }

        [Menu("PurityOfFire  Key", 211, 21)] public HotkeyNode PurityOfFireKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 212, 21)]
        public RangeNode<int> PurityOfFireConnectedSkill { get; set; }
        #endregion

        #region Vitality
        [Menu("Vitality", 22)] public ToggleNode Vitality { get; set; }

        [Menu("Vitality  Key", 221, 22)] public HotkeyNode VitalityKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 222, 22)]
        public RangeNode<int> VitalityConnectedSkill { get; set; }
        #endregion

        #region Precision
        [Menu("Precision", 23)] public ToggleNode Precision { get; set; }

        [Menu("Precision  Key", 231, 23)] public HotkeyNode PrecisionKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 232, 23)]
        public RangeNode<int> PrecisionConnectedSkill { get; set; }
        #endregion

        #region Clarity
        [Menu("Clarity", 24)] public ToggleNode Clarity { get; set; }

        [Menu("Clarity  Key", 241, 24)] public HotkeyNode ClarityKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 242, 24)]
        public RangeNode<int> ClarityConnectedSkill { get; set; }
        #endregion

        #region VaalDiscipline
        [Menu("VaalDiscipline", 25)] public ToggleNode VaalDiscipline { get; set; }

        [Menu("VaalDiscipline  Key", 251, 25)] public HotkeyNode VaalDisciplineKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 252, 25)]
        public RangeNode<int> VaalDisciplineConnectedSkill { get; set; }
        #endregion

        #region VaalGrace
        [Menu("VaalGrace", 28)] public ToggleNode VaalGrace { get; set; }

        [Menu("VaalGrace  Key", 281, 28)] public HotkeyNode VaalGraceKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 282, 28)]
        public RangeNode<int> VaalGraceConnectedSkill { get; set; }

        #endregion

        #region VaalMoltenShell
        [Menu("VaalMoltenShell", 27)] public ToggleNode VaalMoltenShell { get; set; }

        [Menu("VaalMoltenShell  Key", 271, 27)] public HotkeyNode VaalMoltenShellKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 272, 27)]
        public RangeNode<int> VaalMoltenShellConnectedSkill { get; set; }

        [Menu("Max ES", "Es percent above which skill is not cast currently logic is for chaos inoculation", 273, 27)]
        public RangeNode<int> VaalMoltenShellMaxES { get; set; }


        #endregion

        //naming it intuitve link didnt work, needs fixing 
        #region xyz
        [Menu("IntuitiveLink", 26)] public ToggleNode xyz { get; set; }

        [Menu("IntuitiveLink  Key", 261, 26)] public HotkeyNode xyzKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 262, 26)]
        public RangeNode<int> xyzConnectedSkill { get; set; }
        #endregion

        //end my stuff

        #region SoulLink
        [Menu("SoulLink", 29)] public ToggleNode xyz2 { get; set; }

        [Menu("SoulLink  Key", 291, 29)] public HotkeyNode xyz2Key { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 292, 29)]
        public RangeNode<int> xyz2ConnectedSkill { get; set; }

        [Menu("Min charges", "Minimal amount of BF charges", 292, 29)]
        public RangeNode<int> xyz2Charges { get; set; }
        #endregion

        #region Blade Vortex

        [Menu("Blade Vortex", "Use mouse click to release Blade Vortex charges", 40)] public ToggleNode xyz3 { get; set; }

        [Menu("Min charges", "Minimal amount of BF charges", 401, 40)]
        public RangeNode<int> xyz3Charges { get; set; }

        [Menu("BladeVortex  Key", 402, 40)] public HotkeyNode xyz3Key { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 403, 40)]
        public RangeNode<int> xyz3ConnectedSkill { get; set; }

        #endregion


        #region Blood Rage

        public ToggleNode Enable { get; set; }

        [Menu("Blood Rage", 1)] public ToggleNode BloodRage { get; set; }

        [Menu("Blood Rage Key", "Which key to press to activate Blood Rage?", 11, 1)]
        public HotkeyNode BloodRageKey { get; set; }


        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 12, 1)]
        public RangeNode<int> BloodRageConnectedSkill { get; set; }

        [Menu("Max HP", "HP percent above which skill is not cast", 13, 1)]
        public RangeNode<int> BloodRageMaxHP { get; set; }

        [Menu("Max Mana", "Mana percent above which skill is not cast", 14, 1)]
        public RangeNode<int> BloodRageMaxMP { get; set; }

        #endregion

        #region Steel Skin

        [Menu("Steel Skin", 2)] public ToggleNode SteelSkin { get; set; }

        [Menu("Steel Skin Key", 21, 2)] public HotkeyNode SteelSkinKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 22, 2)]
        public RangeNode<int> SteelSkinConnectedSkill { get; set; }

        [Menu("Max HP", "HP percent above which skill is not cast", 23, 2)]
        public RangeNode<int> SteelSkinMaxHP { get; set; }

        #endregion

        #region Immortal Call

        [Menu("Immortal Call", 3)] public ToggleNode ImmortalCall { get; set; }

        [Menu("Immortal Call Key", 31, 3)] public HotkeyNode ImmortalCallKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 32, 3)]
        public RangeNode<int> ImmortalCallConnectedSkill { get; set; }

        [Menu("Max HP", "HP percent above which skill is not cast", 33, 3)]
        public RangeNode<int> ImmortalCallMaxHP { get; set; }

        #endregion

        #region Molten Shell

        [Menu("Molten Shell", 4)] public ToggleNode MoltenShell { get; set; }

        [Menu("Molten Shell Key", 41, 4)] public HotkeyNode MoltenShellKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 42, 4)]
        public RangeNode<int> MoltenShellConnectedSkill { get; set; }

        [Menu("Max HP", "HP percent above which skill is not cast", 43, 4)]
        public RangeNode<int> MoltenShellMaxHP { get; set; }

        #endregion

        #region Phase Run

        [Menu("Phase Run", 5)] public ToggleNode PhaseRun { get; set; }

        [Menu("Phase Run Key", 51, 5)] public HotkeyNode PhaseRunKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 52, 5)]
        public RangeNode<int> PhaseRunConnectedSkill { get; set; }

        [Menu("Max HP", "HP percent above which skill is not cast", 53, 5)]
        public RangeNode<int> PhaseRunMaxHP { get; set; }
            
        [Menu("Move time", "Time in ms spent moving after which skill can be cast", 54, 5)]
        public RangeNode<int> PhaseRunMinMoveTime { get; set; }
            
        [Menu("BV Stacks", "Blade Vortex stacks required to cast Phase Run", 55, 5)]
        public RangeNode<int> PhaseRunMinBVStacks { get; set; }

        #endregion

        #region Withering Step

        [Menu("Withering Step", 6)] public ToggleNode WitheringStep { get; set; }

        [Menu("Withering Step Key", 61, 6)] public HotkeyNode WitheringStepKey { get; set; }

        [Menu("Connected Skill", "Set the skill slot (1 = top left, 8 = bottom right)", 62, 6)]
        public RangeNode<int> WitheringStepConnectedSkill { get; set; }

        [Menu("Max HP", "HP percent above which skill is not cast", 63, 6)]
        public RangeNode<int> WitheringStepMaxHP { get; set; }
            
        [Menu("Move time", "Time in ms spent moving after which skill can be cast", 64, 6)]
        public RangeNode<int> WitheringStepMinMoveTime { get; set; }

        #endregion

        #region Blade Flurry

        [Menu("Blade Flurry", "Use mouse click to release Blade Flurry charges", 7)] public ToggleNode BladeFlurry { get; set; }

        [Menu("Min charges", "Minimal amount of BF charges to release", 71, 7)]
        public RangeNode<int> BladeFlurryMinCharges { get; set; }

        [Menu("Use left click", "Use left click instead of right click to release charges", 72, 7)] 
        public ToggleNode BladeFlurryUseLeftClick { get; set; }
        
        [Menu("Wait for Infused Channeling buff", "Wait for Infused Channeling buff before release", 73, 7)] 
        public ToggleNode BladeFlurryWaitForInfused { get; set; }

        #endregion

        #region Scourge Arrow

        [Menu("Scourge Arrow", "Use mouse click to release Scourge Arrow charges", 8)] public ToggleNode ScourgeArrow { get; set; }

        [Menu("Min charges", "Minimal amount of BF charges to release", 81, 8)]
        public RangeNode<int> ScourgeArrowMinCharges { get; set; }

        [Menu("Use left click", "Use left click instead of right click to release charges", 82, 8)] 
        public ToggleNode ScourgeArrowUseLeftClick { get; set; }
        
        [Menu("Wait for Infused Channeling buff", "Wait for Infused Channeling buff before release", 83, 8)] 
        public ToggleNode ScourgeArrowWaitForInfused { get; set; }
        #endregion

        #region Misc

        [Menu("Misc", 10)] public EmptyNode MiscSettings { get; set; }

        [Menu("Nearby monsters", "Require a minimum count of nearby monsters to cast buffs?", 101, 10)]
        public ToggleNode RequireMinMonsterCount { get; set; }

        [Menu("Range", "Minimum count of nearby monsters to cast", 102, 10)]
        public RangeNode<int> NearbyMonsterCount { get; set; }

        [Menu("Range", "Max distance of monsters to player to count as nearby", 103, 10)]
        public RangeNode<int> NearbyMonsterMaxDistance { get; set; }

        [Menu("Disable in hideout", "Disable the plugin in hideout?", 104, 10)]
        public ToggleNode DisableInHideout { get; set; }
        
        [Menu("Debug", "Print debug messages?", 105, 10)]
        public ToggleNode Debug { get; set; }
        
        [Menu("Silence errors", "Hide error messages?", 106, 10)]
        public ToggleNode SilenceErrors { get; set; }

        #endregion
    }
}