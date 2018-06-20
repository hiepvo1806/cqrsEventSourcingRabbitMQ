using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.MessageService
{
    public class MessageNotify : IMessageNotify<string>
    {
        private ConnectionFactory _connectionFactory;
        private string HostName = "localhost";
        private string UserName = "guest";
        private string Password = "guest";
        private string VirtualHost = "";
        private string ExchangeName = "CQRSDemo.Exchange";
        private int Port = 0;
        private IModel _model;
        private IConnection _connection;
        private Subscription _subscription;
        private IBasicProperties properties;

        public MessageNotify()
        {
            SetUpService();
        }
        public void NotifyService(string content)
        {
            byte[] messageBuffer = Encoding.Default.GetBytes(content);
            _model.BasicPublish(ExchangeName, "", properties, messageBuffer);
        }

        private void SetUpService()
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
            properties = _model.CreateBasicProperties();
            properties.Persistent = true;
        }
    }
}
