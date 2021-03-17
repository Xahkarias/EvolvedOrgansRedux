using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolvedOrgansRedux {
    public class AddMeatToRecipesFromModdedRace {
        public AddMeatToRecipesFromModdedRace(Verse.RaceProperties raceProperties) {
            if (!Verse.ModLister.HasActiveModWithName("Questionable Ethics Enhanced")) {
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_Production_Protein_Humie").fixedIngredientFilter.SetAllow(raceProperties.meatDef, true);
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_Production_Protein_Humie").ClearCachedData();
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_Production_Protein_Humie").ResolveReferences();
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_Production_Protein_Humie_Bulk").fixedIngredientFilter.SetAllow(raceProperties.meatDef, true);
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_Production_Protein_Humie_Bulk").ClearCachedData();
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_Production_Protein_Humie_Bulk").ResolveReferences();
            } else {
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_QEE_Production_Protein_Humie").fixedIngredientFilter.SetAllow(raceProperties.meatDef, true);
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_QEE_Production_Protein_Humie").ClearCachedData();
                Verse.DefDatabase<Verse.RecipeDef>.GetNamed("EVOR_QEE_Production_Protein_Humie").ResolveReferences();
            }
        }
    }
}
