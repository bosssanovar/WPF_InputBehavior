using Entity.XX;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// コレクション型のViewMmodelクラス
    /// </summary>
    public class DetailViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly DetailModel _model;

        private readonly CompositeDisposable _disposable = new();

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// スピーカーON/OFF設定のDetail
        /// </summary>
        public List<ReactivePropertySlim<bool>> SpeakerOnOff { get; set; } = new();

        /// <summary>
        /// スピーカーON/OFF設定が変更されたか
        /// </summary>
        public List<ReadOnlyReactivePropertySlim<bool>> IsSpeakerOnOffChanged { get; set; } = new();

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 変更通知イベント（ReactiveProperty採用時のメモリリーク対策）
        /// </summary>
#pragma warning disable CS0067 // イベント 'DetailViewModel.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'DetailViewModel.PropertyChanged' は使用されていません

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">コレクション型のModel</param>
        public DetailViewModel(DetailModel model)
        {
            _model = model;

            var count = _model.Entity.Value.SpeakerOnOffDetail.Count;
            for (int i = 0; i < count; i++)
            {
                int index = i;
                var sp = _model.Entity.ToReactivePropertySlimAsSynchronized(
                    x => x.Value,
                    x => x.SpeakerOnOffDetail[index].Content,
                    x =>
                    {
                        var corrected = SpeakerOnOffVO.CurrectValue(x);
                        _model.Entity.Value.SpeakerOnOffDetail[index] = new(corrected);

                        _model.ForceNotify();

                        return _model.Entity.Value;
                    },
                    ReactivePropertyMode.DistinctUntilChanged)
                    .AddTo(_disposable);

                SpeakerOnOff.Add(sp);

                var isChanged =
                    _model.Entity.CombineLatest(
                        _model.EntitySnapShot,
                        (entity, snapShot) => entity.SpeakerOnOffDetail[index] != snapShot.SpeakerOnOffDetail[index])
                        .ToReadOnlyReactivePropertySlim(mode: ReactivePropertyMode.DistinctUntilChanged)
                        .AddTo(_disposable);

                IsSpeakerOnOffChanged.Add(isChanged);
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// オブジェクトの後始末
        /// </summary>
        public void Dispose()
        {
            _disposable.Dispose();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
