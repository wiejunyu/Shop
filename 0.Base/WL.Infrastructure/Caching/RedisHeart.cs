using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Caching
{
    /// <summary>
    /// Redis心跳检测 主从切换
    /// </summary>
    public class RedisHeart
    {
        public RedisHeart()
        {
        }

        public RedisHeart(string ip, int port, string password, bool isMaster, bool isRuning, string master)
        {
            this.IP = ip;
            this.Port = port;
            this.Password = password;
            this.IsMaster = isMaster;
            this.IsRuning = isRuning;
            this.Master = master;
        }

        public string Master { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否为主机 true为主机,false为从机
        /// </summary>
        public bool IsMaster { get; set; }

        /// <summary>
        /// 是否正常运行,true 为正常，false表示该服务已经挂掉
        /// </summary>
        public bool IsRuning { get; set; }

        /// <summary>
        /// Redis机器名称
        /// </summary>
        public string RedisName
        {
            get
            {
                string masterOrSlaveof = IsMaster ? "主机" : "从机";

                string redisName = string.Format("{0}:<{1}:{2}>", masterOrSlaveof, IP, Port);

                return redisName;
            }
        }

        /// <summary>
        /// 获取redis链接
        /// </summary>
        /// <returns></returns>
        public RedisClient GetClient()
        {
            if (this.Master.Equals("A"))
            {
                return RedisCluster._poolA.GetClient() as RedisClient;
            }
            else
            {
                return RedisCluster._poolB.GetClient() as RedisClient;
            }
        }

        /// <summary>
        /// Ping Redis查看是否连接
        /// </summary>
        /// <returns></returns>
        public bool Heart()
        {
            try
            {
                using (RedisClient redisClient = GetClient())
                {
                    return redisClient.Ping();
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 脱离主机，自己成为主机
        /// </summary>
        public void SlaveOfNoOne()
        {
            using (RedisClient redisClient = GetClient())
            {
                try
                {
                    redisClient.SlaveOfNoOne();
                }
                catch
                {
                    //Console.WriteLine("警报:" + this.RedisName + "脱离主机，设置自己为主机操作失败，可能AB2台Redis服务器同时挂掉...请及时查看维护");
                }
            }
        }

        /// <summary>
        /// 挂靠主机
        /// </summary>
        /// <param name="ip">主机IP</param>
        /// <param name="port">主机端口</param>
        public void Slaveof(string ip, int port)
        {
            using (RedisClient redisClient = GetClient())
            {
                redisClient.SlaveOf(ip, port);
            }
        }
    }
}