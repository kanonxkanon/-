
/*
 * スペースとかをツイート文に入れる場合は、
 * 改行:\n
 * スペース:^を入れてもらう。
 * twitterの仕様によっては\つけないとだめかも。
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using CoreTweet;
using CoreTweet.Streaming;
using CoreTweet.Streaming.Reactive;


namespace TwitterPronpt
{
    public partial class Form1 : Form
    {

        static string accessSecret;
        static string accessToken;

        static Tokens tokens;
        OAuth.OAuthSession session;

        static string name;
        static string userId;
        static string pin;
        static string follow;
        static string follower;
        static string[] cmArray;

        bool isToken = false;
        string com = null;


        //streaming接続情報保持。
        IDisposable disposable;
        
        public Form1()
        {
            InitializeComponent();
            this.textBox2.ReadOnly = true;
            this.textBox2.TabStop = true;
        }


        private void Form1_Resize(object sender, System.EventArgs e)
        {
            this.textBox2.Width = this.Size.Width-15;
        }
        

        //イベント
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            //押されたキーがエンターキーかどうかの条件分岐
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                //コマンド実行
                CommandEnter(textBox1.Text);
                textBox1.Text = "";
                
                if (isToken)
                {
                }
                else 
                {
                }
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Write("Loding Auth...");
                //ロード
                Settings.Load();
                accessToken = Settings.AccessToken;
                accessSecret = Settings.AccessSecret;

                Write("Printing AuthInfo...");
                Write("AccessSecret :" + accessSecret);
                Write("");
                Write("AccessToken :" + accessToken);
                Write("__________________________________________________________");
                

                if (accessToken == null && accessSecret == null)
                {
                    isToken = false;
                    Write("token is not found...");
                    Write("please input pin...");
                    Write("__________________________________________________________");
                    throw new System.ArgumentException("Parameter", "ori");

                }
                
                Write("Loding-Ok.");
                //すでにある情報からtokenを生成。
                tokens = CoreTweet.Tokens.Create("WvIT7wje8o104o7UU1j82bZWE", "N0ewKX23Kx8ko4fCAxFrVsU5BPAM8eOWTvDx9AFYkNLtagx3vT",accessToken,accessSecret);
                Write("token :" + tokens);

                Write("please, your ID...");
                Write("あなたのIDを入力してください。例:id @takanasi_kanon");

            }
            catch (Exception)
            {
                Write("ブラウザ開きます");
                Write("戻ってきてPINコードの入力をおねがいします");
                Write("コマンドpinの後に数字を入力してください。");
                Write("例:pin 0123456");
                
                //認証開始
                session = OAuth.Authorize("WvIT7wje8o104o7UU1j82bZWE", "N0ewKX23Kx8ko4fCAxFrVsU5BPAM8eOWTvDx9AFYkNLtagx3vT");
                var url = session.AuthorizeUri; // -> user open in browser
                
                //ブラウザおーぷん
                System.Diagnostics.Process.Start(url.ToString());
                
            }
        }
        

        /// <summary>
        /// 実際にユーザー情報をもってくるやつ。
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="userName"></param>
        public async void GetUserDetail(Tokens tokens, String userName)
        {
            UserResponse detail = tokens.Users.Show(id => userName);
            name = detail.Name;
            userId = detail.ScreenName.ToString();
            follower = detail.FollowersCount.ToString();
            follow = detail.FriendsCount.ToString();

            await Task.Delay(30 * 100);
        }


        /// <summary>
        /// ユーザー情報を表示
        /// </summary>
        public void UserDetail(string na)
        {
             
            var home = tokens.Statuses.HomeTimeline();
            GetUserDetail(tokens, na);

            Write("==========================================================");
            Write("==========================================================");
            Write("==========================================================");
            Write("name is..     ||" + name);
            Write("ID is ..      ||" + userId);
            Write("friend are..  ||" + follow);
            Write("follower are..||" + follower);
            Write("==========================================================");

            Write("__________________________________________________________");

        }
    



        /// <summary>
        /// コマンド実行メソッド
        /// ここでコマンドの種類を判別する。
        /// </summary>
        public void CommandEnter(string c)
        {
            // カンマ区切りで分割して配列に格納する
            string[] cmArray = c.Split(' ');
            
                switch (cmArray[0])
                {

                    case "twt":
                    {
                        foreach (string s in cmArray)
                        {
                            //各種コマンド
                            if (s == "-a")
                            {
                            }
                            if (s == "-b")
                            {
                            }
                            if (s == "-c")
                            {
                            }
                        }
                
                        tokens.Statuses.Update(status => cmArray[1]);
                        Write("Tweet-ok");
                        break;
                    }

                    case "/stream":
                    {
                        UserStreamAsync(tokens);
                        break;
                    }

                    case "id":
                    {
                        GetUserDetail(tokens,cmArray[1]);
                        UserDetail(cmArray[1]);
                        break;
                    }

                    case "home":
                    {
                        foreach (string s in cmArray)
                        {
                            //各種コマンド
                            if (s == "-a")
                            {
                            }
                            if (s == "-b")
                            {
                            }
                            if (s == "-c")
                            {
                            }
                        }
                        HomeTimelineAsync(tokens);
                        break;
                    }
                    case "name":
                    {
                        Write("name is...//" + name);
                        break;

                    }
                    case "cls":
                    {
                        textBox2.Text = "";
                        break;
                    }

                    case "pin":
                    {            
                        //ここで数字チェックしたほうがいいかも？
                        pin =cmArray[1];
                        SaveKeys(session, pin);
                        break;
                    }


                    case "exit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                    default:
                        break;
                        
                }
                Write("_______________________________________");
                Write("_______________________________________");
        }
        
        //ただ書き込む
        public void Write(string s)
        {
            textBox2.AppendText(s+"\r\n");
        }

        //スタブ
        public string Read()
        {
            string s = null;
            return s;
        }

        
        private void SaveKeys(CoreTweet.OAuth.OAuthSession session, string pin)
        {
            //ここトークンの受け渡し怪しい。
            //get pin
            tokens = CoreTweet.OAuth.GetTokens(session, pin);
            Settings.AccessToken = tokens.AccessToken;
            Settings.AccessSecret = tokens.AccessTokenSecret;
            Write("データを保存します...");
            Settings.Save();
            Write("ok");

            accessToken = Settings.AccessToken;
            accessSecret = Settings.AccessSecret;

            Write("Printing AuthInfo...");
            Write("AccessSecret :" + accessSecret);
            Write("");
            Write("AccessToken :" + accessToken);
            Write("__________________________________________________________");
            Write("welcame to the twitter-");
        }


        /// <summary>
        /// ユーザーのストリームを受け取る。
        /// </summary>
        /// <param name="t"></param>
        public async void UserStreamAsync(Tokens t)
        {
            
            IConnectableObservable<StreamingMessage> connectable
            = t.Streaming.StartObservableStream(StreamingType.User).Publish();

            IDisposable d1 = connectable.OfType<StatusMessage>()
                .Select(m => m.Status)
                .Subscribe(status =>
                {
                    Write(status.User.Name);
                    Write(status.User.ScreenName);
                    Write(status.Text);
                    Write("_____________________________________________________");

                });
            disposable = connectable.Connect();
            //なぞ
            await Task.Delay(30 * 1000);
        }

        
        /// <summary>
        /// タイムラインを取得
        /// </summary>
        /// <param name="tokens"></param>
        public async void HomeTimelineAsync(Tokens tokens)
        {
            foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => 10))
            {
                Write(status.User.Name);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Write(status.User.ScreenName);
                Console.ResetColor();

                Write(status.Text);
                Write("");
                Write("*********************************************************");
                Write("");
            }
            await Task.Delay(30 * 1000);
        }


        //けし
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
