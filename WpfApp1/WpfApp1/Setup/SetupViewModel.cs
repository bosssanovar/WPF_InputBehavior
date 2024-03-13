using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Setup
{
    /// <summary>
    /// SetuのViewModel
    /// </summary>
    public partial class SetupView : INotifyPropertyChanged
    {
        /// <inheritdoc/>
#pragma warning disable CS0067 // イベント 'SetupView.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'SetupView.PropertyChanged' は使用されていません

        private readonly SetupModel _model;

        private readonly CompositeDisposable _disposables = new();

        /// <summary>
        /// スピーカー数設定
        /// </summary>
        public ReactivePropertySlim<int> SpeakerCount { get; }

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public AsyncReactiveCommand SaveCommand { get; } = new AsyncReactiveCommand();
    }
}
