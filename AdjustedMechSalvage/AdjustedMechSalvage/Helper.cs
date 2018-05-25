using BattleTech;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AdjustedMechSalvage {
    public class Helper {

        public static Settings LoadSettings() {
            try {
                using (StreamReader r = new StreamReader("mods/AdjustedMechSalvage/settings.json")) {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<Settings>(json);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return null;
            }
        }

        public static SalvageDef CreateMechPart(Contract contract, SimGameConstants sc, MechDef m) {
            SalvageDef salvageDef = new SalvageDef();
            salvageDef.Type = SalvageDef.SalvageType.MECH_PART;
            salvageDef.ComponentType = ComponentType.MechPart;
            salvageDef.Count = 1;
            salvageDef.Weight = sc.Salvage.DefaultMechPartWeight;
            DescriptionDef description = m.Description;
            DescriptionDef description2 = new DescriptionDef(description.Id, string.Format("{0} {1}", description.Name, sc.Story.DefaultMechPartName), description.Details, description.Icon, description.Cost, description.Rarity, description.Purchasable, description.Manufacturer, description.Model, description.UIName);
            salvageDef.Description = description2;
            salvageDef.RewardID = contract.GenerateRewardUID();
            return salvageDef;
        }
    }
}