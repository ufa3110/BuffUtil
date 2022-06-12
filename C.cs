using System;

namespace BuffUtil
{
    public static class C
    {
        public static class SteelSkin
        {
            public const string BuffName = "steelskin";
            public const string Name = "QuickGuard";
            public const string InternalName = "steelskin";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class ImmortalCall
        {
            public const string BuffName = "mortal_call";
            public const string Name = "ImmortalCall";
            public const string InternalName = "mortal_call";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class MoltenShell
        {
            public const string BuffName = "molten_shell_shield";
            public const string Name = "MoltenShell";
            public const string InternalName = "molten_shell_barrier";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class PhaseRun
        {
            public const string BuffName = "new_phase_run";
            public const string BuffName2 = "new_phase_run_damage";
            public const string Name = "NewPhaseRun";
            public const string InternalName = "new_phase_run";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class WitheringStep
        {
            public const string BuffName = "slither";
            public const string Name = "Slither";
            public const string InternalName = "slither";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(3);
        }

        public static class BloodRage
        {
            public const string BuffName = "blood_rage";
            public const string Name = "BloodRage";
            public const string InternalName = "blood_rage";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        //my add
        public static class Wrath
        {
            public const string BuffName = "player_aura_lightning_damage";
            public const string Name = "Wrath";
            public const string InternalName = "wrath";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class Zealotry
        {
            public const string BuffName = "player_aura_spell_damage";
            public const string Name = "SpellDamageAura";
            public const string InternalName = "spell_damage_aura";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class Hatred
        {
            public const string BuffName = "player_aura_cold_damage";
            public const string Name = "Hatred";
            public const string InternalName = "hatred";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class Haste
        {
            public const string BuffName = "player_aura_speed";
            public const string Name = "Haste";
            public const string InternalName = "haste";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class Discipline
        {
            public const string BuffName = "player_aura_energy_shield";
            public const string Name = "Discipline";
            public const string InternalName = "discipline";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class Grace
        {
            public const string BuffName = "player_aura_evasion";
            public const string Name = "Grace";
            public const string InternalName = "grace";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class Determination
        {
            public const string BuffName = "player_aura_armour";
            public const string Name = "Determination";
            public const string InternalName = "determination";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class DefianceBanner
        {
            public const string BuffName = "armour_evasion_banner_buff_aura";
            public const string Name = "DefianceBanner";
            public const string InternalName = "banner_armour_evasion";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class PurityOfIce
        {
            public const string BuffName = "player_aura_cold_resist";
            public const string Name = "ColdResistAura";
            public const string InternalName = "cold_resist_aura";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class PurityOfFire
        {
            public const string BuffName = "player_aura_fire_resist";
            public const string Name = "FireResistAura";
            public const string InternalName = "fire_resist_aura";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class PurityOfLightning
        {
            public const string BuffName = "player_aura_lightning_resist";
            public const string Name = "LightningResistAura";
            public const string InternalName = "lightning_resist_aura";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class Vitality
        {
            public const string BuffName = "player_aura_life_regen";
            public const string Name = "Vitality";
            public const string InternalName = "vitality";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class Precision
        {
            public const string BuffName = "player_aura_accuracy_and_crits";
            public const string Name = "AccuracyAndCritsAura";
            public const string InternalName = "aura_accuracy_and_crits";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class Clarity
        {
            public const string BuffName = "player_aura_mana_regen";
            public const string Name = "Clarity";
            public const string InternalName = "clarity";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
        public static class VaalDiscipline
        {
            public const string BuffName = "vaal_aura_energy_shield";
            public const string Name = "VaalAuraEnergyShield";
            public const string InternalName = "vaal_discipline";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class VaalGrace
        {
            public const string BuffName = "vaal_aura_dodge";
            public const string Name = "VaalAuraDodge";
            public const string InternalName = "vaal_grace";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class VaalMoltenShell
        {
            public const string BuffName = "molten_shell_shield";
            public const string Name = "FireShield";
            public const string InternalName = "vaal_molten_shell";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }

        public static class xyz
        {
            public const string BuffName = "trigger_link_source";
            public const string Name = "IntuitiveLink";
            public const string InternalName = "intuitive_link";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(8);
        }

        public static class xyz2
        {
            public const string BuffName = "soul_link_source";
            public const string Name = "SoulLink";
            public const string InternalName = "soul_link";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(0.5f);
        }

        //my end

        public static class BladeFlurry
        {
            public const string BuffName = "charged_attack";
        }

        public static class ScourgeArrow
        {
            public const string BuffName = "virulent_arrow_counter";
        }

        public static class InfusedChanneling
        {
            public const string BuffName = "storm_barrier_support_damage";
        }

        public static class GracePeriod
        {
            public const string BuffName = "grace_period";
        }

        public static class BladeVortex
        {
            public const string BuffName = "blade_vortex_counter";
            public static readonly TimeSpan TimeBetweenCasts = TimeSpan.FromSeconds(1);
        }
    }
}