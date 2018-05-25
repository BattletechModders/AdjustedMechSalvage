using Harmony;
using System.Reflection;

namespace AdjustedMechSalvage
{
    public class AdjustedMechSalvage
    {
        public static void Init() {
            var harmony = HarmonyInstance.Create("de.morphyum.AdjustedMechSalvage");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
