using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
                if (codes[i].opcode != OpCodes.Ldc_R4)
                {
                    continue;
                }

                if ((float)codes[i].operand != 0.481248856f)
                {
                    continue;
                }
                
                codes[i] = new CodeInstruction(OpCodes.Call, 
                    typeof(CustomWindSwitchTime).GetProperty("Instance").GetGetMethod());
                
                codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, 
                    typeof(CustomWindSwitchTime).GetProperty("CurrentFrequency").GetGetMethod()));
                
                break;
            }
            return codes.AsEnumerable();
        }
    }

}