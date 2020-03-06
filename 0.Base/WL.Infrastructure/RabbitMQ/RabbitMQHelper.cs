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
        public static void Send(string sQueueName, string sContent)
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
                    channel.BasicQos(0, 1, false);
                    //消费队列,这里如果不消费队列获取不到消息
                    channel.BasicConsume(sQueueName, true, consumer);
                    var ea = consumer.Queue.Dequeue();
                    var body = ea.Body;
                    return Encoding.UTF8.GetString(body);
                }
            }
        }

        public delegate bool deleMailSending(string Addressee, string Title, string Body);

        /// <summary>
        /// 获取消息并发送邮件
        /// </summary>
        /// <param name="sQueueName">队列名称</param>
        /// <param name="MailSending">发送邮件函数</param>
        /// <returns></returns>
        public static bool Receive(string sQueueName, deleMailSending MailSending)
        {
            var factory = new ConnectionFactory() { HostName = RabbitMQ_HostName, UserName = RabbitMQ_UserName, Password = RabbitMQ_PassWord };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(sQueueName, false, false, false, null);
                    var consumer = new QueueingBasicConsumer(channel);
                    //消费队列,这里如果不消费队列获取不到消息
                    channel.BasicConsume(sQueueName, true, consumer);
                    var ea = consumer.Queue.Dequeue();
                    var body = ea.Body;
                    //
                    string Email = Encoding.UTF8.GetString(body);
                    //发送邮件，成功就结束，不成功就从新回到队列
                    if (MailSending(Email, "欢迎你注册宇宙物流", "欢迎你注册宇宙物流"))
                    {
                        return true;
                    }
                    else
                    {
                        Send(RabbitMQKey.SendRegisterMessageIsEmail, Email);
                    }
                }
            }
            return true;
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
