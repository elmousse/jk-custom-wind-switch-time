using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using JumpKing;

namespace CustomWindSwitch
{
    [HarmonyPatch(typeof(WindManager), "CurrentVelocityRaw", MethodType.Getter)]
    public static class WindManagerPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count - 2; i++)
            {
                if (codes[i].opcode == OpCodes.Call &&
                    codes[i].operand is MethodInfo methodInfo &&
                    methodInfo.Name == "get_TotalSeconds" &&
                    codes[i + 1].opcode == OpCodes.Conv_R4 &&
                    codes[i + 2].opcode == OpCodes.Ldc_R4 &&
                    (float)codes[i + 2].operand == 0.48124886f &&
                    codes[i + 3].opcode == OpCodes.Mul)
                {
                    codes.RemoveAt(i + 2);
                    codes.RemoveAt(i + 2);

                    codes.Insert(i + 2, new CodeInstruction(OpCodes.Call,
                        typeof(ComputeWindCycle).GetMethod("Execute", BindingFlags.Static | BindingFlags.Public)));

                    break;
                }
            }

            return codes.AsEnumerable();
        }
    }
}


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using CustomWindSwitch;
using HarmonyLib;
using JumpKing;

namespace CustomWindSwitch
{
    [HarmonyPatch(typeof(WindManager), "CurrentVelocityRaw", MethodType.Getter)]
    public static class WindManagerPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_R4)
                {
                    if ((float)codes[i].operand == 0.481248856f)
                    {
                        codes[i] = new CodeInstruction(OpCodes.Call,
                            typeof(CustomWindSwitchApi).GetProperty("Instance").GetGetMethod());

                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Call,
                            typeof(CustomWindSwitchApi).GetProperty("CurrentFrequency").GetGetMethod()));
                    }
                }
            }
            return codes.AsEnumerable();
        }
    }
}

float num1 = (float) Math.Sin(d = timeSpan.TotalSeconds * 0.4812488555908203);

float num1 = (float) Math.Sin(d = ComputeWindCycle.Execute(timeSpan.TotalSeconds));*/