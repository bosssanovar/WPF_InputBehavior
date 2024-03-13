using Entity.XX;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;

namespace WpfApp1.Setup
{
    /// <summary>
    /// SetupのModelクラス
    /// </summary>
    public class SetupModel
    {
        /// <summary>
        /// エンティティ
        /// </summary>
        internal ReactivePropertySlim<XXEntity> Entity { get; } = new ReactivePropertySlim<XXEntity>();

        private readonly SaveLoadUsecase _saveLoadUsecase;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="saveLoadUsecase">保存・取り込みユースケース</param>
        public SetupModel(SaveLoadUsecase saveLoadUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            LoadEntity();
        }

        private void LoadEntity()
        {
            Entity.Value = _saveLoadUsecase.Load();
        }

        /// <summary>
        /// Entityの変更を通知する。
        /// </summary>
        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }

        /// <summary>
        /// Entityを保存する。
        /// </summary>
        internal void Save()
        {
            _saveLoadUsecase.Save(Entity.Value);
            _saveLoadUsecase.SaveSnapShot(Entity.Value);
        }
    }
}
