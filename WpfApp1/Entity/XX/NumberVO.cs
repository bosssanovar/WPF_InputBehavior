
namespace Entity.XX
{
    public record NumberVO(int Content) : ValueObjectBase<int>(Content), IInputLimit<int>, ISettingInfos
    {
        private const int MinValue = 0;
        private const int MaxValue = 500;

        public List<(string Name, string Value)> SettingInfos
        {
            get
            {
                var ret = new List<(string Name, string Value)> ();
                ret.Add(("Number", Content.ToString()));

                return ret;
            }
        }

        public static int CurrectValue(int value)
        {
            if (value < MinValue)
            {
                return MinValue;
            }
            else if (value > MaxValue)
            {
                return MaxValue;
            }
            return value;
        }

        public static bool IsValid(int value)
        {
            if (value < MinValue)
            {
                return false;
            }
            else if (value > MaxValue)
            {
                return false;
            }
            return true;
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
