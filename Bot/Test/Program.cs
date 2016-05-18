using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp;
using System.Threading;

namespace DiscordBot
{
    class ChaaBot
    {
        public static bool isbot = true;
        // Forgive me mom
        public static int ChannelMessageCount;
        static void Main(string[] args)
        {
            // Set up client for connection
            DiscordClient client = new DiscordClient("APP ID", isbot);
            client.ClientPrivateInformation.Email = "email";
            client.ClientPrivateInformation.Password = "password";
            ChannelMessageCount = 0;
            
            // Handler for Connection
            client.Connected += (sender, e) =>
            {
                Console.WriteLine($"Connected! User: {e.User.Username}");
                client.UpdateCurrentGame("Riding the waves");
                Console.ReadLine();
            };

            // Handler for private message reciept
            client.PrivateMessageReceived += (sender, e) => 
            {
                Console.WriteLine("Recieved Private message");
                // Determine if the message is chaaaaaaaaaaaaa
                var resp = isChaaa(e.Message);
                Console.WriteLine("Recieved: " + e.Message);
                if (resp.isChaaa == true)
                {
                    e.Author.SendMessage(resp.msg);
                    Console.WriteLine("Sending Message");
                }
                else
                {
                    // TODO: Add this to the string builder message
                    e.Author.SendMessage("Bad vibes");
                }
            };

            client.MessageReceived += (sender, e) =>// Channel message has been reccieved
            {
                bool send = false;
                if (ChannelMessageCount >= 1)
                {
                    ChannelMessageCount = 0;
                } else
                {
                    send = true;
                }

                // For some reason it is sending a ton of messages
                if (send)
                {
                    var resp = isChaaa(e.MessageText);

                    if (resp.isChaaa == true)
                    {
                        e.Channel.SendMessage(resp.msg);
                        ++ChannelMessageCount;
                    }
                }
                                 
            };


            // Attempt to connect to login to discord

            try
            {
                Console.WriteLine("Attempting to login to discord");
                client.SendLoginRequest();
                Console.WriteLine("Connecting client in separate thread");
                client.Connect();
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //client.SendLoginRequest();
            //Thread t = new Thread(client.Connect);
            //t.Start();
        }

        /// <summary>
        /// This structure isn't exactly useful but in the future might be helpful for extensibillity
        /// </summary>
        public struct ChaaaResponse
        {
            public string msg;
            public bool isChaaa;
        }

        /// <summary>
        /// This message will determine if the input message is response worthy
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
     

        public static ChaaaResponse isChaaa(string msg)
        {
            ChaaaResponse ret = new ChaaaResponse();
            string head;
            if (msg.Length >= 3)
            {
                head = msg.Substring(0, 3);
            } else
            {
                head = "dead";
            }

            // Someone said Chaaaaa
            if (head.ToUpper() == "CHA")
            {
                // Count the As for the response
                int count = msg.Count(c => c == 'a' | c == 'A');
                ret.msg = BuildChaa(count);
                ret.isChaaa = true;
            } else
            {
                ret.isChaaa = false;
            }

            return ret;
        }

        public static string BuildChaa(int ACount) 
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("CHA");
            for (int i = 0; i < ACount - 1; i++)
            {
                if (isEven(i))
                {
                    Builder.Append("a");
                } else
                {
                    Builder.Append("A");
                }
            }

            Builder.Append(" ride the wave brah");
            return Builder.ToString();
        }

        public static bool isEven(int n)
        {
            return n % 2 == 0 ? true : false;
        }
    }
}
