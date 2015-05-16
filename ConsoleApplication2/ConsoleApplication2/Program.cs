using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet;
using CoreTweet.Streaming;
using CoreTweet.Streaming.Reactive;
using System.Reflection;


namespace ConsoleApplication2
{
    class Program
    {
        static string accessSecret;
        static string accessToken;
        
        static Tokens tokens;

        static string name;
        static string userId;
        static string pin;
        static string follow;
        static string follower;
        static string[] cmArray;


        static void Main(string[] args)
        {
            try
            {
                Console.Write("Loding Auth...");
                //ロード
                Settings.Load();
                accessToken = Settings.AccessToken;
                accessSecret = Settings.AccessSecret;
                userId = Settings.userID;
                Console.WriteLine("ok");


                Console.Write("Printing Auth...");
                Console.Write("AccessSecret:" + accessSecret);
                Console.WriteLine("");
                Console.Write("AccessToken:" + accessToken);
                Console.WriteLine("ok");


                if (accessToken == null && accessSecret == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null", "original");
                }
                
                
                Console.WriteLine("Loding-Ok.");
                //すでにある情報からtokenを生成。
                tokens = CoreTweet.Tokens.Create("WvIT7wje8o104o7UU1j82bZWE", "N0ewKX23Kx8ko4fCAxFrVsU5BPAM8eOWTvDx9AFYkNLtagx3vT",accessToken,accessSecret);
                Console.WriteLine("token//" + tokens);
                
                follow = tokens.Friends.ToString();
                follower = tokens.Followers.ToString();

            }
            catch (Exception)
            {
                Console.WriteLine("ブラウザ開きます");
                Console.WriteLine("戻ってきてPINコードの入力をおねがいします");

                //認証開始
                var session = CoreTweet.OAuth.Authorize("WvIT7wje8o104o7UU1j82bZWE", "N0ewKX23Kx8ko4fCAxFrVsU5BPAM8eOWTvDx9AFYkNLtagx3vT");
                var url = session.AuthorizeUri; // -> user open in browser

                //ブラウザおーぷん
                System.Diagnostics.Process.Start(url.ToString());

                pin = Console.ReadLine();

                //get pin
                tokens = CoreTweet.OAuth.GetTokens(session, pin);
                userId = tokens.ScreenName;
                
                Settings.AccessToken = tokens.AccessToken;
                Settings.AccessSecret = tokens.AccessTokenSecret;
                Settings.userID = tokens.ScreenName;
                
                Console.Write("データを保存します...");
                Settings.Save();

                Console.WriteLine("ok");

            }

            GetUserDetail(tokens, userId);
            HomeTimelineAsync(tokens); 
            Console.WriteLine("...ok");
            Console.WriteLine("=========================================================");
            Console.WriteLine("=========================================================");
            Console.WriteLine("=========================================================");
            Console.WriteLine("twiPro ver. 0.5                                          ");
            Console.WriteLine("welcome to the twitter                                   ");
            Console.WriteLine("ID is ..      ||" + userId);
            Console.WriteLine("friend are..  ||" + follow);
            Console.WriteLine("follower are..||" + follower);
            Console.WriteLine("=========================================================");
            Console.WriteLine("__________________________________________________________");
            

    

            
             
            Console.Write("twipro>");
            var com = Console.ReadLine();

            // カンマ区切りで分割して配列に格納する
            string[] cmArray = com.Split(' ');


            while (true) 
            {
                switch (cmArray[0])
                {


                    case "tw":
                        tokens.Statuses.Update(status => cmArray[1]);
                        Console.WriteLine("ok");
                        break;

                    case "/stream":
                        Console.WriteLine("ok");
                        UserStreamAsync(tokens);
                        break;

                    case "home":
                        Console.WriteLine("ok");
                        HomeTimelineAsync(tokens);
                        break;

                    case "name":
                        Console.WriteLine("ok");
                        Console.WriteLine("name is...//" +name);
                        break;

                    case "cls":
                        Console.Clear();
                        break;


                    case "exit":
                        Environment.Exit(0);
                        break;                        
                        
                    default:
                        Console.WriteLine(" this command is not application command..");
                        break;
                }
                Console.WriteLine("_______________________________________");
                Console.WriteLine("_______________________________________");

                Console.Write("twipro>");
                com = Console.ReadLine();

                // カンマ区切りで分割して配列に格納する
                cmArray = com.Split(' ');



            }
        }
        static void GetUserDetail(Tokens tokens, String userName)
        {
            UserResponse detail = tokens.Users.Show(id => userName);
            name = detail.Name;
            userId = detail.ScreenName.ToString();
            follower = detail.FollowersCount.ToString();
            follow = detail.FriendsCount.ToString();
        }



        public static async void UserStreamAsync(Tokens t)
        {

            IConnectableObservable<StreamingMessage> connectable
            = t.Streaming.StartObservableStream(StreamingType.User).Publish();

            IDisposable d1 = connectable.OfType<StatusMessage>()
                .Select(m => m.Status)
                .Subscribe(status =>
                {
                    Console.WriteLine();
                    Console.WriteLine(status.User.Name);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(status.User.ScreenName);
                    Console.ResetColor();

                    Console.WriteLine(status.Text);
                    Console.WriteLine("_____________________________________________________");

                });

            IDisposable disposable = connectable.Connect();
            
            await Task.Delay(30 * 1000);
        }

            
        static async void HomeTimelineAsync(Tokens tokens){
            foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => 10))
            {
                Console.WriteLine();
                Console.WriteLine(status.User.Name);
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(status.User.ScreenName);
                Console.ResetColor();
                
                Console.WriteLine(status.Text);
                Console.WriteLine("_____________________________________________________");

            }
            Console.Write("twipro>");
        }
    }
}
