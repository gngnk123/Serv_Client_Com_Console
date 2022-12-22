using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            //var Check2 = p2.Conncect();
            while (Check == false)
            {
                Console.WriteLine("Try Again:");
                Check = p.Conncect();
            }
          

            while (true)
                {
                    Console.WriteLine("for InputMassage Press 'i', for OutputMassage Press 'o'");
                    var Command = Console.ReadLine();
                    if (Command == "i" || Command == "I")
                    {
                    p.ReadMassage();
                    //Thread thr = new Thread(new ThreadStart(p.ReadMassage));
                    //thr.Start();
                }
                    else if (Command == "o" || Command == "O")
                    {
                    while (Check == false)
                    {
                        Console.WriteLine("Try Again:");
                        Check = p.Conncect();
                    }
                    //p.OutputMassage();
                    Console.WriteLine("First massage: ");
                    var Massage1 = Console.ReadLine();
                    Console.WriteLine("Secon massage: ");
                    var Massage2 = Console.ReadLine();
                    //Thread thr = new Thread(new ThreadStart(p.OutputMassage));
                    //Thread thr2 = new Thread(new ThreadStart(p.OutputMassage hi));
                    Thread myNewThread = new Thread(() => p.OutputMassage(Massage1));
                    myNewThread.Start();
                    Thread myNewThread2 = new Thread(() => p.OutputMassage(Massage2));
                    myNewThread2.Start();
                    //thr.Start(Massage1);
                    //thr2.Start();
                    }
                else
                    {
                        Console.WriteLine("for InputMassage Press 'i', for OutputMassage Press 'o'");
                    }
                }
           

        }
        public bool Conncect()
        {
            Console.WriteLine("Type IP Address:");
            var textBox1 = Console.ReadLine();
            Console.WriteLine("Type Port Number:");
            var textBox2 = Console.ReadLine();
            bool Check;
            try
            {
                ClientSocket.Connect(textBox1, Int32.Parse(textBox2));
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

        public void OutputMassage(string massage)
        {
            //Console.WriteLine("Type Output Massage:");
            //var textBox3 = Console.ReadLine();
            var textBox3 =massage;
            try
            {
                byte[] outstream = Encoding.ASCII.GetBytes(textBox3);
                ServerStream.Write(outstream, 0, outstream.Length);
                ServerStream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong " + e);
            }

        }
        }

    }
