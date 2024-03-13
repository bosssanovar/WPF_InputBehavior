using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// コンボボックスアイテムの値-表示文字列
    /// </summary>
    /// <typeparam name="T">コンボボックス値Enum</typeparam>
    /// <param name="Value">値</param>
    /// <param name="Display">表示文字列</param>
    public record ComboBoxItemDisplayValue<T>(T Value, string Display)
    {
    }
}
