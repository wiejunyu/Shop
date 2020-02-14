using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WL.Infrastructure.Caching
{
    /// <summary>
    /// redis心跳检测管理器
    /// </summary>
    public class RedisHeartManage
    {
        /// <summary>
        /// Redis机器 A
        /// </summary>
        public RedisHeart _redisHeartA = new RedisHeart(RedisCluster.Host_A, RedisCluster.Port_A, RedisCluster.Password_A, true, true, "A");

        /// <summary>
        /// Redis机器 B
        /// </summary>
        public RedisHeart _redisHeartB = new RedisHeart(RedisCluster.Host_B, RedisCluster.Port_B, RedisCluster.Password_B, false, true, "B");

        /// <summary>
        /// RedisA 心跳检测
        /// </summary>
        private void StartHeartA()
        {
            int noConnectionCount = 0;

            while (true)
            {
                bool pingSuccess = _redisHeartA.Heart();

                //状态正常运行中.检测是否挂掉
                if (_redisHeartA.IsRuning)
                {
                    #region 检测Redis服务器是否宕机

                    if (!pingSuccess && noConnectionCount >= 5) //Redis连续5次没ping通..则代表redis挂掉
                    {
                        if (_redisHeartA.IsMaster) //如果A是主机
                        {
                            //Console.WriteLine(_redisHeartA.RedisName + "状态宕机,变为从机");

                            //Console.WriteLine(_redisHeartB.RedisName + "停止挂靠" + _redisHeartA.RedisName + ",成为主机");

                            //redisB 马上停止挂靠 redisA 并且成为主机
                            _redisHeartB.SlaveOfNoOne();
                            //设置 redisB 为主机
                            _redisHeartB.IsMaster = true;

                            //设置redisA 状态为挂掉
                            _redisHeartA.IsRuning = false;
                            //设置redisA 为从机
                            _redisHeartA.IsMaster = false;

                            //短信通知开发人员
                            //SendMessage(_redisHeartA.RedisName+ "  Redis服务器挂掉了..请及时查看维护 ");

                            //Console.WriteLine("网站切换Redis地址为:" + _redisHeartB.RedisName);

                            //集群切换redis地址
                            RedisCluster.Master = "B";
                        }
                        else //如果A是从机
                        {
                            //短信通知开发人员
                            //SendMessage(_redisHeartA.RedisName+ "  Redis服务器挂掉了..请及时查看维护 ");

                            //设置redisA 状态为挂掉
                            _redisHeartA.IsRuning = false;

                            //Console.WriteLine(_redisHeartA.RedisName + "状态宕机..请及时查看维护");
                        }
                    }
                    else if (!pingSuccess) //Redis没ping通..则 累加次数
                    {
                        noConnectionCount++;
                        //Console.WriteLine(_redisHeartA.RedisName + "服务器没有响应,次数:" + noConnectionCount);
                    }
                    else //redis成功则 累加数量清0
                    {
                        noConnectionCount = 0;
                        //Console.WriteLine(_redisHeartA.RedisName + "心跳检测正常....");
                    }

                    #endregion 检测Redis服务器是否宕机
                }
                else
                {
                    #region 检测Redis是否恢复正常

                    if (pingSuccess) //正常连接
                    {
                        //Console.WriteLine(_redisHeartA.RedisName + "心跳恢复正常....");

                        _redisHeartA.IsRuning = true;

                        if (_redisHeartB.IsRuning) //若B服务器正常
                        {
                            //RedisA 恢复正常。马上挂靠 RedisB
                            _redisHeartA.Slaveof(_redisHeartB.IP, _redisHeartB.Port);

                            //Console.WriteLine(_redisHeartA.RedisName + " 挂靠 " + _redisHeartB.RedisName);
                        }
                        else//若B服务器不正常
                        {
                            _redisHeartA.IsMaster = true;
                            //RedisA 自己成为主机
                            _redisHeartA.SlaveOfNoOne();

                            RedisCluster.Master = "A";
                        }

                        //短信通知开发人员
                        //SendMessage(_redisHeartA.RedisName+ "  Redis服务器恢复了正常..请及时查看");

                        //Console.WriteLine(_redisHeartA.RedisName + "Redis服务器恢复了正常..请及时查看");
                    }

                    #endregion 检测Redis是否恢复正常
                }
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// RedisB 心跳检测
        /// </summary>
        private void StartHeartB()
        {
            int noConnectionCount = 0;

            while (true)
            {
                bool pingSuccess = _redisHeartB.Heart();

                //状态正常运行中.检测是否挂掉
                if (_redisHeartB.IsRuning)
                {
                    #region 检测Redis服务器是否宕机

                    if (!pingSuccess && noConnectionCount >= 5) //Redis连续5次没ping通..则代表redis挂掉
                    {
                        if (_redisHeartB.IsMaster) //如果B是主机
                        {
                            //Console.WriteLine(_redisHeartB.RedisName + "状态宕机,变为从机");

                            //Console.WriteLine(_redisHeartA.RedisName + "停止挂靠" + _redisHeartB.RedisName + ",成为主机");

                            //redisA 马上停止挂靠 redisB, 并且成为主机
                            _redisHeartA.SlaveOfNoOne();
                            //设置 redisA 为主机
                            _redisHeartA.IsMaster = true;

                            //短信通知
                            //Console.WriteLine(_redisHeartB.RedisName + "  Redis服务器挂掉了..请及时查看维护 ");

                            //设置redisB 状态为挂掉
                            _redisHeartB.IsRuning = false;
                            //设置redisB 为从机
                            _redisHeartB.IsMaster = false;

                            //短信通知开发人员
                            //SendMessage(_redisHeartA.RedisName+ "  Redis服务器挂掉了..请及时查看维护 ");

                            //网站切换redis地址

                            RedisCluster.Master = "A";

                            //Console.WriteLine("网站切换Redis地址为:" + _redisHeartA.RedisName);
                        }
                        else //如果B是从机
                        {
                            //短信通知开发人员
                            //SendMessage(_redisHeartA.RedisName+ "  Redis服务器挂掉了..请及时查看维护 ");

                            //Console.WriteLine(_redisHeartB.RedisName + "状态宕机..请及时查看维护");

                            //设置redisB 状态为挂掉
                            _redisHeartB.IsRuning = false;
                        }
                    }
                    else if (!pingSuccess) //Redis没ping通..则 累加次数
                    {
                        noConnectionCount++;
                        //Console.WriteLine(_redisHeartB.RedisName + "服务器没有响应,次数:" + noConnectionCount);
                    }
                    else //redis成功则 累加数量清0
                    {
                        noConnectionCount = 0;
                        //Console.WriteLine(_redisHeartB.RedisName + "心跳检测正常....");
                    }

                    #endregion 检测Redis服务器是否宕机
                }
                else
                {
                    #region 检测Redis是否恢复正常

                    if (pingSuccess) //正常连接
                    {
                        //Console.WriteLine(_redisHeartB.RedisName + "心跳恢复正常....");

                        _redisHeartB.IsRuning = true;

                        if (_redisHeartA.IsRuning) //若A主机正常运行 则挂靠A
                        {
                            //RedisB 恢复正常。马上挂靠 RedisA
                            _redisHeartB.Slaveof(_redisHeartA.IP, _redisHeartA.Port);

                            //Console.WriteLine(_redisHeartB.RedisName + " 挂靠 " + _redisHeartA.RedisName);
                        }
                        else//A主机宕机。则 B独立。成为主机
                        {
                            _redisHeartB.IsMaster = true;
                            _redisHeartB.SlaveOfNoOne();
                            RedisCluster.Master = "B";
                        }
                    }

                    #endregion 检测Redis是否恢复正常
                }
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// 开始心跳检测
        /// </summary>
        public void StartCheckHeart()
        {
            _redisHeartA.SlaveOfNoOne();
            _redisHeartB.Slaveof(_redisHeartA.IP, _redisHeartA.Port);

            Thread threadA = new Thread(new ThreadStart(StartHeartA));
            threadA.IsBackground = true;
            threadA.Start();

            Thread threadB = new Thread(new ThreadStart(StartHeartB));
            threadB.IsBackground = true;
            threadB.Start();
        }
    }
}