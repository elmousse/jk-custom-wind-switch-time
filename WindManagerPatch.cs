using System;
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
            var cyclePatch = false;
            var velocityPatch1 = false;
            var velocityPatch2 = false;

            for (var i = 0; i < codes.Count - 2; i++)
            {
                if (!cyclePatch &&
                    codes[i].opcode == OpCodes.Call &&
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

                    cyclePatch = true;
                }

                if (!velocityPatch1 &&
                    codes[i].opcode == OpCodes.Ldloc_0 &&
                    codes[i+1].opcode == OpCodes.Ldc_R4 &&
                    (float)codes[i+1].operand == 0.1f &&
                    codes[i + 2].opcode == OpCodes.Mul)
                {
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Dup));
                    
                    codes.Insert(i+2, new CodeInstruction(OpCodes.Call,
                        typeof(ComputeWindVelocity).GetMethod("Execute", new Type[] { typeof(float) })));
                    
                    codes.Insert(i+3, new CodeInstruction(OpCodes.Mul));
                    
                    velocityPatch1 = true;
                }

                if (!velocityPatch2 &&
                    codes[i].opcode == OpCodes.Ldloc_0 &&
                    codes[i+1].opcode == OpCodes.Ldc_R4 &&
                    (float)codes[i+1].operand == 0.0125f &&
                    codes[i + 2].opcode == OpCodes.Call &&
                    codes[i + 2].operand is MethodInfo methodInfo2 &&
                    methodInfo2.Name == "get_CurrentScreen" &&
                    codes[i + 3].opcode == OpCodes.Callvirt &&
                    codes[i + 3].operand is MethodInfo methodInfo3 &&
                    methodInfo3.Name == "get_WindIntensity" &&
                    codes[i + 4].opcode == OpCodes.Mul)
                {
                    codes.Insert(i+1, new CodeInstruction(OpCodes.Dup));
                    
                    codes.Insert(i+2, new CodeInstruction(OpCodes.Call,
                        typeof(ComputeWindVelocity).GetMethod("Execute", new Type[] { typeof(float) })));
                    
                    codes.Insert(i+3, new CodeInstruction(OpCodes.Mul));
                    
                    velocityPatch2 = true;
                }

                if (cyclePatch && velocityPatch1 && velocityPatch2)
                {
                    break;
                }
            }
            
            return codes.AsEnumerable();
        }
    }
}