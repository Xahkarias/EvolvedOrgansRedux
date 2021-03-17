namespace EvolvedOrgansRedux {
    public class AddBodyParts {
        public AddBodyParts(Verse.RaceProperties raceProperties, string nameOfThisMod) {
            bool does_have_BodyPartRecord;
            bool does_have_similar_BodyPartRecord;
            System.Collections.Generic.List<string> currentBodyPartRecordsOnModdedRace = new System.Collections.Generic.List<string>();

            //A list with all body parts the modded race has
            //Is used to check if a body part this mod would add is already a base part of the modded race
            foreach (Verse.BodyPartRecord bodyPartRecordOfModdedRace in Verse.DefDatabase<Verse.BodyDef>.GetNamed(raceProperties.body.defName).corePart.parts) {
                currentBodyPartRecordsOnModdedRace.Add(bodyPartRecordOfModdedRace.def.defName);
            }
            //Go through each BodyPart of Humans
            foreach (Verse.BodyPartRecord bodyPartRecordOfHuman in Verse.DefDatabase<Verse.BodyDef>.GetNamed("Human").corePart.parts) {
                //Only bother to look into the body part if it's not one added by the base game
                //Still needs to check body parts of other mods in case they add the same body part
                if (bodyPartRecordOfHuman.def.modContentPack.Name != "Core" && bodyPartRecordOfHuman.def.modContentPack.Name != "Royalty") {
                    //Reset the bools
                    does_have_BodyPartRecord = false;
                    does_have_similar_BodyPartRecord = false;

                    //Check if the race already has a body part by itself
                    //Don't need to add a second tail if that race already has a tail for example
                    foreach (string bodyPartRecordsOnModdedRace in currentBodyPartRecordsOnModdedRace) {
                        if (bodyPartRecordsOnModdedRace.ToLower().Equals(bodyPartRecordOfHuman.def.defName.ToLower())) {
                            does_have_BodyPartRecord = true;
                            //Check if the race has a body part that has the name of a modded body part in it's name
                            //Example: AlienRace-Tail
                        } else if (bodyPartRecordsOnModdedRace.ToLower().Contains(bodyPartRecordOfHuman.def.defName.ToLower())) {
                            does_have_similar_BodyPartRecord = true;
                            //RecipeDefs for surgeries have a list of body parts where an augment can be attached to
                            //This list gets the similar body part added
                            //Go through each of the RecipeDefs
                            foreach (Verse.RecipeDef recipeDef in Verse.DefDatabase<Verse.RecipeDef>.AllDefs) {
                                //Check if the RecipeDef has the body part we currently want to add already in it's list
                                //If yes: Add the similar body part and refresh the list by clearing and resolving the data
                                if (recipeDef.appliedOnFixedBodyParts.Exists(e => e.defName.Contains(bodyPartRecordOfHuman.def.defName))) {
                                    recipeDef.appliedOnFixedBodyParts.Add(Verse.DefDatabase<Verse.BodyPartDef>.GetNamed(bodyPartRecordsOnModdedRace));
                                    recipeDef.ClearCachedData();
                                    recipeDef.ResolveReferences();
                                }
                            }
                        }
                    }
                    //If the non-Human race doesn't already have this body part or a similar one AND if that body part is part of this mod
                    if (!does_have_BodyPartRecord && !does_have_similar_BodyPartRecord && bodyPartRecordOfHuman.def.modContentPack.Name == nameOfThisMod) {
                        //Copy the body part from the Human to the non-Human races body
                        Verse.BodyPartRecord newBodyPartRecordForModdedRace = new Verse.BodyPartRecord {
                            def = bodyPartRecordOfHuman.def,
                            customLabel = bodyPartRecordOfHuman.customLabel,
                            coverage = bodyPartRecordOfHuman.coverage,
                            groups = bodyPartRecordOfHuman.groups
                        };
                        Verse.DefDatabase<Verse.BodyDef>.GetNamed(raceProperties.body.defName).corePart.parts.Add(newBodyPartRecordForModdedRace);
                    }
                }
                //Clear the list of all body parts because ResolveAllReferences only appends all body parts
                Verse.DefDatabase<Verse.BodyDef>.GetNamed(raceProperties.body.defName).AllParts.Clear();
                Verse.DefDatabase<Verse.BodyDef>.GetNamed(raceProperties.body.defName).ResolveReferences();
            }
        }
    }
}