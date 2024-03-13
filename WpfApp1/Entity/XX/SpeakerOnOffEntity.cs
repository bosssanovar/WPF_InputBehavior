using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.XX
{
    public class SpeakerOnOffEntity : EntityBase<SpeakerOnOffEntity>, ISettingInfos
    {

        public List<SpeakerOnOffDetailEntity> Details { get; private set; } = [];

        public List<(string Name, string Value)> SettingInfos
        {
            get
            {
                var ret = new List<(string Name, string Value)>();

                for (int rowIndex = 0; rowIndex < Details.Count; rowIndex++)
                {
                    SpeakerOnOffDetailEntity detail = Details[rowIndex];
                    for (int columnIndex = 0; columnIndex < detail.SpeakerOnOffDetail.Count; columnIndex++)
                    {
                        SpeakerOnOffVO vo = detail.SpeakerOnOffDetail[columnIndex];

                        ret.Add(($"SpeakerOnOff(row={rowIndex}, column={columnIndex})", vo.Content.ToString()));
                    }
                }

                return ret;
            }
        }

        public void InitSpeakerCount(int count)
        {
            Details.Clear();
            for (int i = 0; i < count; i++)
            {
                var speakerOnOffDetail = new SpeakerOnOffDetailEntity();
                speakerOnOffDetail.Init(count);
                Details.Add(speakerOnOffDetail);
            }
        }

        public override SpeakerOnOffEntity Clone()
        {
            var ret = base.Clone();

            List<SpeakerOnOffDetailEntity> tmp = [];
            foreach (var detail in Details)
            {
                tmp.Add(detail.Clone());
            }

            ret.Details = tmp;

            return ret;
        }
    }
}
