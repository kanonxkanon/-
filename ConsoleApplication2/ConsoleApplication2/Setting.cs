﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApplication2
{
    //トークンを保持するっすー
    [Serializable]
    public class Settings
    {
        #region Values
        
        [Serializable]
        public struct Settings_Data
        {
            public string Token;
            public string Secret;
            public string UserID;
        }

        static Settings_Data data;

        #endregion

        #region Properties

        /// <summary>
        /// Access Token
        /// </summary>
        public static string AccessToken
        {
            get { return data.Token; }
            set { data.Token = value; }
        }

        /// <summary>
        /// Access Token Secret
        /// </summary>
        public static string AccessSecret
        {
            get { return data.Secret; }
            set { data.Secret = value; }
        }

        public static string userID 
        {
            get { return data.UserID; }
            set { data.UserID = value; }
        }


        #endregion
        #region Functions

        /// <summary>
        /// 設定を保存
        /// </summary>
        public static void Save()
        {
            var serializer = new XmlSerializer(typeof(Settings_Data));
            var file = new FileStream("config.settings", FileMode.Create);
            var set = new Settings();

            serializer.Serialize(file, data);

            file.Flush();
            file.Close();
        }

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        public static void Load()
        {
            var serializer = new XmlSerializer(typeof(Settings_Data));
            var file = new FileStream("config.settings", FileMode.OpenOrCreate);
            
            try
            {
                data = (Settings_Data)serializer.Deserialize(file);
                file.Close();
            }
            catch { }

            file.Close();
        }

        #endregion
    }

}
