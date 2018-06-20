using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class QueueProcessor
    {
        public ConnectionFactory _connectionFactory;
        public string HostName = "localhost";
        public string UserName = "guest";
        public string Password = "guest";
        public string VirtualHost = "";
        public string QueueName = Guid.NewGuid().ToString();
        public string ExchangeName = "CQRSDemo.Exchange";

        public  int Port = 0;
        public  IModel _model;
        public  IConnection _connection;
        private Subscription _subscription;

        public bool Enabled { get; set; }

        public void QueueProcessorStart()
        {
            _subscription = new Subscription(_model, QueueName, false);
            var consumer = new ConsumeDelegate(Poll);
            consumer.Invoke();
        }

        private delegate void ConsumeDelegate();
        private void Poll()
        {
            while (Enabled)
            {
                //Get next message
                var deliveryArgs = _subscription.Next();
                //Deserialize message
                var message = Encoding.Default.GetString(deliveryArgs.Body);

                //Handle Message
                Console.WriteLine($"============= {DateTime.Now.ToShortTimeString()}==========");
                Console.WriteLine("Message Recieved - {0}", FormatMessage(message));
                Console.WriteLine($"====================================");
                //Acknowledge message is processed
                _subscription.Ack(deliveryArgs);
                if (message.Contains("exit"))
                {
                    Enabled = false;
                    Console.WriteLine("Dispose");
                    DestroyQueue();
                }
                    
            }
        }
        public void Setup()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };

            if (string.IsNullOrEmpty(VirtualHost) == false)
                _connectionFactory.VirtualHost = VirtualHost;
            if (Port > 0)
                _connectionFactory.Port = Port;
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);
            _model.QueueDeclare(QueueName, true, false, false, null);
            _model.QueueBind(QueueName, ExchangeName, "", null);
        }

        public void DestroyQueue()
        {
            _model.QueueDelete(QueueName, false, false);
        }

        #region helper
        private string FormatMessage(string inputString)
        {
            inputString = inputString.Replace(',', '\n');
            inputString = inputString.Replace('{', '\n');
            inputString = inputString.Replace('}', '\n');
            return inputString;
        }
        #endregion
    }
}
