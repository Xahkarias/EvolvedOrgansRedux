using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace EvolvedOrgansRedux {

    [HarmonyPatch(typeof(AgeInjuryUtility))]
    [HarmonyPatch("RandomHediffsToGainOnBirthday")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(int) })]
    static class PreventAgeHediffsIfImmortal_Patch {


        [HarmonyPrefix]
        public static bool PreventAgeInjuries_Prefix(Pawn pawn, ref IEnumerable<HediffGiver_Birthday> __result) {
            if (pawn.health.hediffSet.HasHediff(HediffDef.Named("EVOR_Hediff_Abd_NoAge"))) {
                Log.Message("no hediff added, pawn is immortal");
                __result = System.Linq.Enumerable.Empty<HediffGiver_Birthday>();
                return false;
            } else {
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("PostApplyDamage")]
    [HarmonyPatch(new Type[] { typeof(DamageInfo), typeof(float) })]
    static class ApplyPlagueToAttacker_Patch {

        [HarmonyPostfix]
        public static void ApplyPlagueToAttacker_Postfix(DamageInfo dinfo, Pawn __instance) {
            if (__instance.health.hediffSet.HasHediff(HediffDef.Named("EVOR_Hediff_Artifact_Lesions"))) {
                if (!dinfo.Def.isRanged) {
                    if (((Pawn)dinfo.Instigator).health.hediffSet.HasHediff(HediffDef.Named("EVOR_Hediff_Damage_PurgleRot"))) {
                        ((Pawn)dinfo.Instigator).health.hediffSet.GetFirstHediffOfDef(HediffDef.Named("EVOR_Hediff_Damage_PurgleRot")).Severity += (float)0.05;
                    } else {
                        ((Pawn)dinfo.Instigator).health.AddHediff(HediffDef.Named("EVOR_Hediff_Damage_PurgleRot"));
                    }
                }
            }
        }
    }
}