using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Setup
{
    /// <summary>
    /// SetupView.xaml の相互作用ロジック
    /// </summary>
    public partial class SetupView : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">SetupのModel</param>
        public SetupView(SetupModel model)
        {
            _model = model;

            SpeakerCount = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.SpeakerCount.Content,
                x =>
                {
                    var currected = SpeakerCountVO.CurrectValue(x);
                    _model.Entity.Value.SpeakerCount = new(currected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                },
                ReactivePropertyMode.DistinctUntilChanged)
                .AddTo(_disposables);

            SaveCommand.Subscribe(async () =>
                {
                    await Task.Delay(500);
                    _model.Save();

                    Close();
                })
                .AddTo(_disposables);

            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                // エンターキー以外は無視
                return;
            }

            // フォーカスを移動する
            FrameworkElement element = (FrameworkElement)FocusManager.GetFocusedElement(this);
            element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _disposables.Dispose();
        }
    }
}
