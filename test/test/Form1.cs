using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        // intを引数にとってintを返すメソッドを参照するデリゲート型
        public delegate int IntToInt(int value);

        // stringを引数にとってintを返すメソッドを参照するデリゲート型
        public delegate int StringToInt(string value);

        // 引数をとらずintを返すメソッドを参照するデリゲート型
        public delegate int ReturnInt();

        // 引数にintをとりオブジェクトを返さないメソッドを参照するデリゲート型
        public delegate void ActionInt(int value);

        // 引数に二つのintをとってintを返すメソッドを参照するデリゲート型
        public delegate int IntIntToInt(int value0, int value1);

        IntToInt addOne = null;
        IntToInt doublerIntToInt = null;
        IntToInt tripplerIntToInt = null;        



        public Form1()
        {
            InitializeComponent();

            // AddOneメソッドを参照するIntToInt型の
            addOne = Calculator.AddOne;//。
            // addOne = new IntToInt(Calculator.AddOne);



            //このMultiplierクラスのインスタンスのCalcメソッドを参照するIntToIntデリゲート型のインスタンスを作るには、次のように書きます。
            Multiplier doubler = new Multiplier(2); // Calcメソッドは引数で渡した数の2倍の数を返す
            doublerIntToInt = doubler.Calc; // doublerのCalcを参照するデリゲートを生成

            Multiplier trippler = new Multiplier(3); // Calcメソッドは引数で渡した数の3倍の数を返す
            tripplerIntToInt = trippler.Calc; // tripplerのCalcを参照するデリゲートを生成



        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }


    //引数に1+して答えを返すPG(クラスメソッド
    public class Calculator
    {
        public static int AddOne(int value)
        {
            return value + 1;
        }
    }

    //インスタンスメソッド
    public class Multiplier
    {
        readonly int number;

        public Multiplier(int number)
        {
            this.number = number;
        }

        public int Calc(int v)
        {
            return number * v;
        }
        

        public static int CountList<T> (List<T> list,    Func<T, bool> predicate)
        {
            int count = 0;
            foreach (T element in list)
            {
                // predicateが参照するメソッドを、elementを引数に渡し呼び出している
                if (predicate(element))
                {
                    // 真ならカウンタをインクリメント
                    count++;
                }
            }
            return count;
        }
    }


}
