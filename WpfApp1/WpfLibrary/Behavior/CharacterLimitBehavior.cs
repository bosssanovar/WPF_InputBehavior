using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using ClassLibrary;
using System.Text.RegularExpressions;

namespace WpfLibrary.Behavior
{
    public class CharacterLimitBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// 入力文字を制限します。
        /// </summary>
        public static readonly DependencyProperty CharactersTypeProperty =
                    DependencyProperty.RegisterAttached(
                        "CharactersType",
                        typeof(AvailableCharactersType),
                        typeof(CharacterLimitBehavior),
                        new UIPropertyMetadata(AvailableCharactersType.None, CharactersTypeChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static AvailableCharactersType GetCharactersType(DependencyObject obj)
        {
            return (AvailableCharactersType)obj.GetValue(CharactersTypeProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetCharactersType(DependencyObject obj, AvailableCharactersType value)
        {
            obj.SetValue(CharactersTypeProperty, value);
        }

        private static void CharactersTypeChanged
            (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // イベントを登録・削除 
            textBox.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
            textBox.PreviewTextInput -= AssociatedObject_PreviewTextInput;
            textBox.PreviewLostKeyboardFocus -= TextBox_PreviewLostKeyboardFocus;
            DataObject.RemovePastingHandler(textBox, PastingHandler);
            var newValue = (AvailableCharactersType)e.NewValue;
            if (newValue is not AvailableCharactersType.None)
            {
                textBox.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
                textBox.PreviewTextInput += AssociatedObject_PreviewTextInput;
                textBox.PreviewLostKeyboardFocus += TextBox_PreviewLostKeyboardFocus;
                DataObject.AddPastingHandler(textBox, PastingHandler);
            }

            // IMEモードを無効化
            if (newValue is AvailableCharactersType.HalfWidthAlphanumeric
                || newValue is AvailableCharactersType.Number
                || newValue is AvailableCharactersType.NumberAndMinus
                || newValue is AvailableCharactersType.Decimal
                || newValue is AvailableCharactersType.DecimalAndMinus)
            {
                InputMethod.SetIsInputMethodEnabled(textBox, false);
            }
        }

        private static void TextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (GetCharactersType(textBox) is AvailableCharactersType.Number
                || GetCharactersType(textBox) is AvailableCharactersType.NumberAndMinus
                || GetCharactersType(textBox) is AvailableCharactersType.Decimal
                || GetCharactersType(textBox) is AvailableCharactersType.DecimalAndMinus)
            {
                textBox.Text = "0";
            }
        }

        private static void AssociatedObject_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // 利用可能文字以外の入力を拒否
            if (!e.Text.IsFormatValid(GetCharactersType(textBox)))
            {
                e.Handled = true;
            }

            // 形式不正となる文字の入力を拒否
            var insertedText = InsertTextAtCaretPosition(textBox, e.Text);
            if (!insertedText.IsFormatValid(GetCharactersType(textBox)))
            {
                e.Handled = true;
            }
        }

        private static void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // PreviewTextInputでは半角スペースを検知できないので、PreviewKeyDownで検知
            if (e.Key == Key.Space && !' '.IsFormatValid(GetCharactersType(textBox)))
            {
                e.Handled = true;
            }
        }

        private static void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // 貼り付け時に文字列を補正

            // ペーストする文字列から有効文字列を抽出
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true);
            if (!isText) return;
            string text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string ?? string.Empty;
            string correctedText = text.ExtractOnlyAbailableCharacters(GetCharactersType(textBox));

            //キャレット位置に文字列挿入
            string wk = InsertTextAtCaretPosition(textBox, correctedText);
            e.CancelCommand();// 自前でペースト処理を実現しているため、標準動作はキャンセル
            textBox.Text = wk;

            //キャレット設定
            int cursorPosition = textBox.SelectionStart;
            textBox.SelectionStart = cursorPosition + correctedText.Length;
        }

        private static string InsertTextAtCaretPosition(TextBox textBox, string correctedText)
        {
            int pos = textBox.SelectionStart;
            string text = textBox.Text;

            //範囲選択されてれば削除
            var len = textBox.SelectedText.Length;
            if (len != 0)
            {
                text = text.Remove(pos, len);
            }

            text = text.Insert(pos, correctedText);

            return text;
        }
    }
}
