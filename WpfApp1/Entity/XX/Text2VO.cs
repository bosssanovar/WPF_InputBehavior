using ClassLibrary;

namespace Entity.XX
{
    public record Text2VO(string Content) : ValueObjectBase<string>(Content), IInputLimit<string>, ISettingInfos
    {
        private const int MaxLength = 10;
        private const AvailableCharactersType Type = AvailableCharactersType.UpToJisLevel1KanjiSet;

        public List<(string Name, string Value)> SettingInfos
        {
            get
            {
                var ret = new List<(string Name, string Value)> ();
                ret.Add(("Text2", Content));

                return ret;
            }
        }

        public static string CurrectValue(string value)
        {
            string ret = value;

            if (!IsValid(value))
            {
                if (!value.IsOnlyAbailableCharacters(Type))
                {
                    ret = ret.ExtractOnlyAbailableCharacters(Type);
                }

                if (!IsLengthWithinWpecified(ret))
                {
                    ret = ret.Substring(0, MaxLength);
                }
            }
            return ret;
        }

        public static bool IsValid(string value)
        {
            if (!IsLengthWithinWpecified(value))
            {
                return false;
            }

            if (!value.IsOnlyAbailableCharacters(Type))
            {
                return false;
            }

            return true;
        }

        private static bool IsLengthWithinWpecified(string value)
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
