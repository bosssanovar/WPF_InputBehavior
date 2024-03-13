using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public record SpeakerOnOffVO(bool Content) : ValueObjectBase<bool>(Content), IInputLimit<bool>
    {
        public static bool CurrectValue(bool value)
        {
            return value;
        }

        public static bool IsValid(bool value)
        {
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
