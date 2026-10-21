using System;

namespace TelegramBot.SessionGenerator
{
    class Program
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {

            ClientFactory clientFactory = new ClientFactory();
            try
            {
                var client = clientFactory.GetClient().Result;
                Console.WriteLine("Archive session.dat generated!!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Could not generate new session.dat");
            }

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }
    }
}
