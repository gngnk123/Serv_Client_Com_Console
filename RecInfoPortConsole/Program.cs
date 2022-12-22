using System;
using System.Collections;
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
            var Check = p.Conncect();                                                               //kavshiri IP da Portis nomrit
            //var Check2 = p2.Conncect();       
            while (Check == false)                                                                  
            {
                Console.WriteLine("Try Again,");                                                    //warumatebeli kavshiris shemtxvevashi procesi daiwyeba tavidan
                Check = p.Conncect();                                                               //manam sanam kavshiri ar shedgeba
            }
          

            while (true)
                {
                                                                    //consolshi migeba
                    Console.WriteLine("for InputMassage Press 'i', for OutputMassage Press 'o'");
                    var Command = Console.ReadLine();
                    if (Command == "i" || Command == "I")
                    {
                    p.ReadMassage();
                    //Thread thr = new Thread(new ThreadStart(p.ReadMassage));
                    //thr.Start();
                    }
                                                                    //consolidan gagzavna
                    else if (Command == "o" || Command == "O")
                    {

                    //p.OutputMassage();
                    Console.WriteLine("How many massages do you want to send? : ");
                    var MassageCount = Console.ReadLine();                                              //mesijebis raodenoba
                    List<string> primeNumbers = new List<string>();                                     //listi mesijebis shesanaxad
                    for (int i = 1; i <= Convert.ToInt32(MassageCount); i++)
                    {
                        Console.WriteLine("Massage N" + i + ": ");
                        var Massage = Console.ReadLine();
                        primeNumbers.Add(Massage);
                    }

                    foreach (var myObj in primeNumbers)
                    {
                        var localCopy = myObj;
                        Thread myThread = new Thread(() => p.OutputMassage(localCopy));                 //mesijebis gashveba thread-ebit
                        myThread.Start();                                                               
                    }

                    }
                    //else if is dasasruli
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
