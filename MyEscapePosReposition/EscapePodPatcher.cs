using System;
using Harmony;
using UnityEngine;

namespace MyEscapePodReposition
{
    [HarmonyPatch(typeof(EscapePod),"StartAtPosition")]
    internal class EscapePodPatcher
    {
        [HarmonyPrefix]
        public static bool Prefix(ref Vector3 position, EscapePod __instance)
        {
            __instance.transform.position = position;
            __instance.anchorPosition = position;
            __instance.RespawnPlayer();
            return false;           //this means don't run original method
        }
    }
}
