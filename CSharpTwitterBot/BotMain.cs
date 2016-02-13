using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using Newtonsoft;

namespace CSharpTwitterBot
{
    class BotMain
    {
        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            var keys = MyTokens;

            var tokens = Tokens.Create(keys.ConsumerKey, keys.ConsumerSecret, keys.AccessToken, keys.AccessSecret);
            var message = $@"C# からの投稿テストです。
毎回 key.json ファイルからアクセストークンを読み込むのではなく、キャッシュするようにしました。
{DateTime.Now}";
            await tokens.Statuses.UpdateAsync(new { status = message });
        }

        // アクセストークンなど、投稿に必要なキーを取得。（キャッシュがあればキャッシュから。無かったらファイルから読み込む）
        private static Keys MyTokens => _myTokens ?? (_myTokens = ReadTokens());
        static Keys _myTokens = null;

        // key.json ファイルから、アクセストークンなどを読み込む。
        private static Keys ReadTokens()
        {
            var json = System.IO.File.ReadAllText("AppConfig/keys.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Keys>(json);
        }

        // 投稿に使う各種キーの集まり。（型定義）
        public class Keys
        {
            public string ConsumerKey { get; set; }
            public string ConsumerSecret { get; set; }
            public string AccessToken { get; set; }
            public string AccessSecret { get; set; }
        }
    }
}
