
using ClassLibrary;

namespace Entity.XX
{
    public record NumberVO(double Content) : ValueObjectBase<double>(Content), IInputLimit<double>, ISettingInfos
    {
        private const double MinValue = -100.0;
        private const double MaxValue = 95.5;
        private const double Step = 0.5;

        public List<(string Name, string Value)> SettingInfos
        {
            get
            {
                var ret = new List<(string Name, string Value)> ();
                ret.Add(("Number", Content.ToString()));

                return ret;
            }
        }

        public static double CurrectValue(double value)
        {
            var corrector = new DoubleNumberCorrector(MaxValue, MinValue, Step);
            return corrector.Correct(value);
        }

        public static bool IsValid(double value)
        {
            var corrector = new DoubleNumberCorrector(MaxValue, MinValue, Step);
            return corrector.IsValid(value);
        }

        protected override void Validate()
        {
            if (!IsValid(Content))
            {
                throw new ArgumentException("Invalid Value", nameof(Content));
            }
        }
    }
}
