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
    /// string型の拡張メソッド群
    /// </summary>
    public static class StringExtensions
    {
        #region 半角英数字のみを判定

        /// <summary>
        /// 半角英数字のみかを判定します。
        /// </summary>
        /// <param name="str">評価対象文字列</param>
        /// <returns>半角英数字のみの場合 true</returns>
        public static bool IsOnlyHalfWidthAlphanumericCharacters(this string str)
        {
            if (str == null)
            {
                return false;
            }

            return Regex.IsMatch(str, @"^[0-9a-zA-Z]+$");
        }

        #endregion

        #region 半角英数字のみに補正

        /// <summary>
        /// 半角英数字のみを抽出します。
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns>半角英数字のみの文字列</returns>
        public static string ExtractOnlyHalfWidthAlphanumericCharacters(this string str)
        {
            Regex re = new Regex(@"[^0-9a-zA-Z]");
            return re.Replace(str, string.Empty);
        }

        #endregion
    }
}
