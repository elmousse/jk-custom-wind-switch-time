using System;
using System.Collections.Generic;
using HarmonyLib;
using JumpKing;
using JumpKing.Mods;

namespace CustomWindSwitch
{
    [JumpKingMod("McOuille.CustomWindSwitch")]
    public static class ModEntry
    {
        [OnLevelStart]
        public static void OnLevelStart()
        {
            var harmony = new Harmony("McOuille.CustomWindSwitch");
            harmony.PatchAll();
            
            var tag = GetTag();
            
            if (String.IsNullOrEmpty(tag))
            {
                return;
            }
            
            var freqs = ToFreqs(tag);
            
            if (freqs?.Count == 0)
            {
                return;
            }
            
            CustomWindSwitchTime.Instance.Init(freqs);
        }
        
        private static string GetTag()
        {
            if (Game1.instance.contentManager?.level?.Info.Tags is null)
            {
                return null;
            }
            foreach (var item in Game1.instance.contentManager.level.Info.Tags)
            {
                if (!item.StartsWith("CustomWindSwitch="))
                {
                    continue;
                }
                var start = item.IndexOf('(');
                var end = item.LastIndexOf(')');
        
                if (start >= 0 && end > start)
                {
                    return item.Substring(start + 1, end - start - 1);
                }
            }
            return null;
        }
        
        private static Dictionary<int, float> ToFreqs(string tag)
        {
            var freqs = new Dictionary<int, float>();
            var list = tag.Split(';');
            foreach (var item in list)
            {
                var pair = item.Split(',');
                if (pair.Length != 2)
                {
                    return null;
                }
                if (!int.TryParse(pair[0], out var key))
                {
                    return null;
                }
                if (!float.TryParse(pair[1], out var value))
                {
                    return null;
                }
                freqs[key-1] = (float)((2 * Math.PI) / value);
            }
            return freqs;
        }
    }
}