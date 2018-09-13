using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace 异步线程_回调
{
    public delegate int DelAdd(object obj);
    class Program
    {
        static void Main(string[] args)
        {
            SubThread st = new SubThread();
            //1、用类的字段方法返回变量
            //Thread t = new Thread(new ParameterizedThreadStart(st.Add));
            MyStruct s = new MyStruct();

            s.a = 1;
            s.b = 2;

            //t.Start(s);
            //t.IsBackground = true;
            //Thread.Sleep(200);
            //Console.WriteLine(st.Result.ToString());

            //2、用委托异步调用，还是会阻塞主线程
            // 与 BeginInvoke 对应 有个 EndInvoke 方法,而且运行完毕返回 IAsyncResult 类型的返回值.
            //这样我们可以这样收集 线程函数的 返回值

            //MyDelegate dele = new MyDelegate (MyFunction);
            //IAsyncResult ref = dele.BeginInvoke(10,"abcd");
            //...
            //int result = dele.EndInvoke(ref); <----收集 返回值
            //int MyFunction(int count, string str); <----带参数和返回值的 线程函数

            //DelAdd del = new DelAdd(st.AddWithReturn);
            //IAsyncResult asc = del.BeginInvoke(s,null,null);
            //int result = del.EndInvoke(asc);
            //Console.WriteLine(result.ToString());
            //Console.WriteLine("main thread");

            //3、异步回调，不阻塞主线程
            DelAdd del = new DelAdd(st.AddWithReturn);
            IAsyncResult asc = del.BeginInvoke(s, new AsyncCallback(huidao), null);

            //int result = del.EndInvoke(asc);
            //Console.WriteLine(result.ToString());
            Console.WriteLine("main thread---");
            Console.WriteLine("main thread---");
            Console.WriteLine("main thread---");




            Console.ReadKey();

        }


        static void huidao(IAsyncResult asc)
        {
            Console.WriteLine("回调结果--- ");

            var ascResult = (AsyncResult)asc;
            Console.WriteLine(asc.IsCompleted.ToString());//状态
            DelAdd delAdd = (DelAdd)ascResult.AsyncDelegate;

           int result= delAdd.EndInvoke(asc);
           Console.WriteLine("结果 "+result);
        }


    }
    public struct MyStruct
    {
        public int a;
        public int b;
    }
    class SubThread
    {
        public int Result;
        public void Add(object obj)
        {
            MyStruct s = (MyStruct)obj;
            this.Result = s.a + s.b;
        }

        public int AddWithReturn(object obj)
        {
            MyStruct s = (MyStruct)obj;
            Thread.Sleep(2000);
            return s.a + s.b;
        }

    }


}
