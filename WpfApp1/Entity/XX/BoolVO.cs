using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public record BoolVO(bool Content) : ValueObjectBase<bool>(Content), IInputLimit<bool>, ISettingInfos
    {
        public List<(string Name, string Value)> SettingInfos
        {
            get
            {
                var ret = new List<(string Name, string Value)> ();
                ret.Add(("Bool", Content.ToString()));
                return ret;
            }
        }
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
            // None.
        }
    }
}
