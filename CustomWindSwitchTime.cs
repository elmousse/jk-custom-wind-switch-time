using System.Collections.Generic;
using JumpKing.Level;

namespace CustomWindSwitch
{
    public class CustomWindSwitchTime
    {
        private static CustomWindSwitchTime _instance;
        public static CustomWindSwitchTime Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomWindSwitchTime();
                }

                return _instance;
            }
        }
        
        private Dictionary<int, float> _freqs = new Dictionary<int, float>();
        private const float BASEFREQ = 0.481248856f;

        public void Init(Dictionary<int, float> freqsInfo)
        {
            _freqs = freqsInfo;
        }
        
        public void Empty()
        {
            _freqs.Clear();
        }

        public double CurrentFrequency {
            get
            {
                var currentScreen = LevelManager.CurrentScreen.GetIndex0();
                if (!_freqs.ContainsKey(currentScreen))
                {
                    return BASEFREQ;
                }
                return _freqs[currentScreen];
            }
        }
    }
}