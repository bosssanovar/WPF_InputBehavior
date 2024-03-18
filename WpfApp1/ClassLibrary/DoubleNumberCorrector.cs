using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// 数値を補正する
    /// </summary>
    /// <param name="Maximum">最大値</param>
    /// <param name="Minimum">最小値</param>
    /// <param name="Step">ステップ</param>
    public record DoubleNumberCorrector(double Maximum, double Minimum, double Step)
    {
        /// <summary>
        /// 有効なステップかを判定します。
        /// </summary>
        /// <param name="value">検査対象値</param>
        /// <returns>有効なステップの場合 true</returns>
        public bool IsValidStep(double value)
        {
            return value % Step == 0;
        }

        /// <summary>
        /// 最大値以内か
        /// </summary>
        /// <param name="value">検査対象値</param>
        /// <returns>最大値以内の場合 true</returns>
        public bool IsInMaxmum(double value)
        {
            return value <= Maximum;
        }

        /// <summary>
        /// 最小値以内か
        /// </summary>
        /// <param name="value">検査対象値</param>
        /// <returns>最小値以内の場合 true</returns>
        public bool IsInMinimum(double value)
        {
            return value >= Minimum;
        }

        /// <summary>
        /// 有効値かを判定します。
        /// </summary>
        /// <param name="value">検査対象値</param>
        /// <returns>有効値の場合 true</returns>
        public bool IsValid(double value)
        {
            return IsValidStep(value)
                && IsInMaxmum(value)
                && IsInMinimum(value);
        }

        /// <summary>
        /// 補正します。
        /// </summary>
        /// <param name="value">補正対象値</param>
        /// <returns>補正値</returns>
        public double Correct(double value)
        {
            // 範囲外補正
            if(value > Maximum)
            {
                return Maximum;
            }
            else if(value < Minimum)
            {
                return Minimum;
            }

            // 丸め込み
            return ((int)(value / Step)) * Step;
        }
    }
}
