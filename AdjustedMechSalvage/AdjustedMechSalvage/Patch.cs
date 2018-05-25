using BattleTech;
using Harmony;
using System.Collections.Generic;


namespace AdjustedMechSalvage {

    [HarmonyPatch(typeof(Contract), "GenerateSalvage")]
    public static class Contract_GenerateSalvage_Patch {
        static void Postfix(Contract __instance, List<UnitResult> enemyMechs, List<VehicleDef> enemyVehicles, List<UnitResult> lostUnits, bool logResults = false) {
           
        }
    }
}