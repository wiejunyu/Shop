using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Caching
{
    /// <summary>
    /// redis集群
    /// </summary>
    public class RedisCluster
    {
        #region 服务器A

        public static string Host_A
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisHost_A"].ToString();
            }
        }

        public static int RedisDB_A
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RedisDB_A"]);
            }
        }

        public static int Port_A
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RedisPost_A"]);
            }
        }

        public static string Password_A
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisPassword_A"].ToString();
            }
        }

        public static readonly PooledRedisClientManager _poolA = new PooledRedisClientManager(
            new[] { Password_A + "@" + Host_A + ":" + Port_A },
            null,
            new RedisClientManagerConfig
            {
                AutoStart = true,
                MaxWritePoolSize = 2000,
                MaxReadPoolSize = 2000,
                DefaultDb = RedisDB_A
            });

        #endregion 服务器A

        #region 服务器B

        public static string Host_B
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisHost_B"].ToString();
            }
        }

        public static int RedisDB_B
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RedisDB_B"]);
            }
        }

        public static int Port_B
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RedisPost_B"]);
            }
        }

        public static string Password_B
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisPassword_B"].ToString();
            }
        }

        public static readonly PooledRedisClientManager _poolB = new PooledRedisClientManager(
            new[] { Password_B + "@" + Host_B + ":" + Port_B },
            null,
            new RedisClientManagerConfig
            {
                AutoStart = true,
                MaxWritePoolSize = 2000,
                MaxReadPoolSize = 2000,
                DefaultDb = RedisDB_B
            });

        #endregion 服务器B

        public static string Master = "A";

        private static int changeddb = -1;

        /// <summary>
        /// 切换到哪个一数据库
        /// </summary>
        /// <param name="db"></param>
        public static void ChangeDB(int db)
        {
            changeddb = db;
        }

        /// <summary>
        /// 设置指向的redis服务器
        /// </summary>
        /// <param name="master"></param>
        public static void SetMaster(string master)
        {
            Master = master;
        }

        /// <summary>
        /// 取得一个缓存数据库实例
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetRedisClient()
        {
            IRedisClient redisClient = null;

            if (Master.Equals("A"))
            {
                redisClient = _poolA.GetClient();
            }
            else
            {
                redisClient = _poolB.GetClient();
            }
            if (changeddb >= 0)
            {
                redisClient.Db = changeddb;
            }
            return redisClient;
        }

        public static List<string> GetContianKeys(string contains)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                List<string> result = redisClient.GetAllKeys();
                if (result == null || result.Count == 0) return result;

                result = result.FindAll(m => m.Contains(contains));

                return result;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cachetime"></param>
        public static void AddCache<T>(string key, T value, int cachetime)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.Set<T>(key, value, DateTime.Now.AddMinutes(cachetime));
            }
        }

        public static void AddCache<T>(string key, T value, DateTime expire)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.Set<T>(key, value, expire);
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.Remove(key);
            }
        }

        /// <summary>
        /// 取得缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            var rs = GetCache<T>(key, false);
            return rs;
        }

        public static T GetCacheAndRemove<T>(string key)
        {
            var rs = GetCache<T>(key);
            RemoveCache(key);
            return rs;
        }

        public static T GetCache<T>(string key, bool isSession, int cacheTime = 40)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                var result = redisClient.Get<T>(key);

                if (isSession == true && result != null)
                {
                    //AddCache<T>(key,result,cacheTime);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddMinutes(cacheTime));
                }
                return result;
            }
        }

        public static void Enqueue<T>(string key, T t)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    formatter.Serialize(ms, t);
                    redisClient.LPush(key, ms.ToArray());
                }
            }
        }

        public static T Dequeue<T>(string key)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                byte[] bytes = redisClient.RPop(key);
                if (bytes == null) return default(T);
                T t;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    t = (T)formatter.Deserialize(ms);
                }
                return t;
            }
        }

        public static void Enqueue(string key, string val)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                redisClient.EnqueueItemOnList(key, val);
            }
        }

        public static string Dequeue(string key)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                return redisClient.DequeueItemFromList(key);
            }
        }

        public static Dictionary<string, string> Info()
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                return redisClient.Info;
            }
        }

        public static long QueueLength(string key)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                var rs = redisClient.LLen(key);
                return rs;
            }
        }

        /// <summary>
        /// 删除缓存，
        /// 删存key中前面为xxxx的缓存
        /// 比如有缓存KSW1，KSW2,KSW3,KK12
        /// DeleteCacheByHeaderContains("KSW") 将删除KSW1,KSW2,KSW3
        /// </summary>
        /// <param name="headerKey"></param>
        public static void RemoveCacheByHeaderContains(string headerKey)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                List<string> keys = redisClient.SearchKeys(headerKey + "*");
                if (keys == null || keys.Count <= 0) return;
                foreach (var key in keys)
                {
                    redisClient.Remove(key);
                }
            }
        }

        public static void AddToList(string listid, string val)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.AddItemToList(listid, val);
            }
        }

        public static List<string> GetList(string listid)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                return redisClient.GetAllItemsFromList(listid);
            }
        }
    }
}