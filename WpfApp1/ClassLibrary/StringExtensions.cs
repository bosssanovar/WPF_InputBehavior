using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace ClassLibrary
{
    /// <summary>
    /// 利用可能な文字種別
    /// </summary>
    public enum AvailableCharactersType
    {
        /// <summary>
        /// 半角英数字
        /// </summary>
        HalfWidthAlphanumeric,

        /// <summary>
        /// JIS第1水準漢字まで
        /// </summary>
        UpToJisLevel1KanjiSet,
    }

    /// <summary>
    /// string型の拡張メソッド群
    /// </summary>
    public static class StringExtensions
    {
        #region 利用可能な文字のみかを判定

        /// <summary>
        /// 利用可能な文字かを判定します。
        /// </summary>
        /// <param name="c">評価対象文字</param>
        /// <param name="type">利用可能な文字種別</param>
        /// <returns>利用可能な文字の場合 true</returns>
        public static bool IsOnlyAbailableCharacters(this char c, AvailableCharactersType type)
        {
            return c.ToString().IsOnlyAbailableCharacters(type);
        }

        /// <summary>
        /// 利用可能な文字のみかを判定します。
        /// </summary>
        /// <param name="str">評価対象文字列</param>
        /// <param name="type">利用可能な文字種別</param>
        /// <returns>利用可能な文字のみの場合 true</returns>
        public static bool IsOnlyAbailableCharacters(this string str, AvailableCharactersType type)
        {
            bool ret;

            switch (type)
            {
                case AvailableCharactersType.HalfWidthAlphanumeric:
                    ret = Regex.IsMatch(str, @"^[0-9a-zA-Z]+$");
                    break;
                case AvailableCharactersType.UpToJisLevel1KanjiSet:
                    ret = IsUntilJISKanjiLevel2(str, true);
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

        #region 利用可能な文字のみに補正

        /// <summary>
        /// 利用可能な文字のみを抽出します。
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <param name="type">利用可能な文字種別</param>
        /// <returns>利用可能な文字のみの文字列</returns>
        public static string ExtractOnlyAbailableCharacters(this string str, AvailableCharactersType type)
        {
            string ret;

            switch (type)
            {
                case AvailableCharactersType.HalfWidthAlphanumeric:
                    ret =
                        string.Concat(
                            str.Where(
                                x => x.IsOnlyAbailableCharacters(AvailableCharactersType.HalfWidthAlphanumeric)));
                    break;
                case AvailableCharactersType.UpToJisLevel1KanjiSet:
                    ret =
                        string.Concat(
                            str.Where(
                                x => x.IsOnlyAbailableCharacters(AvailableCharactersType.UpToJisLevel1KanjiSet)));
                    break;
                default:
                    throw new NotImplementedException();
            }

            return ret;
        }

        #endregion
    }
}
