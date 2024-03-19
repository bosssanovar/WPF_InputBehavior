using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary
{
    /// <summary>
    /// 形式種別
    /// </summary>
    public enum TextFormatType
    {
        /// <summary>
        /// 制限なし
        /// </summary>
        None,

        /// <summary>
        /// 半角英数字
        /// </summary>
        HalfWidthAlphanumeric,

        /// <summary>
        /// JIS第1水準漢字まで
        /// </summary>
        UpToJisLevel1KanjiSet,

        /// <summary>
        /// 数字(0～9))
        /// </summary>
        Number,

        /// <summary>
        /// 数値とマイナス(0～9, -)
        /// </summary>
        NumberAndMinus,

        /// <summary>
        /// 小数(0～9, 小数点)
        /// </summary>
        Decimal,

        /// <summary>
        /// 小数とマイナス(0～9, 小数点, -)
        /// </summary>
        DecimalAndMinus,
    }

    /// <summary>
    /// string型の拡張メソッド群
    /// </summary>
    public static class StringExtensions
    {
        #region 利用可能な形式かを判定

        /// <summary>
        /// 有効な形式となっているかを判定します。
        /// </summary>
        /// <param name="c">評価対象文字</param>
        /// <param name="type">形式種別</param>
        /// <returns>利用可能な文字の場合 true</returns>
        public static bool IsFormatValid(this char c, TextFormatType type)
        {
            return c.ToString().IsFormatValid(type);
        }

        /// <summary>
        /// 有効な形式となっているかを判定します。
        /// </summary>
        /// <param name="str">評価対象文字列</param>
        /// <param name="type">形式種別</param>
        /// <returns>利用可能な文字のみの場合 true</returns>
        public static bool IsFormatValid(this string str, TextFormatType type)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            bool ret;

            switch (type)
            {
                case TextFormatType.HalfWidthAlphanumeric:
                    ret = Regex.IsMatch(str, @"^[0-9a-zA-Z]+$");
                    break;
                case TextFormatType.UpToJisLevel1KanjiSet:
                    ret = IsUntilJISKanjiLevel2(str, true);
                    break;
                case TextFormatType.Number:
                    ret = Regex.IsMatch(str, @"^[0-9]+$");
                    break;
                case TextFormatType.NumberAndMinus:
                    {
                        var sb = new StringBuilder();
                        sb.Append("^[-]?[0-9]+$").Append('|'); // ex. "-0.0", "0.0"
                        sb.Append("^[-]$"); // '-'
                        ret = Regex.IsMatch(str, sb.ToString());
                    }

                    break;
                case TextFormatType.Decimal:
                    {
                        var sb = new StringBuilder();
                        sb.Append("^[0-9]+[.][0-9]+$").Append('|'); // ex. "99.99"
                        sb.Append("^[0-9]+[.]$").Append('|'); // ex. "99."
                        sb.Append("^[.][0-9]+$").Append('|'); // ex. ".99"
                        sb.Append("^[0-9]+$").Append('|'); // ex. "99"
                        sb.Append("^[.]$"); // '.'
                        ret = Regex.IsMatch(str, sb.ToString());
                    }

                    break;
                case TextFormatType.DecimalAndMinus:
                    {
                        var sb = new StringBuilder();
                        sb.Append("^[-]?[0-9]+[.][0-9]+$").Append('|'); // ex. "-0.0", "0.0"
                        sb.Append("^[-]?[0-9]+[.]?$").Append('|'); // ex. "-0.", "-0", "0"
                        sb.Append("^[.][0-9]*$").Append('|'); // ex. ".0", "."
                        sb.Append("^[-]$"); // '-'
                        ret = Regex.IsMatch(str, sb.ToString());
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }

            return ret;
        }

        /// <summary>
        /// 文字列が半角英数字記号かどうかを判定します
        /// </summary>
        /// <param name="target">対象の文字列</param>
        /// <returns>文字列が半角英数字記号の場合はtrue、それ以外はfalse</returns>
        public static bool IsASCII(string target)
        {
            return new Regex("^[\x20-\x7E]+$").IsMatch(target);
        }

        /// <summary>
        /// 文字列が半角カタカナ（句読点～半濁点）かどうかを判定します
        /// </summary>
        /// <param name="target">対象の文字列</param>
        /// <returns>文字列が半角カタカナ（句読点～半濁点）の場合はtrue、それ以外はfalse</returns>
        public static bool IsHalfKatakanaPunctuation(string target)
        {
            return new Regex("^[\uFF61-\uFF9F]+$").IsMatch(target);
        }

        #region 第1水準漢字用処理

        /// <summary>
        /// 文字列がJIS X 0208 漢字第二水準までで構成されているかを判定します
        /// </summary>
        /// <param name="target">対象の文字列</param>
        /// <param name="containsHalfKatakana">漢字第二水準までに半角カタカナを含む場合はtrue、それ以外はfalse</param>
        /// <returns>文字列がJIS X 0208 漢字第二水準までで構成されている場合はtrue、それ以外はfalse</returns>
        public static bool IsUntilJISKanjiLevel2(string target, bool containsHalfKatakana)
        {
            // 文字エンコーディングに「iso-2022-jp」を指定
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("iso-2022-jp");
            // 文字列長を取得
            int length = target.Length;
            for (int i = 0; i < length; i++)
            {
                // 対象の部分文字列を取得
                string targetSubString = target.Substring(i, 1);
                // 半角英数字記号の場合
                if (IsASCII(targetSubString) == true)
                {
                    continue;
                }

                // 漢字第二水準までに半角カタカナを含まずかつ対象の部分文字列が半角カタカナの場合
                if (containsHalfKatakana == false &&
                    IsHalfKatakanaPunctuation(targetSubString) == true)
                {
                    return false;
                }

                // 対象部分文字列の文字コードバイト配列を取得
                byte[] targetBytes = encoding.GetBytes(targetSubString);
                // 要素数が「1」の場合は漢字第三水準以降の漢字が「?」に変換された
                if (targetBytes.Length == 1)
                {
                    return false;
                }

                // 文字コードバイト配列がJIS X 0208 漢字第二水準外の場合
                if (IsUntilJISKanjiLevel2(targetBytes) == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 文字列がJIS X 0208 漢字第二水準までで構成されているかを判定します
        /// </summary>
        /// <param name="target">対象の文字列</param>
        /// <returns>文字列がJIS X 0208 漢字第二水準までで構成されている場合はtrue、それ以外はfalse</returns>
        /// <remarks>句読点～半濁点の半角カタカナはJIS X 0208 漢字第二水準外と判定します</remarks>
        public static bool IsUntilJISKanjiLevel2(string target)
        {
            return IsUntilJISKanjiLevel2(target, false);
        }

        /// <summary>
        /// 文字コードバイト配列がJIS X 0208 漢字第二水準までであるかを判定します
        /// </summary>
        /// <param name="targetBytes">文字コードバイト配列</param>
        /// <returns>文字コードバイト配列がJIS X 0208 漢字第二水準までである場合はtrue、それ以外はfalse</returns>
        private static bool IsUntilJISKanjiLevel2(byte[] targetBytes)
        {
            // 文字コードバイト配列の要素数が8ではない場合
            if (targetBytes.Length != 8)
            {
                return false;
            }

            // 区を取得
            int row = targetBytes[3] - 0x20;
            // 点を取得
            int cell = targetBytes[4] - 0x20;
            switch (row)
            {
                case 1: // 1区の場合
                    if (cell is >= 1 and <= 94)
                    {
                        // 1点～94点の場合
                        return true;
                    }

                    break;
                case 2: // 2区の場合
                    if (cell is >= 1 and <= 14)
                    {
                        // 1点～14点の場合
                        return true;
                    }
                    else if (cell is >= 26 and <= 33)
                    {
                        // 26点～33点の場合
                        return true;
                    }
                    else if (cell is >= 42 and <= 48)
                    {
                        // 42点～48点の場合
                        return true;
                    }
                    else if (cell is >= 60 and <= 74)
                    {
                        // 60点～74点の場合
                        return true;
                    }
                    else if (cell is >= 82 and <= 89)
                    {
                        // 82点～89点の場合
                        return true;
                    }
                    else if (cell == 94)
                    {
                        // 94点の場合
                        return true;
                    }

                    break;
                case 3: // 3区の場合
                    if (cell is >= 16 and <= 25)
                    {
                        // 16点～25点の場合
                        return true;
                    }
                    else if (cell is >= 33 and <= 58)
                    {
                        // 33点～58点の場合
                        return true;
                    }
                    else if (cell is >= 65 and <= 90)
                    {
                        // 65点～90点の場合
                        return true;
                    }

                    break;
                case 4: // 4区の場合
                    if (cell is >= 1 and <= 83)
                    {
                        // 1点～83点の場合
                        return true;
                    }

                    break;
                case 5: // 5区の場合
                    if (cell is >= 1 and <= 86)
                    {
                        // 1点～86点の場合
                        return true;
                    }

                    break;
                case 6: // 6区の場合
                    if (cell is >= 1 and <= 24)
                    {
                        // 1点～24点の場合
                        return true;
                    }
                    else if (cell is >= 33 and <= 56)
                    {
                        // 33点～56点の場合
                        return true;
                    }

                    break;
                case 7: // 7区の場合
                    if (cell is >= 1 and <= 33)
                    {
                        // 1点～33点の場合
                        return true;
                    }
                    else if (cell is >= 49 and <= 81)
                    {
                        // 49点～81点の場合
                        return true;
                    }

                    break;
                case 8: // 8区の場合
                    if (cell is >= 1 and <= 32)
                    {
                        // 1点～32点の場合
                        return true;
                    }

                    break;
                default:
                    if (row is >= 16 and <= 46)
                    {
                        // 16区～46区の場合
                        if (cell is >= 1 and <= 94)
                        {
                            // 1点～94点の場合
                            return true;
                        }
                    }
                    else if (row == 47)
                    {
                        // 47区の場合
                        if (cell is >= 1 and <= 51)
                        {
                            // 1点～51点の場合
                            return true;
                        }
                    }

                    break;
            }

            return false;
        }

        #endregion

        #endregion

        #region 利用可能な形式に補正

        /// <summary>
        /// 利用可能な文字のみを抽出します。
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <param name="type">利用可能な文字種別</param>
        /// <returns>利用可能な文字のみの文字列</returns>
        public static string ExtractOnlyAbailableCharacters(this string str, TextFormatType type)
        {
            if (str.IsFormatValid(type))
            {
                // 不正な文字列でなければ、そのまま完了
                return str;
            }

            string ret;

            // まず、不正文字の削除。それだけでOKとなれば、それで完了
            ret = string.Concat(str.Where(x => x.IsFormatValid(type)));
            if (ret.IsFormatValid(type))
            {
                // 不正な文字列でなければ、そのまま完了
                return ret;
            }

            switch (type)
            {
                case TextFormatType.HalfWidthAlphanumeric:
                case TextFormatType.UpToJisLevel1KanjiSet:
                case TextFormatType.Number:
                    //不正文字削除だけで完了している。
                    break;
                case TextFormatType.NumberAndMinus:
                    ret = CorrectNumberAndMinus(ret);
                    break;
                case TextFormatType.Decimal:
                    ret = CorrectDecimal(ret);
                    break;
                case TextFormatType.DecimalAndMinus:
                    bool hasMainus = ret.Contains('-');
                    bool hasDecimalPoint = ret.Contains(".");

                    if (hasMainus && !hasDecimalPoint)
                    {
                        // 「数値とマイナス」と同様のケースのため、当該処理をするだけで抽出完了。
                        return CorrectNumberAndMinus(ret);
                    }
                    else if (!hasMainus && hasDecimalPoint)
                    {
                        // 「小数」と同様のケースのため、当該処理をするだけで抽出完了。
                        return CorrectDecimal(ret);
                    }
                    else if (!hasMainus && !hasDecimalPoint)
                    {
                        // マイナスも小数点もない数値だけのケース。
                        // その場合はここまで処理が来ないため、NoCare.
                    }

                    // マイナスと小数点が混在する場合
                    ret = CorrectDecimalAndMinus(ret);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return ret;
        }

        private static string CorrectNumberAndMinus(string str)
        {
            string ret = str;

            var index = ret.IndexOf('-');
            if (index >= 1)
            {
                // 先頭がマイナス文字でなく数字の中盤にマイナス文字がある場合、
                // マイナス文字の前までを抽出
                return ret.Substring(0, index);
            }
            else
            {
                // マイナス文字が先頭と、それ以外1つ以上ある場合
                // 2つ目のマイナス文字前までを抽出して完了
                var secondDotIndex =
                    ret.Select((character, index) => (character, index))
                    .Where(item => item.character == '-')
                    .Skip(1)
                    .First()
                    .index;
                return ret.Substring(0, secondDotIndex);
            }
        }

        private static string CorrectDecimal(string str)
        {
            // 2つ目の小数点前までを抽出
            var secondDotIndex =
                str.Select((character, index) => (character, index))
                .Where(item => item.character == '.')
                .Skip(1)
                .First()
                .index;
            return str.Substring(0, secondDotIndex);
        }

        private static string CorrectDecimalAndMinus(string str)
        {
            if (str.Substring(0, 2) == "-.")
            {
                return "-";
            }

            // 先頭以外にマイナスがある場合は、マイナス前までを抽出
            if (Regex.IsMatch(str, $"^[0-9.]+[-]"))
            {
                var minusIndex = str.IndexOf("-");
                return str.Substring(0, minusIndex);
            }

            // 先頭マイナスで、全体でマイナスもしくは小数点が2個以上ある場合
            if (Regex.IsMatch(str, $"^[-][0-9.]*[-]")
                || Regex.IsMatch(str, $"^[-][0-9]*[.][0-9]*[.]"))
            {
                return str.Substring(0, GetEndIndex(str));
            }

            return str;
        }

        private static int GetEndIndex(string str)
        {
            /* 2つ目のマイナス前、もしくは２つ目の小数点前の短い方までを抽出 */

            int ret = 0;
            int minusCount = 0;
            int dotCount = 0;

            for (int index = 0; index < str.Length; index++)
            {
                if (str[index] is '.')
                {
                    dotCount++;
                }
                else if (str[index] is '-')
                {
                    minusCount++;
                }

                if (minusCount > 1 || dotCount > 1)
                {
                    ret = index;
                    break;
                }
            }

            return ret;
        }

        #endregion

        #region 文字列のバイト数を取得する(Shift-JIS)

        /// <summary>
        /// ShiftJisエンコード時のバイト数を取得します。
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns>ShiftJisエンコード時のバイト数</returns>
        public static int GetShiftJisByteCount(this string str)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

            return sjisEnc.GetByteCount(str);
        }

        #endregion

        #region 文字列を指定バイト数でトリミングする(Shift-JIS)

        /// <summary>
        /// Shift-JISエンコード時の指定バイト数の文字列を切り出します。
        /// </summary>
        /// <param name="source">文字列ソース</param>
        /// <param name="byteCount">バイト数</param>
        /// <returns>Shift-JISエンコード時の指定バイト数で切り出されたの文字列</returns>
        public static string SubstringSJisByteCount(this string source, int byteCount)
        {
            //指定したバイト数が文字バイト数以上であれば文字列をそのまま返す
            if (source.GetShiftJisByteCount() <= byteCount)
            {
                return source;
            }

            //Shift-JISのエンコーディングを取得する
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding enc = Encoding.GetEncoding("Shift_JIS");

            //文字列のバイト配列を取得する
            byte[] b = enc.GetBytes(source);

            //①指定されたバイト数で文字を切り出す
            string result = enc.GetString(b, 0, byteCount);

            //②指定されたバイト数+1バイトで文字を切り出す
            string result2 = enc.GetString(b, 0, byteCount + 1);

            //①と②の文字数を比較する
            if (result.Length == result2.Length)
            {
                //同じなら①から最後の１文字を削除した文字列を返す
                return result.Remove(result.Length - 1);
            }
            else
            {
                //異なれば①をそのまま返す
                return result;
            }
        }

        #endregion
    }
}
