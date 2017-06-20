# TwitterBot in C＃.
  
Twitter bot を C# で書きます。他の人も clone して (４行設定書き換えたら) 簡単に使えるように設計しています    

## 使い方

1. clone してください。
1. Visual Studio 2017 で CSharpTwitterBot.sln を開く
1. `CoreTweet` と `Newtonsoft.Json` を NuGet 経由でインストール (Nuget package の「復元」(restore)、もしくは Windows 版の VS ならリビルドすれば入ります。)
1. 呟かせたいアカウントのアカウントの、Consumer Key や Access Token などお決まり４点セットをメモしておく。（やり方: [Twitter APIの使い方まとめ](https://syncer.jp/twitter-api-matome) )
1. `/CSharpTwitterBot/AppConfig/keys_SAMPLE.json` を編集し、`keys.json` にリネームして保存。
1. ビルド＆実行 → つぶやける！
