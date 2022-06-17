using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WindowsInput;
using WindowsInput.Native;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using SharpDX;
using System.Threading;

namespace BuffUtil
{
    public class BuffUtil : BaseSettingsPlugin<BuffUtilSettings>
    {
        private readonly HashSet<Entity> loadedMonsters = new HashSet<Entity>();
        private readonly object loadedMonstersLock = new object();

        private List<Buff> buffs;
        private List<ActorSkill> skills;
        
        private DateTime? currentTime { get => DateTime.Now; }
        private InputSimulator inputSimulator;
        private Random rand;
        private DateTime? lastBloodRageCast;
        private DateTime? lastPhaseRunCast;
        private DateTime? lastWitheringStepCast;
        private DateTime? lastSteelSkinCast;
        private DateTime? lastImmortalCallCast;
        private DateTime? lastMoltenShellCast;
        //my
        private DateTime? lastWrathCast;
        private DateTime? lastZealotryCast;
        private DateTime? lastHatredCast;

        private DateTime? lastHasteCast;
        private DateTime? lastDisciplineCast;
        private DateTime? lastGraceCast;
        private DateTime? lastDeterminationCast;
        private DateTime? lastDefianceBannerCast;

        private DateTime? lastPurityOfIceCast;
        private DateTime? lastPurityOfLightningCast;
        private DateTime? lastPurityOfFireCast;
        private DateTime? lastVitalityCast;
        private DateTime? lastPrecisionCast;
        private DateTime? lastClarityCast;
        private DateTime? lastIntuitiveLinkCast;
        private DateTime? lastSoulLinkCast;

        private DateTime? lastVaalDisciplineCast;
        private DateTime? lastVaalGraceCast;
        private DateTime? lastVaalMoltenShellCast;

        // from copilot
        private List<ActorVaalSkill> vaalSkills = new List<ActorVaalSkill>();

        //end my
        private float HPPercent;
        private float MPPercent;
        private float ESPercent; // add by me
        private int? nearbyMonsterCount;
        private bool showErrors = true;
        private Stopwatch movementStopwatch { get; set; } = new Stopwatch();

        public override bool Initialise()
        {
            inputSimulator = new InputSimulator();
            rand = new Random();

            showErrors = !Settings.SilenceErrors;
            Settings.SilenceErrors.OnValueChanged += delegate { showErrors = !Settings.SilenceErrors; };
            return base.Initialise();
        }

        public override void OnPluginDestroyForHotReload()
        {
            if (loadedMonsters != null)
                lock (loadedMonstersLock)
                {
                    loadedMonsters.Clear();
                }

            base.OnPluginDestroyForHotReload();
        }

        public override void Render()
        {
            // Should move to Tick?
            if (OnPreExecute())
                OnExecute();
            OnPostExecute();
        }

        private void OnExecute()
        {
            try
            {
                HandleBladeFlurry();
                HandleScourgeArrow();
                HandleBloodRage();
                HandleSteelSkin();
                HandleImmortalCall();
                HandleMoltenShell();
                HandlePhaseRun();
                HandleWitheringStep();
                HandleBladeVortex();

                HandleWrath();
                HandleZealotry();
                HandleHatred();

                HandleHaste();
                HandleDiscipline();
                HandleGrace();
                HandleDetermination();
                HandleDefianceBanner();
                HandlePurityOfIce();
                HandlePurityOfLightning();
                HandlePurityOfFire();
                HandleVitality();
                HandlePrecision();
                HandleClarity();
                HandleIntuitiveLink();
                HandleSoulLink();

                HandleVaalDiscipline();
                HandleVaalGrace();
                HandleVaalMoltenShell();
            }
            catch (Exception ex)
            {
                if (showErrors)
                {
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(OnExecute)}: {ex.StackTrace}", 3f);
                }
            }
        }


        // my stuff
        // handle warath zeal and hatred also check each other for march of the legion boots
        private void HandleWrath()
        {
            try
            {
                if (!Settings.Wrath)
                    return;

                if (lastWrathCast.HasValue && currentTime - lastWrathCast.Value <
                    C.Wrath.TimeBetweenCasts)
                    return;

                if (lastZealotryCast.HasValue && currentTime - lastZealotryCast.Value <
                    C.Zealotry.TimeBetweenCasts)
                    return;

                if (lastHatredCast.HasValue && currentTime - lastHatredCast.Value <
                    C.Hatred.TimeBetweenCasts)
                    return;



                var hasBuff = HasBuff(C.Wrath.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Wrath.Name, C.Wrath.InternalName,
                    Settings.WrathConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Wrath - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Wrath for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.WrathKey.Value);
                lastWrathCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleWrath)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleZealotry()
        {
            try
            {
                if (!Settings.Zealotry)
                    return;

                if (lastWrathCast.HasValue && currentTime - lastWrathCast.Value <
                    C.Wrath.TimeBetweenCasts)
                    return;

                if (lastZealotryCast.HasValue && currentTime - lastZealotryCast.Value <
                    C.Zealotry.TimeBetweenCasts)
                    return;

                if (lastHatredCast.HasValue && currentTime - lastHatredCast.Value <
                    C.Hatred.TimeBetweenCasts)
                    return;



                var hasBuff = HasBuff(C.Zealotry.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Zealotry.Name, C.Zealotry.InternalName,
                    Settings.ZealotryConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Zealotry - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Zealotry for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.ZealotryKey.Value);
                lastZealotryCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleZealotry)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleHatred()
        {
            try
            {
                if (!Settings.Hatred)
                    return;

                if (lastWrathCast.HasValue && currentTime - lastWrathCast.Value <
                    C.Wrath.TimeBetweenCasts)
                    return;

                if (lastZealotryCast.HasValue && currentTime - lastZealotryCast.Value <
                    C.Zealotry.TimeBetweenCasts)
                    return;

                if (lastHatredCast.HasValue && currentTime - lastHatredCast.Value <
                    C.Hatred.TimeBetweenCasts)
                    return;



                var hasBuff = HasBuff(C.Hatred.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Hatred.Name, C.Hatred.InternalName,
                    Settings.HatredConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Hatred - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Hatred for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.HatredKey.Value);
                lastHatredCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleHatred)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleHaste()
        {
            try
            {
                if (!Settings.Haste)
                    return;

                if (lastHasteCast.HasValue && currentTime - lastHasteCast.Value <
                    C.Haste.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Haste.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Haste.Name, C.Haste.InternalName,
                    Settings.HasteConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Haste - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Haste for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.HasteKey.Value);
                lastHasteCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleHaste)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleDiscipline()
        {
            try
            {
                if (!Settings.Discipline)
                    return;

                if (lastDisciplineCast.HasValue && currentTime - lastDisciplineCast.Value <
                    C.Discipline.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Discipline.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Discipline.Name, C.Discipline.InternalName,
                    Settings.DisciplineConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Discipline - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Discipline for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.DisciplineKey.Value);
                lastDisciplineCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleDiscipline)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleGrace()
        {
            try
            {
                if (!Settings.Grace)
                    return;

                if (lastGraceCast.HasValue && currentTime - lastGraceCast.Value <
                    C.Grace.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Grace.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Grace.Name, C.Grace.InternalName,
                    Settings.GraceConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Grace - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Grace for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.GraceKey.Value);
                lastGraceCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleGrace)}: {ex.StackTrace}", 3f);
            }
        }
        private void HandleDetermination()
        {
            try
            {
                if (!Settings.Determination)
                    return;

                if (lastDeterminationCast.HasValue && currentTime - lastDeterminationCast.Value <
                    C.Determination.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Determination.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Determination.Name, C.Determination.InternalName,
                    Settings.DeterminationConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Determination - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Determination for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.DeterminationKey.Value);
                lastDeterminationCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleDetermination)}: {ex.StackTrace}", 3f);
            }
        }


        private void HandleDefianceBanner()
        {
            try
            {
                if (!Settings.DefianceBanner)
                    return;

                if (lastDefianceBannerCast.HasValue && currentTime - lastDefianceBannerCast.Value <
                    C.DefianceBanner.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.DefianceBanner.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.DefianceBanner.Name, C.DefianceBanner.InternalName,
                    Settings.DefianceBannerConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast DefianceBanner - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("DefianceBanner for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.DefianceBannerKey.Value);
                lastDefianceBannerCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleDefianceBanner)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandlePurityOfIce()
        {
            try
            {
                if (!Settings.PurityOfIce)
                    return;

                if (lastPurityOfIceCast.HasValue && currentTime - lastPurityOfIceCast.Value <
                    C.PurityOfIce.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.PurityOfIce.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.PurityOfIce.Name, C.PurityOfIce.InternalName,
                    Settings.PurityOfIceConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast PurityOfIce - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("PurityOfIce for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.PurityOfIceKey.Value);
                lastPurityOfIceCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandlePurityOfIce)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandlePurityOfLightning()
        {
            try
            {
                if (!Settings.PurityOfLightning)
                    return;

                if (lastPurityOfLightningCast.HasValue && currentTime - lastPurityOfLightningCast.Value <
                    C.PurityOfLightning.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.PurityOfLightning.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.PurityOfLightning.Name, C.PurityOfLightning.InternalName,
                    Settings.PurityOfLightningConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast PurityOfLightning - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("PurityOfLightning for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.PurityOfLightningKey.Value);
                lastPurityOfLightningCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandlePurityOfLightning)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandlePurityOfFire()
        {
            try
            {
                if (!Settings.PurityOfFire)
                    return;

                if (lastPurityOfFireCast.HasValue && currentTime - lastPurityOfFireCast.Value <
                    C.PurityOfFire.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.PurityOfFire.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.PurityOfFire.Name, C.PurityOfFire.InternalName,
                    Settings.PurityOfFireConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast PurityOfFire - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("PurityOfFire for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.PurityOfFireKey.Value);
                lastPurityOfFireCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandlePurityOfFire)}: {ex.StackTrace}", 3f);
            }
        }


        private void HandleVitality()
        {
            try
            {
                if (!Settings.Vitality)
                    return;

                if (lastVitalityCast.HasValue && currentTime - lastVitalityCast.Value <
                    C.Vitality.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Vitality.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Vitality.Name, C.Vitality.InternalName,
                    Settings.VitalityConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Vitality - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Vitality for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.VitalityKey.Value);
                lastVitalityCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleVitality)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandlePrecision()
        {
            try
            {
                if (!Settings.Precision)
                    return;

                if (lastPrecisionCast.HasValue && currentTime - lastPrecisionCast.Value <
                    C.Precision.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Precision.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Precision.Name, C.Precision.InternalName,
                    Settings.PrecisionConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Precision - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Precision for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.PrecisionKey.Value);
                lastPrecisionCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandlePrecision)}: {ex.StackTrace}", 3f);
            }
        }


        private void HandleClarity()
        {
            try
            {
                if (!Settings.Clarity)
                    return;

                if (lastClarityCast.HasValue && currentTime - lastClarityCast.Value <
                    C.Clarity.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.Clarity.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.Clarity.Name, C.Clarity.InternalName,
                    Settings.ClarityConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Clarity - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Clarity for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.ClarityKey.Value);
                lastClarityCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleClarity)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleVaalDiscipline()
        {
            try
            {
                if (!Settings.VaalDiscipline)
                    return;

                if (lastVaalDisciplineCast.HasValue && currentTime - lastVaalDisciplineCast.Value <
                    C.VaalDiscipline.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.VaalDiscipline.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.VaalDiscipline.Name, C.VaalDiscipline.InternalName,
                    Settings.VaalDisciplineConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast VaalDiscipline - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("VaalDiscipline for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.VaalDisciplineKey.Value);
                lastVaalDisciplineCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleVaalDiscipline)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleVaalGrace()
        {
            try
            {
                if (!Settings.VaalGrace)
                    return;

                if (lastVaalGraceCast.HasValue && currentTime - lastVaalGraceCast.Value <
                    C.VaalGrace.TimeBetweenCasts)
                    return;

                var hasBuff = HasBuff(C.VaalGrace.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.VaalGrace.Name, C.VaalGrace.InternalName,
                    Settings.VaalGraceConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast VaalGrace - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("VaalGrace for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.VaalGraceKey.Value);
                lastVaalGraceCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleVaalGrace)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleVaalMoltenShell()
        {
            try
            {
                if (!Settings.VaalMoltenShell)
                    return;
                // todo vaal souls check vaalSkills = localPlayer.GetComponent<Actor>().ActorVaalSkills;
                //var vaalSouls = ExileCore.PoEMemory.MemoryObjects.ActorVaalSkill.CurrVaalSouls
                //vaalSkills.Exists(x => x.CurrVaalSouls >= x.VaalSoulsPerUse)


               // if (SkillInfo.ManageCooldown(SkillInfo.vaalSkill, skill))

                if (lastVaalMoltenShellCast.HasValue && currentTime - lastVaalMoltenShellCast.Value <
                    C.VaalMoltenShell.TimeBetweenCasts)
                    return;

                if (ESPercent > Settings.VaalMoltenShellMaxES.Value)
                    return;

                var hasBuff = HasBuff(C.VaalMoltenShell.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.VaalMoltenShell.Name, C.VaalMoltenShell.InternalName,
                    Settings.VaalMoltenShellConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast VaalMoltenShell - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("VaalMoltenShell for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.VaalMoltenShellKey.Value);
                lastVaalMoltenShellCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleVaalMoltenShell)}: {ex.StackTrace}", 3f);
            }
        }


        private void HandleIntuitiveLink()
        {
            try
            {
                if (!Settings.xyz)
                    return;

                if (lastIntuitiveLinkCast.HasValue && currentTime - lastIntuitiveLinkCast.Value <
                    C.xyz.TimeBetweenCasts)
                    return;

                
                var hasBuff = HasBuff(C.xyz.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.xyz.Name, C.xyz.InternalName,
                    Settings.xyzConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast IntuitiveLink - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("IntuitiveLink for real", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.xyzKey.Value);
                lastIntuitiveLinkCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleIntuitiveLink)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleSoulLink()
        {
            try
            {
                Graphics.DrawText($"Nearby players count: ", new Vector2(100, 100),Color.Red);

                if (!Settings.SoulLink)
                    return;

                if (lastSoulLinkCast.HasValue && currentTime - lastSoulLinkCast.Value <
                    C.xyz2.TimeBetweenCasts)
                    return;

                var playersCount = GameController.IngameState.Data.ServerData.NearestPlayers.Count;

                var hasBuff = HasBuffs(C.xyz2.BuffName, playersCount);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.xyz2.Name, C.xyz2.InternalName,
                    Settings.SoulLinkConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast IntuitiveLink - not found in usable skills.", 1);
                    return;
                }

                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.SoulLinkKey.Value);
                lastSoulLinkCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
                Thread.Sleep(100 + rand.Next(0, 100));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleSoulLink)}: {ex.StackTrace}", 3f);
            }
        }

        //my stuff end


        private void HandleBladeFlurry()
        {
            try
            {
                if (!Settings.BladeFlurry)
                    return;

                var stacksBuff = GetBuff(C.BladeFlurry.BuffName);
                if (stacksBuff == null)
                    return;

                var charges = stacksBuff.Charges;
                if (charges < Settings.BladeFlurryMinCharges.Value)
                    return;

                if (Settings.BladeFlurryWaitForInfused)
                {
                    var hasInfusedBuff = HasBuff(C.InfusedChanneling.BuffName);
                    if (!hasInfusedBuff.HasValue || !hasInfusedBuff.Value)
                        return;
                }

                if (Settings.Debug)
                    LogMessage($"Releasing Blade Flurry at {charges} charges.", 1);

                if (Settings.BladeFlurryUseLeftClick)
                {
                    inputSimulator.Mouse.LeftButtonUp();
                    inputSimulator.Mouse.LeftButtonDown();
                }
                else
                {
                    inputSimulator.Mouse.RightButtonUp();
                    inputSimulator.Mouse.RightButtonDown();
                }
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleBloodRage)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleBladeVortex()
        {
            try
            {
                if (!Settings.BladeVortex)
                    return;

                if (lastBloodRageCast.HasValue && currentTime - lastBloodRageCast.Value <
                    C.BladeVortex.TimeBetweenCasts)
                    return;

                var stacksBuff = GetBuff(C.BladeVortex.BuffName);
                if (stacksBuff != null)
                {
                    var charges = stacksBuff.Charges;
                    if (charges >= Settings.BladeVortexMinCharges.Value)
                        return;
                }

                inputSimulator.Keyboard.KeyPress((VirtualKeyCode)Settings.BladeVortexKey.Value);
                lastBloodRageCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleBloodRage)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleScourgeArrow()
        {
            try
            {
                if (!Settings.ScourgeArrow)
                    return;

                var stacksBuff = GetBuff(C.ScourgeArrow.BuffName);
                if (stacksBuff == null)
                    return;

                var charges = stacksBuff.Charges;
                if (charges < Settings.ScourgeArrowMinCharges.Value)
                    return;

                if (Settings.ScourgeArrowWaitForInfused)
                {
                    var hasInfusedBuff = HasBuff(C.InfusedChanneling.BuffName);
                    if (!hasInfusedBuff.HasValue || !hasInfusedBuff.Value)
                        return;
                }

                if (Settings.Debug)
                    LogMessage($"Releasing Scourge Arrow at {charges} charges.", 1);

                if (Settings.ScourgeArrowUseLeftClick)
                {
                    inputSimulator.Mouse.LeftButtonUp();
                    inputSimulator.Mouse.LeftButtonDown();
                }
                else
                {
                    inputSimulator.Mouse.RightButtonUp();
                    inputSimulator.Mouse.RightButtonDown();
                }
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleScourgeArrow)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleBloodRage()
        {
            try
            {
                if (!Settings.BloodRage)
                    return;

                if (lastBloodRageCast.HasValue && currentTime - lastBloodRageCast.Value <
                    C.BloodRage.TimeBetweenCasts)
                    return;

                if (HPPercent > Settings.BloodRageMaxHP.Value || MPPercent > Settings.BloodRageMaxMP)
                    return;

                var hasBuff = HasBuff(C.BloodRage.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.BloodRage.Name, C.BloodRage.InternalName,
                    Settings.BloodRageConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Blood Rage - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Casting Blood Rage", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode) Settings.BloodRageKey.Value);
                lastBloodRageCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleBloodRage)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleSteelSkin()
        {
            try
            {
                if (!Settings.SteelSkin)
                    return;

                if (lastSteelSkinCast.HasValue && currentTime - lastSteelSkinCast.Value <
                    C.SteelSkin.TimeBetweenCasts)
                    return;

                if (HPPercent > Settings.SteelSkinMaxHP.Value)
                    return;

                var hasBuff = HasBuff(C.SteelSkin.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.SteelSkin.Name, C.SteelSkin.InternalName,
                    Settings.SteelSkinConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Steel Skin - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Casting Steel Skin", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode) Settings.SteelSkinKey.Value);
                lastSteelSkinCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleSteelSkin)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleImmortalCall()
        {
            try
            {
                if (!Settings.ImmortalCall)
                    return;

                if (lastImmortalCallCast.HasValue && currentTime - lastImmortalCallCast.Value <
                    C.ImmortalCall.TimeBetweenCasts)
                    return;

                if (HPPercent > Settings.ImmortalCallMaxHP.Value)
                    return;

                var hasBuff = HasBuff(C.ImmortalCall.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.ImmortalCall.Name, C.ImmortalCall.InternalName,
                    Settings.ImmortalCallConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Immortal Call - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Casting Immortal Call", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode) Settings.ImmortalCallKey.Value);
                lastImmortalCallCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleImmortalCall)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleMoltenShell()
        {
            try
            {
                if (!Settings.MoltenShell)
                    return;

                if (lastMoltenShellCast.HasValue && currentTime - lastMoltenShellCast.Value <
                    C.MoltenShell.TimeBetweenCasts)
                    return;

                if (HPPercent > Settings.MoltenShellMaxHP.Value)
                    return;

                var hasBuff = HasBuff(C.MoltenShell.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.MoltenShell.Name, C.MoltenShell.InternalName,
                    Settings.MoltenShellConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Molten Shell - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Casting Molten Shell", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode) Settings.MoltenShellKey.Value);
                lastMoltenShellCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleMoltenShell)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandlePhaseRun()
        {
            try
            {
                if (!Settings.PhaseRun)
                    return;

                if (lastPhaseRunCast.HasValue && currentTime - lastPhaseRunCast.Value <
                    C.PhaseRun.TimeBetweenCasts)
                    return;

                if (HPPercent > Settings.PhaseRunMaxHP.Value)
                    return;

                if (movementStopwatch.ElapsedMilliseconds < Settings.PhaseRunMinMoveTime)
                    return;

                var hasBuff = HasBuff(C.PhaseRun.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var requiredBVStacks = Settings.PhaseRunMinBVStacks.Value;
                if (requiredBVStacks > 0)
                {
                    var bvBuff = GetBuff(C.BladeVortex.BuffName);
                    if (bvBuff == null || bvBuff.Charges < requiredBVStacks)
                        return;
                }

                var skill = GetUsableSkill(C.PhaseRun.Name, C.PhaseRun.InternalName,
                    Settings.PhaseRunConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Phase Run - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Casting Phase Run", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode) Settings.PhaseRunKey.Value);
                lastPhaseRunCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.2));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleSteelSkin)}: {ex.StackTrace}", 3f);
            }
        }

        private void HandleWitheringStep()
        {
            try
            {
                if (!Settings.WitheringStep)
                    return;

                if (lastWitheringStepCast.HasValue && currentTime - lastWitheringStepCast.Value <
                    C.WitheringStep.TimeBetweenCasts)
                    return;

                if (HPPercent > Settings.WitheringStepMaxHP.Value)
                    return;

                if (movementStopwatch.ElapsedMilliseconds < Settings.WitheringStepMinMoveTime)
                    return;

                var hasBuff = HasBuff(C.WitheringStep.BuffName);
                if (!hasBuff.HasValue || hasBuff.Value)
                    return;

                var skill = GetUsableSkill(C.WitheringStep.Name, C.WitheringStep.InternalName,
                    Settings.WitheringStepConnectedSkill.Value);
                if (skill == null)
                {
                    if (Settings.Debug)
                        LogMessage("Can not cast Withering Step - not found in usable skills.", 1);
                    return;
                }

                if (!NearbyMonsterCheck())
                    return;

                if (Settings.Debug)
                    LogMessage("Casting Withering Step", 1);
                inputSimulator.Keyboard.KeyPress((VirtualKeyCode) Settings.WitheringStepKey.Value);
                lastWitheringStepCast = currentTime + TimeSpan.FromSeconds(rand.NextDouble(0, 0.5));
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(HandleSteelSkin)}: {ex.StackTrace}", 3f);
            }
        }





        private bool OnPreExecute()
        {
            try
            {
                if (!Settings.Enable)
                    return false;
                var inTown = GameController.Area.CurrentArea.IsTown;
                if (inTown)
                    return false;
                if (Settings.DisableInHideout && GameController.Area.CurrentArea.IsHideout)
                    return false;
                var player = GameController.Game.IngameState.Data.LocalPlayer;
                if (player == null)
                    return false;
                var playerLife = player.GetComponent<Life>();
                if (playerLife == null)
                    return false;
                var isDead = playerLife.CurHP <= 0;
                if (isDead)
                    return false;

                buffs = player.GetComponent<Buffs>()?.BuffsList;
                if (buffs == null)
                    return false;

                var gracePeriod = HasBuff(C.GracePeriod.BuffName);
                if (!gracePeriod.HasValue || gracePeriod.Value)
                    return false;

                skills = player.GetComponent<Actor>().ActorSkills;
                if (skills == null || skills.Count == 0)
                    return false;

                vaalSkills = player.GetComponent<Actor>().ActorVaalSkills;
                if (vaalSkills == null || skills.Count == 0)
                    return false;

                HPPercent = 100f * playerLife.HPPercentage;
                MPPercent = 100f * playerLife.MPPercentage;
                ESPercent = 100f * playerLife.ESPercentage;

                var playerActor = player.GetComponent<Actor>();
                if (player != null && player.Address != 0 && playerActor.isMoving)
                {
                    if (!movementStopwatch.IsRunning)
                        movementStopwatch.Start();
                }
                else
                {
                    movementStopwatch.Reset();
                }

                return true;
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(OnPreExecute)}: {ex.StackTrace}", 3f);
                return false;
            }
        }

        private void OnPostExecute()
        {
            try
            {
                buffs = null;
                skills = null;
                nearbyMonsterCount = null;
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(OnPostExecute)}: {ex.StackTrace}", 3f);
            }
        }

        private bool? HasBuff(string buffName)
        {
            if (buffs == null)
            {
                if (showErrors)
                    LogError("Requested buff check, but buff list is empty.", 1);
                return null;
            }

            return buffs.Any(b => string.Compare(b.Name, buffName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        private bool? HasBuffs(string buffName, int count)
        {
            if (buffs == null)
            {
                if (showErrors)
                    LogError("Requested buff check, but buff list is empty.", 1);
                return null;
            }

            return buffs.Where(b => string.Compare(b.Name, buffName, StringComparison.OrdinalIgnoreCase) == 0).Count() >= count;
        }

        private Buff GetBuff(string buffName)
        {
            if (buffs == null)
            {
                if (showErrors)
                    LogError("Requested buff retrieval, but buff list is empty.", 1);
                return null;
            }

            return buffs.FirstOrDefault(b => string.Compare(b.Name, buffName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        private ActorSkill GetUsableSkill(string skillName, string skillInternalName, int skillSlotIndex)
        {
            if (skills == null)
            {
                if (showErrors)
                    LogError("Requested usable skill, but skill list is empty.", 1);
                return null;
            }

            return skills.FirstOrDefault(s =>
                (s.Name == skillName || s.InternalName == skillInternalName));
        }

        private bool NearbyMonsterCheck()
        {
            if (!Settings.RequireMinMonsterCount.Value)
                return true;

            if (nearbyMonsterCount.HasValue)
                return nearbyMonsterCount.Value >= Settings.NearbyMonsterCount;

            var playerPosition = GameController.Game.IngameState.Data.LocalPlayer.GetComponent<Render>().Pos;

            List<Entity> localLoadedMonsters;
            lock (loadedMonstersLock)
            {
                localLoadedMonsters = new List<Entity>(loadedMonsters.Where(m => IsMonster(m)));
            }

            var maxDistance = Settings.NearbyMonsterMaxDistance.Value;
            var maxDistanceSquared = maxDistance * maxDistance;
            var monsterCount = 0;
            foreach (var monster in localLoadedMonsters)
                if (IsValidNearbyMonster(monster, playerPosition, maxDistanceSquared))
                    monsterCount++;

            nearbyMonsterCount = monsterCount;
            var result = nearbyMonsterCount.Value >= Settings.NearbyMonsterCount;
            if (Settings.Debug.Value && !result)
                LogMessage("NearbyMonstersCheck failed.", 1);
            return result;
        }

        private bool IsValidNearbyPlayer(Entity player, Vector3 playerPosition, int maxDistanceSquared)
        {
            try
            {
                if (!player.IsAlive || player.IsHidden || !player.IsTargetable ||
                    !player.IsValid || player.HasComponent<Monster>() || !player.HasComponent<Player>())
                    return false;

                var monsterPosition = player.Pos;

                var xDiff = playerPosition.X - monsterPosition.X;
                var yDiff = playerPosition.Y - monsterPosition.Y;
                var monsterDistanceSquare = xDiff * xDiff + yDiff * yDiff;

                return monsterDistanceSquare <= maxDistanceSquared;
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(IsValidNearbyPlayer)}: {ex.StackTrace}", 3f);
                return false;
            }
        }

        private bool IsValidNearbyMonster(Entity monster, Vector3 playerPosition, int maxDistanceSquared)
        {
            try
            {
                if (!monster.IsTargetable || !monster.IsAlive || !monster.IsHostile || monster.IsHidden ||
                    !monster.IsValid)
                    return false;

                var monsterPosition = monster.Pos;

                var xDiff = playerPosition.X - monsterPosition.X;
                var yDiff = playerPosition.Y - monsterPosition.Y;
                var monsterDistanceSquare = xDiff * xDiff + yDiff * yDiff;

                return monsterDistanceSquare <= maxDistanceSquared;
            }
            catch (Exception ex)
            {
                if (showErrors)
                    LogError($"Exception in {nameof(BuffUtil)}.{nameof(IsValidNearbyMonster)}: {ex.StackTrace}", 3f);
                return false;
            }
        }

        private bool IsMonster(Entity entity) => entity != null && entity.HasComponent<Monster>();

        public override void EntityAdded(Entity entity)
        {
            lock (loadedMonstersLock)
            {
                loadedMonsters.Add(entity);
            }
        }

        public override void EntityRemoved(Entity entity)
        {
            lock (loadedMonstersLock)
            {
                loadedMonsters.Remove(entity);
            }
        }
    }
}