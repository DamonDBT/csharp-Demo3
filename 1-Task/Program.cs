using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 500; i++)
            //{
            //    ThreadPool.SetMaxThreads(20, 50);
            //    //线程池是异步吗？
            //    ThreadPool.QueueUserWorkItem(m =>
            //    {
            //        Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            //    }); 

            //    //Thread t = new Thread(Show);
            //    //t.IsBackground = true;
            //    //t.Start();

            //}
            //Console.WriteLine("end");


            Task t = new Task(() =>
                {
                    Console.WriteLine("task");
                });
            Console.WriteLine(t.Status);
            t.Start();

            Thread.Sleep(100);

            Console.WriteLine(t.Status);
            t.ContinueWith((task) =>
            {
                Console.WriteLine("continue task.");
                Console.WriteLine("state: "+task.Status + "  " + task.IsCanceled);
            });
            Console.ReadKey();




            Console.ReadKey();


        }
        static void Show()
        {
            Console.WriteLine("12");
        }
    }
}
