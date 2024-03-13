using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using Usecase;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowのModelクラス
    /// </summary>
    public class MainWindowModel
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        private readonly SaveLoadUsecase _saveLoadUsecase;

        private readonly InitUsecase _initUsecase;

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 自動保存するか
        /// </summary>
        public bool IsAutoSave { get; set; }

        /// <summary>
        /// エンティティ
        /// </summary>
        public ReactivePropertySlim<XXEntity> Entity { get; } = new ReactivePropertySlim<XXEntity>();

        /// <summary>
        /// コレクション型のModel
        /// </summary>
        public ObservableCollection<DetailModel> Details { get; } = new ObservableCollection<DetailModel>();

        /// <summary>
        /// エンティティのスナップショット
        /// </summary>
        public ReactivePropertySlim<XXEntity> EntitySnapShot { get; } = new();

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="saveLoadUsecase">保存・読み込みユースケース</param>
        /// <param name="initUsecase">初期化ユースケース</param>
        public MainWindowModel(SaveLoadUsecase saveLoadUsecase, InitUsecase initUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            _initUsecase = initUsecase;

            LoadEntity();
            Entity.Subscribe(x =>
            {
                if (IsAutoSave)
                {
                    _saveLoadUsecase.Save(x);
                }
            });

            LoadEntitySnapShot();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        /// <summary>
        /// Entityを読み込みます。
        /// </summary>
        public void LoadEntity()
        {
            Entity.Value = _saveLoadUsecase.Load();
            InitDetails();
        }

        /// <summary>
        /// Entityのスナップショットを読み込みます。
        /// </summary>
        public void LoadEntitySnapShot()
        {
            EntitySnapShot.Value = _saveLoadUsecase.LoadSnapShot();
            UpdateDetailsSnapShot();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        /// <summary>
        /// Entityを初期化します。
        /// </summary>
        internal void Init()
        {
            _initUsecase.Init();

            LoadEntity();
            LoadEntitySnapShot();
        }

        /// <summary>
        /// Entityを保存します。
        /// </summary>
        internal void Save()
        {
            _saveLoadUsecase.Save(Entity.Value);
        }

        /// <summary>
        /// Entityの変更通知発行
        /// </summary>
        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void InitDetails()
        {
            ClearDetails();
            foreach (var speakerOnOffEntity in Entity.Value.SpeakerOnOff.Details)
            {
                var detail = new DetailModel(speakerOnOffEntity);
                detail.ContentChanged += Detail_ContentChanged;
                Details.Add(detail);
            }
        }

        private void ClearDetails()
        {
            foreach (var detail in Details)
            {
                detail.ContentChanged -= Detail_ContentChanged;
            }

            Details.Clear();
        }

        private void UpdateDetailsSnapShot()
        {
            var detailCount = Details.Count;
            for(var i = 0; i < detailCount; i++)
            {
                Details[i].UpdateSnapShot(EntitySnapShot.Value.SpeakerOnOff.Details[i]);
            }
        }

        private void Detail_ContentChanged()
        {
            if (IsAutoSave)
            {
                _saveLoadUsecase.Save(Entity.Value);
            }
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
