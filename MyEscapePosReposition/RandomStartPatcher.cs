using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;

namespace MyEscapePodReposition
{
    [HarmonyPatch(typeof(RandomStart), "GetRandomStartPoint")]
    internal class RandomStartIsStartPointValidPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(EscapePod __instance, ref Vector3 __result)
        {
            string line;
            var path = @"./QMods/MyEscapePodReposition/config.txt";

            System.IO.StreamReader file = new StreamReader(path);
            line = file.ReadLine();
            line = line.Substring(10);
            Console.WriteLine("Spawn selected: " + line);

            switch (line)
            {
                case "Custom":
                    string customSelection = file.ReadLine();
                    string[] coords = customSelection.Split(',');
                    __result = new Vector3((float)Convert.ToDouble(
                                                                    coords[0]),
                        (float)Convert.ToDouble(coords[1]), 
                        (float)Convert.ToDouble((coords[2])));
                    break;
                case "Random":
                    bool isBad = false;
                    Console.WriteLine("Adjusting spawn location due to Aurora vicinity");

                    do
                    {
                        __result = new Vector3(UnityEngine.Random.Range(-1250f, 1250f),
                                                                             UnityEngine.Random.Range(-1250f,1250f));
                        if (__result.x > 543 && __result.x < 1724 && __result.z > -574 && __result.z < 400)
                        {
                            Console.WriteLine("Adjusting spawn again, still too close!");
                            isBad = true;
                        }
                        else
                        {
                            isBad = false;
                        }

                    } while (isBad);
                    Console.WriteLine("Good random spawn found at " + __result);
                    break;

                case "Landfall": //randomly on one of the two islands
                    int randInt = UnityEngine.Random.Range(0, 2);

                    if (randInt == 0)
                    {
                        __result = new Vector3(UnityEngine.Random.Range(228f, 401f), 200f, UnityEngine.Random.Range(770f, 1130f));
                    }
                    else
                    {
                        __result = new Vector3(UnityEngine.Random.Range(-905f, -637f), 200f, UnityEngine.Random.Range(-1157f, -922f));
                    }

                    break;

                case "Oasis": //the nice little puddle in the center of the floating island
                    __result = new Vector3(-711, 200, -1075);
                    break;
                case "Safe Shallows": //use default spawn rules
                    return true;
                case "Mountain Island":
                    __result = new Vector3(UnityEngine.Random.Range(228f, 401f), 200f, UnityEngine.Random.Range(770f, 1130f));
                    break;
                case "Floating Island":
                    __result = new Vector3(UnityEngine.Random.Range(-905f, -637f), 200f, UnityEngine.Random.Range(-1157f, -922f));
                    break;
                case "Dunes":
                    __result = new Vector3(UnityEngine.Random.Range(-1689f, -1248f), 100f, UnityEngine.Random.Range(-10f, 1009f));
                    break;
                default: /* Optional */
                    Console.WriteLine("Defaulting to random");
                    __result = new Vector3(UnityEngine.Random.Range(-1250f, 1250f), 200f, UnityEngine.Random.Range(-1250f, 1250f));
                    break;
            }

            Console.WriteLine("Spawn Coordinates:");
            Console.WriteLine(__result);
            return false;
        }
    }
}
