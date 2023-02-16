using System;

namespace Calculator
{
    public class AdvancedCalculator : Calculator
    {

        public double Power(int @base, int exponent)
        {
            var power = Math.Pow(@base, exponent);
            base.SetResult(power);
            return power;
        }

        public override double GetResult()
        {
            return base.GetResult() * Math.Pow(10, 6);
        }
    }
}
