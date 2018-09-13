using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test3
{
    delegate void MyDel(int count);
    class Program
    {
        static void Main(string[] args)
        {
            DelTest dt = new DelTest();
          

            dt.del += Show;
            //dt.del(1);
            //dt.Receive();
            dt.Random();

            Console.ReadKey();
        }

        static void Show(int count)
        {
            Console.WriteLine("receive:"+count);
        }
    }
    class DelTest
    {
        public MyDel del;
        public void Receive()
        {           
            del(val);
        }
        Random ra = new Random();
        int val = 0;
        public void Random()
        {
            for (int i = 0; i < 100; i++)
            {
                val = ra.Next(1, 10);
                if (val < 5)
                {
                    this.Receive();
                }
            }
           
        }

    }
    
}
