namespace EvolvedOrgansRedux {
    public class AddDefsForEachModdedRace {
        public AddDefsForEachModdedRace(string nameOfThisMod) {
            //Go through all the ThingDefs to find out which races exist
            foreach (Verse.ThingDef def in Verse.DefDatabase<Verse.ThingDef>.AllDefs) {
                //If the race is Humanoid but not the base race of this game
                if (def.race?.Humanlike == true && !def.defName.Equals("Human")) { //Human stuff will just be solved with the XMLs.
                    new AddBodyParts(def.race, nameOfThisMod);
                    new AddMeatToRecipesFromModdedRace(def.race);
                }
            }
        }
    }
}
