namespace Calculator
{
    public class Calculator
    {

        private double _result;
        public void SetResult(double result)
        {
            _result = result;
        }

        public double Add(int a, int b)
        {
            _result = a + b;
            return _result;
        }

        public double Add(int a, int b, int c)
        {
            _result = a + b + c;
            return _result;
        }

        public double Add(double a, double b)
        {
            _result = a + b;
            return _result;
        }

        public virtual double GetResult()
        {
            return _result;
        }
    }
}
