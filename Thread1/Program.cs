using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace Thread1
{
    class Program
    {
        delegate string MyDelegate(string name);

        static void Main(string[] args)
        {
            ThreadMessage("Main Thread");

            //建立委托
            MyDelegate myDelegate = new MyDelegate(Hello);
            //异步调用委托，获取计算结果
            myDelegate.BeginInvoke("Leslie", new AsyncCallback(Completed), null);
            //在启动异步线程后，主线程可以继续工作而不需要等待
            for (int n = 0; n < 10; n++)
            {
                Console.WriteLine("  Main thread do work!");
                Thread.Sleep(500);
            }
            Console.WriteLine("");

            Console.ReadKey();





            Console.WriteLine("main thread start ");

            //1
            Console.WriteLine("normal thread start");
            Thread t = new Thread(Do);
            t.IsBackground = true;
            t.Start();
            t.Join();//保证子线程结束后，主线程才开始

            //2
            Console.WriteLine("threadpool start");
            ThreadPool.SetMaxThreads(20, 20);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoPool), "hello");
            

            Console.ReadKey();
        }

        static void DoPool(object obj)
        {
            Console.WriteLine("\r\n");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("now in threadPool..." + obj);
                Thread.Sleep(100);
            }

        }
        static void Do()
        {
            Console.WriteLine("\r\n");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("now in thread...");
                Thread.Sleep(100);
            }
            
        }

        static string Hello(string name)
        {
            ThreadMessage("Async Thread");
            Thread.Sleep(2000);             //模拟异步操作
            return "\nHello " + name;
        }

        static void Completed(IAsyncResult result)
        {
            ThreadMessage("Async Completed");

            //获取委托对象，调用EndInvoke方法获取运行结果
            AsyncResult _result = (AsyncResult)result;
            MyDelegate myDelegate = (MyDelegate)_result.AsyncDelegate;
            string data = myDelegate.EndInvoke(_result);
            Console.WriteLine(data);
        }

        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  ThreadId is:{1}",
                   data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }
    }
}
