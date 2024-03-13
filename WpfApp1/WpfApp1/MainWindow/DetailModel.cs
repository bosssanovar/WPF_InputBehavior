using Entity.XX;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// コレクション型のModelクラス
    /// </summary>
    public class DetailModel
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// エンティティ
        /// </summary>
        public ReactivePropertySlim<SpeakerOnOffDetailEntity> Entity { get; }

        /// <summary>
        /// エンティティのスナップショット
        /// </summary>
        public ReactivePropertySlim<SpeakerOnOffDetailEntity> EntitySnapShot { get; } = new();

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 設定値の変更通知
        /// </summary>
        public event Action? ContentChanged;

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entity">エンティティ</param>
        public DetailModel(SpeakerOnOffDetailEntity entity)
        {
            Entity = new ReactivePropertySlim<SpeakerOnOffDetailEntity>(entity);

            Entity.Subscribe(x =>
            {
                ContentChanged?.Invoke();
            });
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        /// <summary>
        /// Entityの変更通知を発行します。
        /// </summary>
        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }

        /// <summary>
        /// Entityのスナップショットを更新します。
        /// </summary>
        /// <param name="snapShot">Entityのスナップショット</param>
        internal void UpdateSnapShot(SpeakerOnOffDetailEntity snapShot)
        {
            EntitySnapShot.Value = snapShot;
        }

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
