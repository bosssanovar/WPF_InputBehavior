
namespace Entity.XX
{
    public record TextVO(string Content) : ValueObjectBase<string>(Content), IInputLimit<string>, ISettingInfos
    {
        private const int MaxLength = 10;

        public List<(string Name, string Value)> SettingInfos
        {
            get
            {
                var ret = new List<(string Name, string Value)> ();
                ret.Add(("Text", Content));

                return ret;
            }
        }

        public static string CurrectValue(string value)
        {
            if (!IsValid(value))
            {
                return value.Substring(0, MaxLength);
            }
            return value;
        }

        public static bool IsValid(string value)
        {
            return value.Length <= MaxLength;
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
