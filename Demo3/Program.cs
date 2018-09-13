using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo3
{
    class Program
    {
        static void Main(string[] args)
        {
            A a = new B();//先调用父类构造，在用子类构造
            a.Fun();//如果是override 了父类的方法，直接调用子类的方法，如果子类中是new Fun ,相当于子类中没有Fun方法，虽然名字同，不是一个，调用父类的Fun

            B b = new B();
            b.Fun();

            Console.ReadKey();
        }
    }
}

public abstract class A
{

    public A()
    {

        Console.WriteLine('A');

    }

    public virtual void Fun()
    {

        Console.WriteLine("A.Fun()");

    }

}

public class B : A
{

    public B() 
    {

        Console.WriteLine('B');

    }

    public new void Fun()
    {

        Console.WriteLine("B.Fun()");

    }

    //public override void Fun()
    //{

    //    Console.WriteLine("B.Fun()");

    //}

}
 
