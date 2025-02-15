using System.Collections.Generic;
using EntityComponent;
using JumpKing.Level;
using JumpKing.Player;

namespace FasterWindSwitch
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
        
        private Dictionary<int, double> _freqs;
        private double _lastFreq;
        private const double BASEFREQ = 0.4812488555908203;

        public void Init(Dictionary<int, double> freqsInfo)
        {
            _freqs = freqsInfo;
            _lastFreq = BASEFREQ;
        }

        public double CurrentFrequency {
            get
            {
                var currentScreen = LevelManager.CurrentScreen.GetIndex0();
                if (!_freqs.ContainsKey(currentScreen))
                {
                    return BASEFREQ;
                }
                _lastFreq = _freqs[currentScreen];
                return _freqs[currentScreen];
            }
        }
    }
}