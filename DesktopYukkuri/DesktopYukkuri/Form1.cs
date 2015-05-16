using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopYukkuri
{
    public partial class Form1 : Form
    {
        private Point lastMousePosition;
        private bool mouseCapture;
        int h=0, w=0;
        int formPosX = 0;
        int formPosWidth = 0;
        
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //フォームを背景色で透過させる
            this.TransparencyKey = BackColor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = @"../../yukkuri.png"; //画像のパス
            show(path);

        }

        //画像を表示する
        private void show(string path)
        {
            //フォームの境界線をなくす
            this.FormBorderStyle = FormBorderStyle.None;

            // このフォームを常に手前に表示する
            this.TopMost = true;
            
            //フォームのサイズ変更
            size_change(@path);
            //背景画像を指定する
            this.BackgroundImage = Image.FromFile(@path);

            formPosX = this.Left;         // X位置
            formPosWidth = this.Width;         // X位置
            
        }

        //ウィンドウの大きさを画像の大きさに変更
        private void size_change(string path)
        {
            //元画像の縦横サイズを調べる
            System.Drawing.Bitmap bmpSrc = new System.Drawing.Bitmap(@path);
            int width = bmpSrc.Width;
            int height = bmpSrc.Height;
            //ウィンドウのサイズを変更
            this.Size = new Size(width, height);
        }
        


        //以下マウス

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            //マウスの位置を所得
            this.lastMousePosition = Control.MousePosition;
            this.mouseCapture = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouseCapture == false)
            {
                return;
            }

            // 最新のマウスの位置を取得
            Point mp = Control.MousePosition;

            // 差分を取得
            int offsetX = mp.X - this.lastMousePosition.X;
            int offsetY = mp.Y - this.lastMousePosition.Y;

            // コントロールを移動
            this.Location = new Point(this.Left + offsetX, this.Top + offsetY);
            this.lastMousePosition = mp;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //ディスプレイの高さ
            h = System.Windows.Forms.Screen.GetBounds(this).Height;
            //ディスプレイの幅
            w = System.Windows.Forms.Screen.GetBounds(this).Width;
            
            while (0 < formPosX)
            {
                formPosX = formPosX - 1;
                this.Left = formPosX;
            }
            while (w -formPosWidth*2> formPosX)
            {
                formPosX = formPosX + 1;
                this.Left = formPosX;
            }


            
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            this.mouseCapture = false;
        }

        private void Form1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (this.mouseCapture == true && this.Capture == false)
            {
                this.mouseCapture = false;
            }
        }

    }
}
