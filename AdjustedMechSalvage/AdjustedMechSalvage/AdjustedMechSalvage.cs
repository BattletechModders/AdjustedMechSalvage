using Harmony;
using System.Reflection;

namespace AdjustedMechSalvage
{
    public class AdjustedMechSalvage
    {
        internal static string ModDirectory;
        public static void Init(string directory, string settingsJSON) {
            var harmony = HarmonyInstance.Create("de.morphyum.AdjustedMechSalvage");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            ModDirectory = directory;
        }
    }
}
