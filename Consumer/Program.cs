using RabbitMQ.Client;
using System;

namespace Consumer
{
    class Program 
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var model = new QueueProcessor();
            model.Setup();
            model.Enabled = true;
            model.QueueProcessorStart();
            //Console.ReadLine();
            //model.DestroyQueue();
        }

       
    }
}
