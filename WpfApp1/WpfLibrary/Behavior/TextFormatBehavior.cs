using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using ClassLibrary;
using System.Text.RegularExpressions;

namespace WpfLibrary.Behavior
{
    public class TextFormatBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// 入力文字を制限します。
        /// </summary>
        public static readonly DependencyProperty TextFormatProperty =
                    DependencyProperty.RegisterAttached(
                        "TextFormat",
                        typeof(TextFormatType),
                        typeof(TextFormatBehavior),
                        new UIPropertyMetadata(TextFormatType.None, TextFormatChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static TextFormatType GetTextFormat(DependencyObject obj)
        {
            return (TextFormatType)obj.GetValue(TextFormatProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetTextFormat(DependencyObject obj, TextFormatType value)
        {
            obj.SetValue(TextFormatProperty, value);
        }

        private static void TextFormatChanged
            (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // イベントを登録・削除 
            textBox.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
            textBox.PreviewTextInput -= AssociatedObject_PreviewTextInput;
            textBox.PreviewLostKeyboardFocus -= TextBox_PreviewLostKeyboardFocus;
            DataObject.RemovePastingHandler(textBox, PastingHandler);
            var newValue = (TextFormatType)e.NewValue;
            if (newValue is not TextFormatType.None)
            {
                textBox.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
                textBox.PreviewTextInput += AssociatedObject_PreviewTextInput;
                textBox.PreviewLostKeyboardFocus += TextBox_PreviewLostKeyboardFocus;
                DataObject.AddPastingHandler(textBox, PastingHandler);
            }

            // IMEモードを無効化
            if (newValue is TextFormatType.HalfWidthAlphanumeric
                || newValue is TextFormatType.Number
                || newValue is TextFormatType.NumberAndMinus
                || newValue is TextFormatType.Decimal
                || newValue is TextFormatType.DecimalAndMinus)
            {
                InputMethod.SetIsInputMethodEnabled(textBox, false);
            }
            else
            {
                InputMethod.SetIsInputMethodEnabled(textBox, true);
            }
        }

        private static void TextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (!string.IsNullOrEmpty(textBox.Text))
            {
                return;
            }

            // ブランク時には０を挿入
            if (GetTextFormat(textBox) is TextFormatType.Number
                || GetTextFormat(textBox) is TextFormatType.NumberAndMinus
                || GetTextFormat(textBox) is TextFormatType.Decimal
                || GetTextFormat(textBox) is TextFormatType.DecimalAndMinus)
            {
                textBox.Text = "0";
            }
        }

        private static void AssociatedObject_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // 利用可能文字以外の入力を拒否
            if (!e.Text.IsFormatValid(GetTextFormat(textBox)))
            {
                e.Handled = true;
            }

            // 形式不正となる文字の入力を拒否
            var insertedText = InsertTextAtCaretPosition(textBox, e.Text);
            if (!insertedText.IsFormatValid(GetTextFormat(textBox)))
            {
                e.Handled = true;
            }
        }

        private static void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // PreviewTextInputでは半角スペースを検知できないので、PreviewKeyDownで検知
            if (e.Key == Key.Space && !' '.IsFormatValid(GetTextFormat(textBox)))
            {
                e.Handled = true;
            }
        }

        private static void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();// 自前でペースト処理を実現しているため、標準動作はキャンセル

            var textBox = sender as TextBox;
            if (textBox == null) return;

            // 貼り付け時に文字列を補正

            // ペーストする文字列から有効文字列を抽出
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true);
            if (!isText) return;
            string text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string ?? string.Empty;
            string correctedText = text.ExtractOnlyAbailableCharacters(GetTextFormat(textBox));
            if(string.IsNullOrEmpty(correctedText)) return;

            //キャレット位置に文字列挿入
            string wk = InsertTextAtCaretPosition(textBox, correctedText);
            textBox.Text = wk;

            //キャレット設定
            int cursorPosition = textBox.SelectionStart;
            textBox.SelectionStart = cursorPosition + correctedText.Length;
        }

        private static string InsertTextAtCaretPosition(TextBox textBox, string str)
        {
            int pos = textBox.SelectionStart;
            string text = textBox.Text;

            //範囲選択されてれば削除
            var len = textBox.SelectedText.Length;
            if (len != 0)
            {
                text = text.Remove(pos, len);
            }

            //キャレット位置に文字列を挿入
            text = text.Insert(pos, str);

            return text;
        }
    }
}
