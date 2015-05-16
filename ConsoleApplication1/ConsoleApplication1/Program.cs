using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
namespace ConsoleApplication1
{
    class Program
    {
        private static Tokens tokens;

        static void Main(string[] args)
        {
            string s = "123";

            Console.Write("hash:");
            s = "3iHXFnLE+oc+XsuhB5Lanw==";

            //MD5ハッシュ値を計算する文字列
            //文字列をbyte型配列に変換する
            byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

            //MD5CryptoServiceProviderオブジェクトを作成
            System.Security.Cryptography.MD5CryptoServiceProvider md5 =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            //または、次のようにもできる
            //System.Security.Cryptography.MD5 md5 =
            //    System.Security.Cryptography.MD5.Create();

            //ハッシュ値を計算する
            byte[] bs = md5.ComputeHash(data);

            //リソースを解放する
            md5.Clear();

            //byte型配列を16進数の文字列に変換
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                result.Append(b.ToString("x2"));
            }
            //ここの部分は次のようにもできる
            //string result = BitConverter.ToString(bs).ToLower().Replace("-","");

            //結果を表示
            Console.WriteLine(result);

            string re =result.ToString();
            Code cd = new Code();
            
            string sha1 = cd.sha1("3iHXFnLE+oc+XsuhB5Lanw==", re);
            string sha256 =cd.sha256("3iHXFnLE+oc+XsuhB5Lanw==",re ) ;

            Console.WriteLine(sha1);
            Console.WriteLine(sha256);

            write("MD5\n" + re, "sha-1\n" + sha1, "sha-256\n" + sha256);
            Console.ReadKey();
        }

        static public void write(string s1,string s2, string s3) {
            
            System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\test.txt", true,
                System.Text.Encoding.GetEncoding("Shift_Jis"));
            
            //３行書き込み
            sw.WriteLine(s1);
            sw.WriteLine(s2);
            sw.WriteLine(s3);

            //ストリームを閉じる
            sw.Close();
        }
    }
}
