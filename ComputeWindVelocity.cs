namespace CustomWindSwitch
{
    public static class ComputeWindVelocity
    {
        public static float Execute(float num2)
        {
            if (!CustomWindSwitchApi.Instance.HasCurrentCustomWind)
            {
                return 1.0f;
            }
            return num2 >= 0
                ? CustomWindSwitchApi.Instance.CurrentRightIntensity
                : CustomWindSwitchApi.Instance.CurrentLeftIntensity;
        }
    }
}