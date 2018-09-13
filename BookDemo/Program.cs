using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Timers;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BookDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SerilizeClass sc = new SerilizeClass();
            sc.Name = "zhangsan";
            sc.Age = 12;
            sc.Score = 120;

            IFormatter formater = new BinaryFormatter();
            FileStream fss = new FileStream("1.bin", FileMode.OpenOrCreate);
            formater.Serialize(fss, sc);

            fss.Close();
            fss = new FileStream("1.bin", FileMode.OpenOrCreate);
            var v = formater.Deserialize(fss);
            var sc1 = v as SerilizeClass;
            Console.WriteLine(sc1.Age);



            //FileStream fs = new FileStream("data.txt", FileMode.Open,FileAccess.Write);

            //GZipStream gzs = new GZipStream(fs,CompressionMode.Compress);
            //StreamWriter sw = new StreamWriter(gzs);
            //sw.Write("dasd");
            //sw.Close();





            //FileStream fs = new FileStream("data.txt",FileMode.Open);
            //byte[] bts = new byte[fs.Length];
            //fs.Read(bts, 0, bts.Length);
            //string str = Encoding.UTF8.GetString(bts);
            //Console.WriteLine("result");
            //Console.WriteLine(str   );
            //fs.Close();

            //StreamReader sr = new StreamReader("data.txt", Encoding.UTF8);
            //string str1= sr.ReadToEnd();
            //Console.WriteLine(str1);
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

            //string strLine=sr.ReadLine();
            //var strFirst = strLine.Split(',');
            //List<string> columns = new List<string>();
            //foreach (var item in strFirst)
            //{
            //    columns.Add(item.ToString());
            //}
            //strLine = sr.ReadLine();
            //while (strLine!=null)
            //{
            //    var temp = strLine.Split(',');
            //    Dictionary<string, string> dictemp = new Dictionary<string, string>();
            //    for (int i = 0; i < temp.Length; i++)
            //    {
            //        dictemp.Add(columns[i], temp[i]);
            //    }
            //    data.Add(dictemp);
            //    strLine = sr.ReadLine();
            
            //}
            //foreach (var item in columns)
            //{
            //    Console.Write("{0,-20}",item.ToString());
            //}
            //Console.WriteLine("");
            //foreach (var item in data)
            //{
            //    foreach (var items in item)
            //    {
            //        Console.Write("{0,-20}", items.Value);
            //    }
            //    Console.WriteLine();
            //}



            int aa = 0;
            //int bb = ++aa;
            //if (!File.Exists(Environment.CurrentDirectory+"\\"+"1.txt"))
            //{
            //    File.Create("1.txt");
            
            //}

            //FileInfo fi = new FileInfo("1.txt");
            //Console.WriteLine(fi.FullName);
            //FileStream fs = new FileStream("1.txt", FileMode.Open);
            //byte[] bytes = Encoding.Default.GetBytes("hello world");
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();




            Timer timer = new Timer();
            timer.Interval = 1000; ;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Elapsed += delegate(object sender, ElapsedEventArgs e)
            {
                Console.WriteLine("进入匿名函数");
            };
            timer.Enabled = true;


            MyDrive md = new MyDrive();
            MyBase mb = md;//多态
            mb.Do();

            ArrayList ar = new ArrayList();
            ar.Add(12);
            ar.Add("dsad");
            Console.WriteLine(ar[1].GetType());
            Console.WriteLine(typeof(ArrayList));


            Console.WriteLine(Test(33));
            Console.WriteLine(Test(12121, 333));



            SubClass sbc = new SubClass();
            sbc.SayChinese();

            ClassA a = new ClassA();
            a.a = 12;
            ClassA b = a;
            b.a = 122;


            Console.WriteLine(a.a);
            Console.WriteLine(b.a);



            try
            {
                Console.WriteLine(MaxValue());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToShortDateString());
        }

        [Serializable]
        class SerilizeClass
        {
            public string Name { get; set; }
            public int Age { get; set; }

            [NonSerialized]
            public int Score;
        }

        static int MaxValue(params int[] arrary)
        {
            if (arrary.Length > 0)
            {
                return arrary[0];
            }
            else
            {
                throw new IndexOutOfRangeException("meiyouhszi");
            }
        }

        static int Test(int b, int a = 12)
        {
            return a;
        }
    }

    class ClassA
    {
        public int a;
    }


    public class MyBase
    {
        public virtual void Do()
        {
            Console.WriteLine("mybase do");

        }
    }

    public class MyDrive1 : MyBase
    {

    }
    public class MyDrive : MyBase
    {
        //public override void Do()
        //{
        //    base.Do();
        //    Console.WriteLine("mydrive do");
        //}
        public new void Do()//覆盖基类
        {
            Console.WriteLine("new do so");
        }
        public void Do1()
        {

        }

    }


    interface ISay
    {
        void SayChinese();

    }
    public class BaseClass : ISay
    {
        public BaseClass()
        {
        }
        public BaseClass(int a)
        {
            Console.WriteLine(a.ToString());
        }
        public BaseClass(int a, int b)
        {
            Console.WriteLine((a + b).ToString());
        }
        public void SayChinese()
        {
            Console.WriteLine("shuo zhong wen");
        }
    }
    internal class SubClass : BaseClass, ISay
    {
        public SubClass(int i, int j)
            : base(i)
        {

        }

        public SubClass()
            : this(3, 5)
        {

        }

    }
}
