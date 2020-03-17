using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace EvolvedOrgansRedux
{

    [StaticConstructorOnStartup]
    public static class Initializer
    {
        static Initializer()
        {
            Harmony harmony = new Harmony("EvolvedOrgansRedux");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(AgeInjuryUtility))]
    [HarmonyPatch("RandomHediffsToGainOnBirthday")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(int) })]
    static class PreventAgeHediffsIfImmortal_Patch
    {
        

        [HarmonyPrefix]
        public static bool PreventAgeInjuries_Prefix(Pawn pawn, ref IEnumerable<HediffGiver_Birthday> __result)
        {
            if (pawn.health.hediffSet.HasHediff(HediffDef.Named("EVOR_Hediff_Abd_NoAge")))
            {
                Log.Message("no hediff added, pawn is immortal");
                __result = System.Linq.Enumerable.Empty<HediffGiver_Birthday>();
                return false;
            }
            else
            {
                return true;
            }
        }
    }


}