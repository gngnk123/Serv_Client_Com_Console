using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace RecInfoPortConsole
{

    internal class Program
    {
        TcpClient ClientSocket = new TcpClient();
        NetworkStream ServerStream = default(NetworkStream);
        String Readdata = null;

        static void Main(string[] args)
        {
            Program p = new Program();
            Program p2 = new Program();
            var Check = p.Conncect();
            p2.Conncect();      
            while (Check == false)
            {
                Console.WriteLine("Try Again,");                                                    //warumatebeli kavshiris shemtxvevashi procesi daiwyeba tavidan
                Check = p.Conncect();                                                               //manam sanam kavshiri ar shedgeba
            }


            while (true)
            {
                //consolshi migeba
                    p.ReadMassage();
                    p2.ReadMassage();
            }


        }
        public bool Conncect()
        {
            Console.WriteLine("'TCP' Type IP Address:");
            var textBox1 = Console.ReadLine();
            Console.WriteLine("Type Port Number:");
            var textBox2 = Console.ReadLine();
            bool Check;
            try
            {
                ClientSocket.Connect(textBox1, Int32.Parse(textBox2));                                  //tcp connection
                Check = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalide IP Address or Port Number " + e);
                Check = false;
            }
            return Check;
        }


        public void ReadMassage()
        {
            string ReturnData;


            Console.WriteLine("Waiting InputMassage: ");
            ServerStream = ClientSocket.GetStream();
            var Buffsize = ClientSocket.ReceiveBufferSize;
            byte[] InStream = new byte[Buffsize];

            ServerStream.Read(InStream, 0, Buffsize);

            ReturnData = System.Text.Encoding.ASCII.GetString(InStream);

            Console.WriteLine(ReturnData);

        }


    }
}


