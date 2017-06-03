using CoreTweet;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CSharpTwitterBot
{
    /// <summary>
    /// tweet bot の、メインのクラス。実際につぶやく処理が書かれている。
    /// </summary>
    class BotMain
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        /// <summary>
        /// メイン処理
        /// </summary>
        private static async Task MainAsync()
        {
            var keys = MyTokens;
            var tokens = Tokens.Create(keys.ConsumerKey, keys.ConsumerSecret, keys.AccessToken, keys.AccessSecret);

            var message = TweetText();

            //Console.WriteLine(message);
            await tokens.Statuses.UpdateAsync(new { status = message });
        }

        /// <summary>
        /// 投稿するツイートの文章を組み立てる。
        /// </summary>
        static string TweetText()
        {
            var tweetLine = TweetContents.TweetsList.RandomTweet();
            var passedTime = TweetContents.TweetsDate.FormatPassedTime();

            return $@"C# からの投稿。
{tweetLine}
{passedTime}
"; 
        }

        #region OAuth 認証

        /// <summary>
        /// アクセストークンなど、投稿に必要なキーを取得。（キャッシュがあればキャッシュから。無かったらファイルから読み込む）
        /// </summary>
        private static Keys MyTokens => _myTokens ?? (_myTokens = ReadTokens());
        static Keys _myTokens = null;

        /// <summary>
        /// key.json ファイルから、アクセストークンなどを読み込む。
        /// </summary>
        private static Keys ReadTokens()
        {
            var json = File.ReadAllText("../../AppConfig/keys.json");
            return JsonConvert.DeserializeObject<Keys>(json);
        }

        /// <summary>
        /// 投稿に使う各種キーの集まり。（型定義）
        /// </summary>
        public class Keys
        {
            public string ConsumerKey { get; set; }
            public string ConsumerSecret { get; set; }
            public string AccessToken { get; set; }
            public string AccessSecret { get; set; }
        }

        #endregion
    }
}
