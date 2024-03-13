using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public class SpeakerOnOffDetailEntity : EntityBase<SpeakerOnOffDetailEntity>
    {
        public List<SpeakerOnOffVO> SpeakerOnOffDetail { get; private set; } = [];

        public void Init(int count)
        {
            SpeakerOnOffDetail.Clear();
            for (int i = 0; i < count; i++)
            {
                SpeakerOnOffDetail.Add(new(i % 3 == 0));
            }
        }

        public override SpeakerOnOffDetailEntity Clone()
        {
            var ret = base.Clone();

            List<SpeakerOnOffVO> tmp = [];
            foreach (var detail in  SpeakerOnOffDetail)
            {
                tmp.Add(detail);
            }

            ret.SpeakerOnOffDetail = tmp;

            return ret;
        }
    }
}
