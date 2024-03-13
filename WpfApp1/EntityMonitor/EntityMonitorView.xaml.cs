using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace EntityMonitor
{
    /// <summary>
    /// EntityMonitorView.xaml の相互作用ロジック
    /// </summary>
    public partial class EntityMonitorView : Window
    {
        public EntityMonitorView(IXXRepository repository)
        {
            _repository = repository;

            ClearCommand.Subscribe(async _ =>
            {
                await Task.Delay(100);

                Items.Clear();
            });

            InitializeComponent();
        }

        // タイマを設定する
        private async Task SetupTimer()
        {
            while (true)
            {
                var task = DetectDifferenceAsync();
                var task2 = Task.Delay(500);

                await Task.WhenAll(task, task2);
            }
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            await SetupTimer();
        }
    }
}
