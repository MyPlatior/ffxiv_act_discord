using System.Collections.Generic;

namespace FFXIV_Discord
{
    static class UIStrings
    {
        public readonly static Dictionary<Sharlayan.Core.Enums.Actor.Job, string> JobNames = new Dictionary<Sharlayan.Core.Enums.Actor.Job, string>
        {
            { Sharlayan.Core.Enums.Actor.Job.Unknown, "Unknown"},
            //classes
            { Sharlayan.Core.Enums.Actor.Job.GLD, "Gladiator" },
            { Sharlayan.Core.Enums.Actor.Job.PGL, "Pugilist" },
            { Sharlayan.Core.Enums.Actor.Job.MRD, "Marauder" },
            { Sharlayan.Core.Enums.Actor.Job.LNC, "Lancer" },
            { Sharlayan.Core.Enums.Actor.Job.ARC, "Archer" },
            { Sharlayan.Core.Enums.Actor.Job.CNJ, "Conjurer" },
            { Sharlayan.Core.Enums.Actor.Job.THM, "Thaumaturge" },
            { Sharlayan.Core.Enums.Actor.Job.ACN, "Arcanist" },
            { Sharlayan.Core.Enums.Actor.Job.ROG, "Rogue" },
            //crafters
            { Sharlayan.Core.Enums.Actor.Job.CPT, "Carpenter" },
            { Sharlayan.Core.Enums.Actor.Job.BSM, "Blacksmith" },
            { Sharlayan.Core.Enums.Actor.Job.ARM, "Armorer" },
            { Sharlayan.Core.Enums.Actor.Job.GSM, "Goldsmith" },
            { Sharlayan.Core.Enums.Actor.Job.LTW, "Leatherworker" },
            { Sharlayan.Core.Enums.Actor.Job.WVR, "Weaver" },
            { Sharlayan.Core.Enums.Actor.Job.ALC, "Alchemist" },
            { Sharlayan.Core.Enums.Actor.Job.CUL, "Culinarian" },
            //gatherers
            { Sharlayan.Core.Enums.Actor.Job.MIN, "Miner" },
            { Sharlayan.Core.Enums.Actor.Job.FSH, "Fisher" },
            { Sharlayan.Core.Enums.Actor.Job.BTN, "Botanist" },
            //jobs
            { Sharlayan.Core.Enums.Actor.Job.PLD, "Paladin" },
            { Sharlayan.Core.Enums.Actor.Job.WAR, "Warrior" },
            { Sharlayan.Core.Enums.Actor.Job.DRK, "Dark Knight" },
            { Sharlayan.Core.Enums.Actor.Job.WHM, "White Mage" },
            { Sharlayan.Core.Enums.Actor.Job.SCH, "Scholar" },
            { Sharlayan.Core.Enums.Actor.Job.AST, "Astrologian" },
            { Sharlayan.Core.Enums.Actor.Job.MNK, "Monk" },
            { Sharlayan.Core.Enums.Actor.Job.DRG, "Dragoon" },
            { Sharlayan.Core.Enums.Actor.Job.NIN, "Ninja" },
            { Sharlayan.Core.Enums.Actor.Job.SAM, "Samurai" },
            { Sharlayan.Core.Enums.Actor.Job.BRD, "Bard" },
            { Sharlayan.Core.Enums.Actor.Job.MCH, "Machinist" },
            { Sharlayan.Core.Enums.Actor.Job.BLM, "Black Mage" },
            { Sharlayan.Core.Enums.Actor.Job.SMN, "Summoner" },
            { Sharlayan.Core.Enums.Actor.Job.RDM, "Red Mage" },
            { Sharlayan.Core.Enums.Actor.Job.BLU, "Blue Mage" },
            { Sharlayan.Core.Enums.Actor.Job.GNB, "Gunbreaker" },
            { Sharlayan.Core.Enums.Actor.Job.DNC, "Dancer" }
        };
        public readonly static Dictionary<Sharlayan.Core.Enums.Actor.Job, string> JobAbbreviations = new Dictionary<Sharlayan.Core.Enums.Actor.Job, string>
        {
            { Sharlayan.Core.Enums.Actor.Job.Unknown, "???"},
            //classes
            { Sharlayan.Core.Enums.Actor.Job.GLD, "GLA" },
            { Sharlayan.Core.Enums.Actor.Job.PGL, "PGL" },
            { Sharlayan.Core.Enums.Actor.Job.MRD, "MRD" },
            { Sharlayan.Core.Enums.Actor.Job.LNC, "LNC" },
            { Sharlayan.Core.Enums.Actor.Job.ARC, "ARC" },
            { Sharlayan.Core.Enums.Actor.Job.ACN, "ACN" },
            { Sharlayan.Core.Enums.Actor.Job.CNJ, "CNJ" },
            { Sharlayan.Core.Enums.Actor.Job.THM, "THM" },
            { Sharlayan.Core.Enums.Actor.Job.ROG, "ROG" },
            //crafters
            { Sharlayan.Core.Enums.Actor.Job.CPT, "CRP" },
            { Sharlayan.Core.Enums.Actor.Job.BSM, "BSM" },
            { Sharlayan.Core.Enums.Actor.Job.ARM, "ARM" },
            { Sharlayan.Core.Enums.Actor.Job.GSM, "GSM" },
            { Sharlayan.Core.Enums.Actor.Job.LTW, "LTW" },
            { Sharlayan.Core.Enums.Actor.Job.WVR, "WVR" },
            { Sharlayan.Core.Enums.Actor.Job.ALC, "ALC" },
            { Sharlayan.Core.Enums.Actor.Job.CUL, "CUL" },
            //gatherers
            { Sharlayan.Core.Enums.Actor.Job.MIN, "MIN" },
            { Sharlayan.Core.Enums.Actor.Job.FSH, "FSH" },
            { Sharlayan.Core.Enums.Actor.Job.BTN, "BTN" },
            //jobs
            { Sharlayan.Core.Enums.Actor.Job.PLD, "PLD" },
            { Sharlayan.Core.Enums.Actor.Job.WAR, "WAR" },
            { Sharlayan.Core.Enums.Actor.Job.DRK, "DRK" },
            { Sharlayan.Core.Enums.Actor.Job.WHM, "WHM" },
            { Sharlayan.Core.Enums.Actor.Job.SCH, "SCH" },
            { Sharlayan.Core.Enums.Actor.Job.AST, "AST" },
            { Sharlayan.Core.Enums.Actor.Job.MNK, "MNK" },
            { Sharlayan.Core.Enums.Actor.Job.DRG, "DRG" },
            { Sharlayan.Core.Enums.Actor.Job.NIN, "NIN" },
            { Sharlayan.Core.Enums.Actor.Job.SAM, "SAM" },
            { Sharlayan.Core.Enums.Actor.Job.BRD, "BRD" },
            { Sharlayan.Core.Enums.Actor.Job.MCH, "MCH" },
            { Sharlayan.Core.Enums.Actor.Job.BLM, "BLM" },
            { Sharlayan.Core.Enums.Actor.Job.SMN, "SMN" },
            { Sharlayan.Core.Enums.Actor.Job.RDM, "RDM" },
            { Sharlayan.Core.Enums.Actor.Job.BLU, "BLU" },
            { Sharlayan.Core.Enums.Actor.Job.GNB, "GNB" },
            { Sharlayan.Core.Enums.Actor.Job.DNC, "DNC" }
        };
    }
}
