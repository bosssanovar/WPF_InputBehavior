using Entity.XX;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowのViewModel要素
    /// </summary>
    public partial class MainWindowView : INotifyPropertyChanged
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly MainWindowModel _model;

        private readonly CompositeDisposable _disposable = new();

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 自動保存するしない
        /// </summary>
        public bool IsAutoSave
        {
            get { return _model.IsAutoSave; }
            set { _model.IsAutoSave = value; }
        }

        /// <summary>
        /// Text設定
        /// </summary>
        public ReactivePropertySlim<string> Text { get; }

        /// <summary>
        /// Text設定が変更されたか
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsTextChanged { get; }

        /// <summary>
        /// Text設定
        /// </summary>
        public ReactivePropertySlim<string> Text2 { get; }

        /// <summary>
        /// Text設定が変更されたか
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsText2Changed { get; }

        /// <summary>
        /// Number設定
        /// </summary>
        public ReactivePropertySlim<double> Number { get; }

        /// <summary>
        /// Number設定が変更されたか
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsNumberChanged { get; }

        /// <summary>
        /// Bool設定
        /// </summary>
        public ReactivePropertySlim<bool> Bool { get; }

        /// <summary>
        /// Bool設定が変更されたか
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsBoolChanged { get; }

        /// <summary>
        /// SomeEnum設定
        /// </summary>
        public ReactivePropertySlim<SomeEnum> SomeEnum { get; }

        /// <summary>
        /// SomeEnum設定が変更されたか
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsSomeEnumChanged { get; }

        /// <summary>
        /// SpeakerOnOffコレクション設定
        /// </summary>
        public ReadOnlyReactiveCollection<DetailViewModel> SpeakerOnOffs { get; }

        /// <summary>
        /// 初期化コマンド
        /// </summary>
        public AsyncReactiveCommand InitCommand { get; } = new AsyncReactiveCommand();

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public AsyncReactiveCommand SaveCommand { get; } = new AsyncReactiveCommand();

        /// <summary>
        /// SomeEnum設定のコンボボックスアイテムリスト
        /// </summary>
        public ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>> ComboBoxItems { get; private set; } =
            new ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>>();

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

#pragma warning disable CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:Closing brace should be followed by blank line", Justification = "<保留中>")]
        private void InitComboBoxItems()
        {
            var comboBoxItemList = new List<ComboBoxItemDisplayValue<SomeEnum>>();
            comboBoxItemList.Add(new(Entity.XX.SomeEnum.Dog, Entity.XX.SomeEnum.Dog.GetText()));
            if (Bool.Value)
            {
                comboBoxItemList.Add(new(Entity.XX.SomeEnum.Cat, Entity.XX.SomeEnum.Cat.GetText()));
            }
            comboBoxItemList.Add(new(Entity.XX.SomeEnum.Elephant, Entity.XX.SomeEnum.Elephant.GetText()));
            comboBoxItemList.Add(new(Entity.XX.SomeEnum.Pig, Entity.XX.SomeEnum.Pig.GetText()));

            ComboBoxItems.Value = new(comboBoxItemList);
        }

        private void UpdateEntity()
        {
            _model.LoadEntity();
        }

        private void UpdateEntitySnapShot()
        {
            _model.LoadEntitySnapShot();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
