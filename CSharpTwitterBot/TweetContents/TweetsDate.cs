using System;

namespace CSharpTwitterBot.TweetContents
{
    /// <summary>
    /// 今年のうちどれくらいが経過したかなど、日付に関連したツイート文面を作るクラス。
    /// </summary>
    static class TweetsDate
    {
        // 日本指定
        static readonly TimeZoneInfo jstZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
        static readonly System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ja-JP");

        // 現在時刻（JST)
        static DateTimeOffset today => DateTimeOffset.UtcNow.ToOffset(jstZoneInfo.BaseUtcOffset);

        /// <summary>
        /// "今日 2016/02/20 は第7週目の土曜日です。今年の13.75%が経過しました。"
        /// </summary>
        public static string FormatPassedTime()
        {
            // 今年の何パーセントが経過したかの数字
            var passedTimePercent = GetPassedTime().ToPercentage();

            return $@"今日 {today.ToString("yyyy/MM/dd HH:mm:ss")} は第{today.DayOfYear / 7}週目の{today.ToString("ddd", culture)}曜日です。
今年の{passedTimePercent}%が経過しました。";
        }

        /// <summary>
        /// 小数を受け取って、パーセント(小数点以下2桁)に変換した double型を返す
        /// 0.523545 → 52.35
        /// </summary>
        /// <param name="quotient">小数</param>
        static double ToPercentage(this double quotient)
        {
            return Math.Round(quotient * 100, 3);
        }

        /// <summary>
        /// 今日は、今年のうちどれくらいの割合が経過しているか、を返す。（小数）
        /// </summary>
        /// <remarks>秒単位まで見る</remarks>
        private static double GetPassedTime()
        {
            var firstDayOfYear = new DateTimeOffset(today.Year, 1, 1, 0, 0, 0, jstZoneInfo.BaseUtcOffset); // JST
            var elapsedInYear = today - firstDayOfYear;

            const double secondsPerDay = 24 * 60 * 60;
            var daysInYear = 365;
            if (DateTime.IsLeapYear(today.Year)) daysInYear++; // うるう年
            var secondsPerYear = daysInYear * secondsPerDay;

            return elapsedInYear.TotalSeconds / secondsPerYear;
        }
    }
}
