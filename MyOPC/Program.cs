using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Threading;
using System.Configuration;

namespace MyOPC
{
    /// <summary>
    /// 这个是自己写的，参考E:\codeWrok\OPCnew\Normal\如何在C#中实现OPC数据访问.pdf
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            OPCServer opcserver = new OPCServer();
            var serverName = ConfigurationManager.AppSettings["opcServer"];
            var count = Convert.ToInt16(ConfigurationManager.AppSettings["count"]);

            opcserver.Connect(serverName);  //ArchestrA.DASSIDirect.3
            OPCGroup opcgroup = opcserver.OPCGroups.Add("ModelConnection");//随便写

            List<OPCItem> listItem = new List<OPCItem>();

            for (int i = 0; i < count; i++)
            {
                var appsettingName = "item" + (i + 1).ToString();

                listItem.Add(opcgroup.OPCItems.AddItem(ConfigurationManager.AppSettings[appsettingName], i + 1));
            }


            object itemValue;
            object quality;
            object timeStamps;

            while (true)
            {

                listItem[0].Write("111");
                listItem[1].Write("rrrrrrrrrrrrrr");


                for (int i = 0; i < listItem.Count; i++)
                {
                    listItem[i].Read(1, out itemValue, out quality, out timeStamps);

                    Console.WriteLine(itemValue);
                    Console.WriteLine(quality);
                    Console.WriteLine(timeStamps);
                }

                Console.WriteLine("======");
                Thread.Sleep(1000);
            }

            //ModelConnection.Strand_Data_Read.$SYS$ErrorCount

        }
    }
}
