using BattleTech;
using Harmony;
using HBS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdjustedMechSalvage {

    [HarmonyPatch(typeof(Contract), "GenerateSalvage")]
    public static class Contract_GenerateSalvage_Patch {
        static bool Prefix(Contract __instance, List<UnitResult> enemyMechs, List<VehicleDef> enemyVehicles, List<UnitResult> lostUnits, bool logResults = false) {
            try {
                Settings settings = Helper.LoadSettings();
                if (__instance.BattleTechGame.Simulation == null) {
                    return false;
                }
                ReflectionHelper.SetPrivateField(__instance, "finalPotentialSalvage", new List<SalvageDef>());
                ReflectionHelper.InvokePrivateMethode(__instance, "set_SalvagedChassis", new object[] { new List<SalvageDef>() });
                ReflectionHelper.InvokePrivateMethode(__instance, "set_LostMechs", new object[] { new List<MechDef>() });
                ReflectionHelper.InvokePrivateMethode(__instance, "set_SalvageResults", new object[] { new List<SalvageDef>() });
                SimGameState simulation = __instance.BattleTechGame.Simulation;
                SimGameConstants constants = simulation.Constants;
                for (int i = 0; i < lostUnits.Count; i++) {
                    MechDef mech = lostUnits[i].mech;
                    float num = simulation.NetworkRandom.Float(0f, 1f);
                    bool flag = num <= constants.Salvage.DestroyedMechRecoveryChance;
                    if (flag) {
                        lostUnits[i].mechLost = false;
                    }
                    else {
                        lostUnits[i].mechLost = true;

                        float mechparts = simulation.Constants.Story.DefaultMechPartMax;
                        if (mech.IsLocationDestroyed(ChassisLocations.CenterTorso)) {
                            mechparts = 0;
                        }
                        else {
                            if (mech.IsLocationDestroyed(ChassisLocations.LeftArm)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                            if (mech.IsLocationDestroyed(ChassisLocations.RightArm)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                            if (mech.IsLocationDestroyed(ChassisLocations.LeftLeg)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                            if (mech.IsLocationDestroyed(ChassisLocations.RightLeg)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                        }

                        SalvageDef def = Helper.CreateMechPart(__instance, constants, mech);
                        if (settings.ownMechsForFree) {
                            for (int s = 0; s < Mathf.RoundToInt(mechparts); s++) {
                                __instance.SalvageResults.Add(def);
                            }
                        }
                        else {
                            List<SalvageDef> finalPotentialSalvage = (List<SalvageDef>)ReflectionHelper.GetPrivateField(__instance, "finalPotentialSalvage");
                            ReflectionHelper.InvokePrivateMethode(__instance, "CreateAndAddMechPart", new object[] { constants, mech, Mathf.RoundToInt(mechparts), finalPotentialSalvage });
                        }
                        if (settings.ownMechsForFree) {
                            foreach (MechComponentRef mechComponentRef in mech.Inventory) {
                                if (!mech.IsLocationDestroyed(mechComponentRef.MountedLocation) && mechComponentRef.DamageLevel != ComponentDamageLevel.Destroyed) {
                                    ReflectionHelper.InvokePrivateMethode(__instance, "AddToFinalSalvage", new object[] { new SalvageDef {
                                    MechComponentDef = mechComponentRef.Def,
                                    Description = new DescriptionDef(mechComponentRef.Def.Description),
                                    RewardID = __instance.GenerateRewardUID(),
                                    Type = SalvageDef.SalvageType.COMPONENT,
                                    ComponentType = mechComponentRef.Def.ComponentType,
                                    Damaged = false,
                                    Count = 1
                                } });
                                }
                            }
                        } else {
                            foreach (MechComponentRef mechComponentRef in mech.Inventory) {
                                if (!mech.IsLocationDestroyed(mechComponentRef.MountedLocation) && mechComponentRef.DamageLevel != ComponentDamageLevel.Destroyed) {
                                    List<SalvageDef> finalPotentialSalvage = (List<SalvageDef>)ReflectionHelper.GetPrivateField(__instance, "finalPotentialSalvage");
                                    ReflectionHelper.InvokePrivateMethode(__instance, "AddMechComponentToSalvage", new object[] { finalPotentialSalvage, mechComponentRef.Def, ComponentDamageLevel.Functional, false, constants, simulation.NetworkRandom, true });
                                }
                            }
                        }
                    }
                }
                int k = 0;
                while (k < enemyMechs.Count) {
                    MechDef mech2 = enemyMechs[k].mech;
                    Pilot pilot = enemyMechs[k].pilot;
                    SalvageDef salvageDef = null;
                    bool flag2 = false;
                    bool flag3 = false;
                    List<SalvageDef> finalPotentialSalvage = (List<SalvageDef>)ReflectionHelper.GetPrivateField(__instance, "finalPotentialSalvage");
        
                    if (pilot.IsIncapacitated || mech2.IsDestroyed) {
                        float mechparts = simulation.Constants.Story.DefaultMechPartMax;
                        if (mech2.IsLocationDestroyed(ChassisLocations.CenterTorso)) {
                            mechparts = 0;
                        }
                        else {
                            if (mech2.IsLocationDestroyed(ChassisLocations.LeftArm)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                            if (mech2.IsLocationDestroyed(ChassisLocations.RightArm)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                            if (mech2.IsLocationDestroyed(ChassisLocations.LeftLeg)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                            if (mech2.IsLocationDestroyed(ChassisLocations.RightLeg)) {
                                mechparts -= (simulation.Constants.Story.DefaultMechPartMax / 5);
                            }
                        }
                        ReflectionHelper.InvokePrivateMethode(__instance, "CreateAndAddMechPart", new object[] { constants, mech2, mechparts, finalPotentialSalvage });
                        goto IL_2E4;
                    }
                    IL_4AB:
                    k++;
                    continue;
                    IL_2E4:
                    foreach (MechComponentRef mechComponentRef2 in mech2.Inventory) {
                        if (!mech2.IsLocationDestroyed(mechComponentRef2.MountedLocation) && mechComponentRef2.DamageLevel != ComponentDamageLevel.Destroyed) {
                            ReflectionHelper.InvokePrivateMethode(__instance, "AddMechComponentToSalvage", new object[] { finalPotentialSalvage, mechComponentRef2.Def, ComponentDamageLevel.Functional, false, constants, simulation.NetworkRandom, true });
                        }
                    }
                    if (flag3) {
                        salvageDef = new SalvageDef();
                        salvageDef.Type = SalvageDef.SalvageType.CHASSIS;
                        salvageDef.Description = new DescriptionDef(mech2.Chassis.Description);
                        salvageDef.Count = 1;
                        IEnumerator enumerator = Enum.GetValues(typeof(ChassisLocations)).GetEnumerator();
                        try {
                            while (enumerator.MoveNext()) {
                                object obj = enumerator.Current;
                                ChassisLocations chassisLocations = (ChassisLocations)obj;
                                if (chassisLocations != ChassisLocations.None && chassisLocations != ChassisLocations.All && chassisLocations != ChassisLocations.Arms && chassisLocations != ChassisLocations.Legs && chassisLocations != ChassisLocations.Torso && chassisLocations != ChassisLocations.MainBody) {
                                    salvageDef.ChassisLocations.Add(chassisLocations);
                                    salvageDef.ChassisStructure.Add(mech2.GetLocationLoadoutDef(chassisLocations).CurrentInternalStructure);
                                }
                            }
                        }
                        finally {
                            IDisposable disposable;
                            if ((disposable = (enumerator as IDisposable)) != null) {
                                disposable.Dispose();
                            }
                        }
                        if (flag2) {
                            for (int m = 0; m < salvageDef.ChassisLocations.Count; m++) {
                                List<float> chassisStructure;
                                int index;
                                (chassisStructure = salvageDef.ChassisStructure)[index = m] = chassisStructure[index] / 2f;
                            }
                        }
                        salvageDef.Weight = constants.Salvage.DefaultChassisWeight;
                        __instance.SalvagedChassis.Add(salvageDef);
                        goto IL_4AB;
                    }
                    goto IL_4AB;
                }
                for (int n = 0; n < enemyVehicles.Count; n++) {
                    VehicleDef vehicleDef = enemyVehicles[n];
                    List<SalvageDef> finalPotentialSalvage = (List<SalvageDef>)ReflectionHelper.GetPrivateField(__instance, "finalPotentialSalvage");
                    foreach (VehicleComponentRef vehicleComponentRef in vehicleDef.Inventory) {
                        ReflectionHelper.InvokePrivateMethode(__instance, "AddMechComponentToSalvage", new object[] { finalPotentialSalvage, vehicleComponentRef.Def, ComponentDamageLevel.Functional, false, constants, simulation.NetworkRandom, true });

                    }
                }
                int num2 = __instance.SalvagePotential;
                float num3 = constants.Salvage.VictorySalvageChance;
                float num4 = constants.Salvage.VictorySalvageLostPerMechDestroyed;
                if (__instance.State == Contract.ContractState.Failed) {
                    num3 = constants.Salvage.DefeatSalvageChance;
                    num4 = constants.Salvage.DefeatSalvageLostPerMechDestroyed;
                }
                else if (__instance.State == Contract.ContractState.Retreated) {
                    num3 = constants.Salvage.RetreatSalvageChance;
                    num4 = constants.Salvage.RetreatSalvageLostPerMechDestroyed;
                }
                float num5 = num3;
                float num6 = (float)num2 * __instance.PercentageContractSalvage;
                if (num2 > 0) {
                    num6 += (float)constants.Finances.ContractFloorSalvageBonus;
                }
                num3 = Mathf.Max(0f, num5 - num4 * (float)lostUnits.Count);
                int num7 = Mathf.FloorToInt(num6 * num3);
                if (num2 > 0) {
                    num2 += constants.Finances.ContractFloorSalvageBonus;
                }
                ReflectionHelper.InvokePrivateMethode(__instance, "set_FinalSalvageCount", new object[] { num7 });
                ReflectionHelper.InvokePrivateMethode(__instance, "set_FinalPrioritySalvageCount", new object[] { Mathf.FloorToInt((float)num7 * constants.Salvage.PrioritySalvageModifier) });

                return false;
            }
            catch (Exception e) {
                Logger.LogError(e);
                return false;

            }
        }
    }
}