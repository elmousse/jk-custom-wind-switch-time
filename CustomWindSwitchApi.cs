using System;
using System.Collections.Generic;
using JumpKing.Level;

namespace CustomWindSwitch
{
    public class CustomWindSwitchApi
    {
        private static CustomWindSwitchApi _instance;
        public static CustomWindSwitchApi Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomWindSwitchApi();
                }

                return _instance;
            }
        }
        
        private Dictionary<int, WindParams> _freqs = new Dictionary<int, WindParams>();

        public void Init(Dictionary<int,WindParams> freqsInfo)
        {
            _freqs = freqsInfo;
        }
        
        public void Empty()
        {
            _freqs.Clear();
        }
        
        private int CurrentScreenIndex => LevelManager.CurrentScreen.GetIndex0();

        public bool HasCurrentCustomWind => _freqs.ContainsKey(CurrentScreenIndex);

        public float CurrentLeftTime => _freqs[CurrentScreenIndex].LeftWindTime;
        
        public float CurrentRightTime => _freqs[CurrentScreenIndex].RightWindTime;
         
        public float CurrentLeftFrequency => (float)Math.PI / _freqs[CurrentScreenIndex].LeftWindTime;
        
        public float CurrentRightFrequency => (float)Math.PI / _freqs[CurrentScreenIndex].RightWindTime;
        
        public float CurrentLeftIntensity => _freqs[CurrentScreenIndex].LeftWindIntensity;
        
        public float CurrentRightIntensity => _freqs[CurrentScreenIndex].RightWindIntensity;
    }
}