namespace Entity.XX
{
    public class XXEntity : EntityBase<XXEntity>
    {
        private const string Text_InitValue = "Init Value";
        private const int Number_InitValue = 100;
        private const bool Bool_InitValue = true;
        private const SomeEnum SomeEnum_InitValue = XX.SomeEnum.Pig;
        private const int SpeakerCount_InitValue = 40;

        private BoolVO _bool = new(Bool_InitValue);
        private SpeakerCountVO _speakerCount = new(SpeakerCount_InitValue);

        public TextVO Text { get; set; } = new(Text_InitValue);

        public NumberVO Number { get; set; } = new(Number_InitValue);

        public BoolVO Bool
        {
            get => _bool;
            set
            {
                _bool = value;

                CurrectSomeEnumIfNeed();
            }
        }
        public SomeEnumVO SomeEnum { get; set; } = new SomeEnumVO(SomeEnum_InitValue);

        public SpeakerCountVO SpeakerCount
        {
            get => _speakerCount;
            set
            {
                _speakerCount = value;
                SpeakerOnOff.InitSpeakerCount(value.Content);
            }
        }

        public SpeakerOnOffEntity SpeakerOnOff { get; private set; } = new SpeakerOnOffEntity();

        public XXEntity()
        {
            SpeakerOnOff.InitSpeakerCount(SpeakerCount_InitValue);
        }

        public void Init()
        {
            Text = new TextVO(Text_InitValue);
            Number = new NumberVO(Number_InitValue);
            Bool = new BoolVO(Bool_InitValue);
            SomeEnum = new SomeEnumVO(SomeEnum_InitValue);
            SpeakerCount = new SpeakerCountVO(SpeakerCount_InitValue);

            SpeakerOnOff.InitSpeakerCount(SpeakerCount.Content);
        }

        private void CurrectSomeEnumIfNeed()
        {
            if (!Bool.Content && SomeEnum.Content == XX.SomeEnum.Cat)
            {
                SomeEnum = new SomeEnumVO(SomeEnum_InitValue);
            }
        }

        public override XXEntity Clone()
        {
            var ret = base.Clone();
            ret.SpeakerOnOff = SpeakerOnOff.Clone();

            return ret;
        }
    }
}
