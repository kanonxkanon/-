using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Code {

        public string sha1(string s, string key){

            //文字列をバイト型配列に変換する
            byte[] data = System.Text.Encoding.UTF8.GetBytes(s);
            byte[] keyData = System.Text.Encoding.UTF8.GetBytes(key);

            //HMACSHA1オブジェクトの作成
            System.Security.Cryptography.HMACSHA1 hmac =
            new System.Security.Cryptography.HMACSHA1(keyData);
            //ハッシュ値を計算
            byte[] bs = hmac.ComputeHash(data);
            //リソースを解放する
            hmac.Clear();

            //byte型配列を16進数に変換
            string result = BitConverter.ToString(bs).ToLower().Replace("-", "");
            return "sha-1///"+result;
        }

        public string sha256(string s, string key)
        {

            //文字列をバイト型配列に変換する
            byte[] data = System.Text.Encoding.UTF8.GetBytes(s);
            byte[] keyData = System.Text.Encoding.UTF8.GetBytes(key);

            //HMACSHA1オブジェクトの作成
            System.Security.Cryptography.HMACSHA256 hmac =
            new System.Security.Cryptography.HMACSHA256(keyData);
            //ハッシュ値を計算
            byte[] bs = hmac.ComputeHash(data);
            //リソースを解放する
            hmac.Clear();

            //byte型配列を16進数に変換
            string result = BitConverter.ToString(bs).ToLower().Replace("-", "");
            return "sha-256///" + result;
        }
        

    }
}
