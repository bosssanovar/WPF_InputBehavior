using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public record SomeEnumVO(SomeEnum Content) : ValueObjectBase<SomeEnum>(Content), IInputLimit<SomeEnum>, ISettingInfos
    {
        public List<(string Name, string Value)> SettingInfos {
            get
            {
                var ret = new List<(string Name, string Value)> ();
                ret.Add(("SumEnum", Content.GetText()));

                return ret;
            }
        }

        public static SomeEnum CurrectValue(SomeEnum value)
        {
            return value;
        }

        public static bool IsValid(SomeEnum value)
        {
            return true;
        }

        protected override void Validate()
        {
            // None.
        }
    }
}
