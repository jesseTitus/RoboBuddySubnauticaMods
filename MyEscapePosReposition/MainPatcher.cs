using System.Reflection;
using Harmony;

namespace MyEscapePodReposition
{
    public class MainPatcher
    {
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("com.robobuddy.subnautica.mod");
            harmony.PatchAll((Assembly.GetExecutingAssembly()));
        }
    }
}
