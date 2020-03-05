using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace WL.Infrastructure.RabbitMQ
{
    /// <summary>
    /// RabbitMQ帮助类
    /// </summary>
    public class RabbitMQHelper
    {
        /// <summary>
        /// Rabbitmq服务IP，不包含端口,一般为localhost
        /// </summary>
        public static string RabbitMQ_HostName
        {
            get
            {
                return ConfigurationManager.AppSettings["RabbitMQ_HostName"] ?? string.Empty;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string RabbitMQ_UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["RabbitMQ_UserName"] ?? string.Empty;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public static string RabbitMQ_PassWord
        {
            get
            {
                return ConfigurationManager.AppSettings["RabbitMQ_PassWord"] ?? string.Empty;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sQueueName">队列名称</param>
        /// <param name="sContent">内容</param>
        /// <returns></returns>
        public static void Send(string sQueueName,string sContent)
        {
            var factory = new ConnectionFactory() { HostName = RabbitMQ_HostName, UserName = RabbitMQ_UserName, Password = RabbitMQ_PassWord };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(sQueueName, false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(sContent);
                    channel.BasicPublish("", sQueueName, null, body);
                }
            }
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="sQueueName">队列名称</param>
        /// <returns></returns>
        public static string Receive(string sQueueName)
        {
            var factory = new ConnectionFactory() { HostName = RabbitMQ_HostName, UserName = RabbitMQ_UserName, Password = RabbitMQ_PassWord };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(sQueueName, false, false, false, null);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(sQueueName, true, consumer);
                    var ea = consumer.Queue.Dequeue();
                    var body = ea.Body;
                    return Encoding.UTF8.GetString(body);
                }
            }
        }

        ///// <summary>
        ///// 获取消息并发送邮件
        ///// </summary>
        ///// <param name="queue">需要进入的队列</param>
        ///// <param name="MailSending">发送邮件函数</param>
        ///// <returns></returns>
        //public static bool Receive(string sQueue, deleMailSending MailSending)
        //{
        //    //初始化
        //    ConnectionFactory factory = new ConnectionFactory();
        //    //Rabbitmq服务IP
        //    factory.HostName = HostName;
        //    //用户名
        //    factory.UserName = UserName;
        //    //密码
        //    factory.Password = Password;
        //    factory.Port = AmqpTcpEndpoint.UseDefaultPort;
        //    factory.VirtualHost = "/";

        //    using (var connection = factory.CreateConnection())
        //    {
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare(queue: "hello",
        //                durable: false,
        //                exclusive: false,
        //                autoDelete: false,
        //                arguments: null);
        //            var consumer = new EventingBasicConsumer(channel);
        //            consumer.Received += (model, ea) =>
        //            {
        //                var body = ea.Body;
        //                var message = Encoding.UTF8.GetString(body);
        //            };
        //            channel.BasicConsume(queue: "hello",
        //                autoAck: true,
        //                consumer: consumer);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 返回队列模型
        ///// </summary>
        ///// <param name="queue">队列</param>
        ///// <returns>队列模型</returns>
        //public static IModel GetChannelModel(string queue) 
        //{
        //    //初始化
        //    var factory = new ConnectionFactory();
        //    //Rabbitmq服务IP
        //    factory.HostName = HostName;
        //    //用户名
        //    factory.UserName = UserName;
        //    //密码
        //    factory.Password = Password;
        //    factory.Port = AmqpTcpEndpoint.UseDefaultPort;
        //    factory.VirtualHost = "/";
        //    var connection = factory.CreateConnection();
        //    var channel = connection.CreateModel();
        //    channel.QueueDeclare(
        //            //队列
        //            queue: queue,
        //            //是否设置队列为持久化
        //            durable: false,
        //            //排他
        //            exclusive: false,
        //            //设置是否自动删除
        //            autoDelete: false,
        //            //其他参数
        //            arguments: null);
        //    return channel;
        //}


        ///// <summary>
        ///// 从队列获取消息
        ///// </summary>
        ///// <param name="channel">队列模型</param>
        ///// <returns></returns>
        //public static string GetMessageIsChannel(IModel channel)
        //{
        //    string message = null;
        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received += (model, ea) => {
        //        message = Encoding.UTF8.GetString(ea.Body);
        //    };
        //    return message;
        //}

        ///// <summary>
        ///// 从队列模型消费消息
        ///// </summary>
        ///// <param name="channel">队列模型</param>
        ///// <param name="queue">队列</param>
        ///// <returns></returns>
        //public static void BasicConsumeChannel(IModel channel,string queue)
        //{
        //    var consumer = new EventingBasicConsumer(channel);
        //    //消费消息
        //    channel.BasicConsume(queue: queue,
        //        autoAck: true,
        //        consumer: consumer);
        //}
    }

    /// <summary>
    /// 进入队列
    /// </summary>
    public class RabbitMQQueue
    {
        /// <summary>
        /// 邮箱队列
        /// </summary>
        public static string EmailQueue
        {
            get { return "EmailQueue"; }
        }
    }

    /// <summary>
    /// 操作Key
    /// </summary>
    public class RabbitMQKey
    {
        /// <summary>
        /// 发送注册成功邮件
        /// </summary>
        public static string SendRegisterMessageIsEmail
        {
            get { return "SendRegisterMessageIsEmail"; }
        }
    }

}
