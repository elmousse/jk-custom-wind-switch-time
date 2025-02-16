using System;
using System.Collections.Generic;
using System.Diagnostics;
using HarmonyLib;
using JumpKing;
using JumpKing.Mods;

namespace CustomWindSwitch
{
    [JumpKingMod("McOuille.CustomWindSwitch")]
    public static class ModEntry
    {
        private const string TAG = "CustomWindSwitch";
        
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            var harmony = new Harmony("McOuille.CustomWindSwitch");
            Harmony.DEBUG = true;
            harmony.PatchAll();
        }
        
        [OnLevelStart]
        public static void OnLevelStart()
        {
            var tag = GetTag();
            
            if (String.IsNullOrEmpty(tag))
            {
                return;
            }
            
            var freqs = ToWindParamsDic(tag);
            
            if (freqs == null)
            {
                return;
            }
            
            if (freqs.Count == 0)
            {
                return;
            }
            
            CustomWindSwitchApi.Instance.Init(freqs);
        }
        
        [OnLevelUnload]
        public static void OnLevelUnload()
        {
            CustomWindSwitchApi.Instance.Empty();
        }
        
        private static string GetTag()
        {
            if (Game1.instance.contentManager?.level?.Info.Tags is null)
            {
                return null;
            }
            foreach (var item in Game1.instance.contentManager.level.Info.Tags)
            {
                if (!item.StartsWith(TAG + "=(") || !item.EndsWith(")"))
                {
                    continue;
                }
                return item.Substring(TAG.Length + 2, item.Length - TAG.Length - 3);
            }
            return null;
        }

        private static Dictionary<int, WindParams> ToWindParamsDic(string tag)
        {
            var windParamsDic = new Dictionary<int, WindParams>();
            var windInfos = System.Text.RegularExpressions.Regex.Split(tag, @"\)\s*,\s*\(");
            
            foreach (var windInfo in windInfos)
            {
                var windInfoSplit = windInfo.Split(',');
                if (windInfoSplit.Length != 5 && windInfoSplit.Length != 3)
                {
                    return null;
                }
                
                if (!int.TryParse(windInfoSplit[0], out var key))
                {
                    return null;
                }
                
                if (!float.TryParse(windInfoSplit[1], out var leftWindTime))
                {
                    return null;
                }
                
                if (!float.TryParse(windInfoSplit[2], out var rightWindTime))
                {
                    return null;
                }
                
                var leftWindIntensity = 1f;
                var rightWindIntensity = 1f;
                
                if (windInfoSplit.Length == 5)
                {
                    if (!float.TryParse(windInfoSplit[3], out leftWindIntensity))
                    {
                        return null;
                    }
                    
                    if (!float.TryParse(windInfoSplit[4], out rightWindIntensity))
                    {
                        return null;
                    }
                }
                
                windParamsDic[key-1] = new WindParams
                {
                    LeftWindTime = leftWindTime,
                    RightWindTime = rightWindTime,
                    LeftWindIntensity = leftWindIntensity,
                    RightWindIntensity = rightWindIntensity
                };
            }
            
            return windParamsDic;
        }
    }
}