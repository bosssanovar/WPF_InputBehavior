using ClassLibrary;

namespace Entity.XX
{
    public record Text2VO(string Content) : ValueObjectBase<string>(Content), IInputLimit<string>, ISettingInfos
    {
        private const int MaxByteCount = 8;
        private const TextFormatType Type = TextFormatType.UpToJisLevel1KanjiSet;

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
                if (!value.IsFormatValid(Type))
                {
                    ret = ret.ExtractOnlyAbailableCharacters(Type);
                }

                if (!IsLengthWithinWpecified(ret))
                {
                    ret = ret.SubstringSJisByteCount(MaxByteCount);
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

            if (!value.IsFormatValid(Type))
            {
                return false;
            }

            return true;
        }

        private static bool IsLengthWithinWpecified(string value)
        {
            return value.GetShiftJisByteCount() <= MaxByteCount;
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
