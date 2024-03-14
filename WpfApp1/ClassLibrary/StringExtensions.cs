using System;
using System.Collections.Generic;
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
    }

    /// <summary>
    /// string型の拡張メソッド群
    /// </summary>
    public static class StringExtensions
    {
        #region 利用可能な文字のみかを判定

        /// <summary>
        /// 利用可能な文字のみかを判定します。
        /// </summary>
        /// <param name="str">評価対象文字列</param>
        /// <param name="type">利用可能な文字種別</param>
        /// <returns>利用可能な文字のみの場合 true</returns>
        public static bool IsOnlyAbailableCharacters(this string str, AvailableCharactersType type)
        {
            bool ret = false;

            switch (type)
            {
                case AvailableCharactersType.HalfWidthAlphanumeric:
                    ret = Regex.IsMatch(str, @"^[0-9a-zA-Z]+$");
                    break;
                default:
                    break;
            }

            return ret;
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
            string ret = string.Empty;

            switch (type)
            {
                case AvailableCharactersType.HalfWidthAlphanumeric:
                    ret = Regex.Replace(str, @"[^0-9a-zA-Z]", string.Empty);
                    break;
                default:
                    break;
            }

            return ret;
        }

        #endregion
    }
}
