using System;
using System.IO;

namespace CSharpTwitterBot.TweetContents
{
    /// <summary>
    /// 用意された、呟き一覧のテキストファイル（TweetContent/TweetsList.txt）を、管理するクラス。
    /// </summary>
    class TweetsList
    {
        /// <summary>
        /// TweetContent/TweetsList.txt ファイルにある呟き一覧から、ランダムに１行返す
        /// </summary>
        public static string RandomTweet()
        {
            // TweetContent/TweetsList.txt ファイルにある呟き一覧
            var tweetList = File.ReadAllLines(path: "../../TweetContentData/TweetsList.txt");

            // 0以上、リストの要素数未満の乱数を生成.
            // new Random()だと時刻がシードになるけど、今回の用途を考えるとそれで十分なランダム性になる
            var i = new Random().Next(minValue: 0, maxValue: tweetList.Length); // ファイルが存在しない場合はぬるぽでアプリが落ちる仕様

            return tweetList[i];
        }
    }
}
