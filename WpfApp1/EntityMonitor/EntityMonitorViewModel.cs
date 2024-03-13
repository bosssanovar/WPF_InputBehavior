using Entity;
using Entity.XX;
using Reactive.Bindings;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityMonitor
{
    public partial class EntityMonitorView : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // イベント 'EntityMonitorView.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'EntityMonitorView.PropertyChanged' は使用されていません

        readonly IXXRepository _repository;
        XXEntity? _oldEntity;

        public ReactiveCollection<Item> Items { get; } = [];

        public AsyncReactiveCommand ClearCommand { get; } = new AsyncReactiveCommand();

        private async Task DetectDifferenceAsync()
        {
            await Task.Run(() =>
            {
                _oldEntity ??= _repository.Load();

                var newEntity = _repository.Load();

                var oldProperties = GetEntityProperties(_oldEntity);
                var newProperties = GetEntityProperties(newEntity);

                foreach (var name in oldProperties.Keys)
                {
                    if (!newProperties.ContainsKey(name)) continue;

                    if (oldProperties[name] != newProperties[name])
                    {
                        Items.InsertOnScheduler(0, new Item(name, newProperties[name], oldProperties[name]));
                    }
                }

                _oldEntity = newEntity;
            });
        }

        private static Dictionary<string, string> GetEntityProperties(XXEntity entity)
        {
            var ret = new Dictionary<string, string>();

            // Entityの型を取得
            Type entityType = entity.GetType();

            // Entityのプロパティの一覧を取得
            PropertyInfo[] entityPropertyInfos = entityType.GetProperties();

            // Entityのプロパティ一覧を列挙
            foreach (PropertyInfo entityPropertyInfo in entityPropertyInfos)
            {
                // Entity直下のオブジェクトのプロパティ名を取得
                string entityPropertyName = entityPropertyInfo.Name;

                // Entity直下のオブジェクトを取得
                var entityPropertyValue = entityPropertyInfo.GetValue(entity);

                if (entityPropertyValue is ISettingInfos settingInfos)
                {
                    var infos = settingInfos.SettingInfos;

                    foreach (var info in infos)
                    {
                        if (ret.ContainsKey(info.Name))
                        {
                            throw new NotImplementedException($"ISettingInfosで提供されるName値が重複しています: {info.Name}");
                        }

                        ret[info.Name] = info.Value;
                    }
                }
            }

            return ret;
        }
    }
}
