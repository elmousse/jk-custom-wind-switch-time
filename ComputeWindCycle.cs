namespace CustomWindSwitch
{
    public static class ComputeWindCycle
    {
        public static float Execute(float time)
        {
            if (!CustomWindSwitchApi.Instance.HasCurrentCustomWind)
            {
                return time * 0.48124886f;
            }
            
            var lt = CustomWindSwitchApi.Instance.CurrentLeftTime;
            var rt = CustomWindSwitchApi.Instance.CurrentRightTime;
            var lf = CustomWindSwitchApi.Instance.CurrentLeftFrequency;
            var rf = CustomWindSwitchApi.Instance.CurrentRightFrequency;
            
            return time % (lt + rt) <= lt
                ? lf * (time % (lt + rt))
                : rf * ((time - lt) % (lt + rt) + rt);
        }
    }
}