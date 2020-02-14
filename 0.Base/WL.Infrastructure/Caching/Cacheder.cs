using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace WL.Infrastructure.Caching
{
    public class Cacheder
    {
        private static readonly PooledRedisClientManager _pool = new PooledRedisClientManager(
            new[] { Password + "@" + Host + ":" + Port },
            null,
            new RedisClientManagerConfig
            {
                AutoStart = true,
                MaxWritePoolSize = 2000,
                MaxReadPoolSize = 2000,
                DefaultDb = DefaultDB
            });

        /// <summary>
        /// 缓存数据库地址
        /// </summary>
        public static string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisServer"].Split(':')[0];
            }
        }

        public static int DefaultDB
        {
            get
            {
                int db = 0;
                var dbstr = ConfigurationManager.AppSettings["XRedisDB"];
                int.TryParse(dbstr, out db);
                return db;
            }
        }

        /// <summary>
        /// 缓存数据库端口
        /// </summary>
        public static int Port
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["RedisServer"].Split(':')[1]);
            }
        }

        /// <summary>
        /// 缓存数据库密码
        /// </summary>
        public static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["REDIS_CACHE_PASSWORD"] ?? string.Empty;
            }
        }

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
        /// 取得一个缓存数据库实例
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetRedisClient()
        {
            //RedisClient redisClient = new RedisClient(Host, Port, Password);
            var redisClient = _pool.GetClient();
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

        /// <summary>
        /// 进队
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// 非阻塞式出队
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 阻塞式出队
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="Secs">超时时间, 单位:秒, 默认为:0</param>
        /// <returns></returns>
        public static T BDequeue<T>(string key, int Secs = 0)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                byte[][] bytes = redisClient.BRPop(key, 0);
                if (bytes == null) return default(T);
                T t;
                using (MemoryStream ms = new MemoryStream(bytes[1]))
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

        public static List<T> GetList<T>(string listid)
        {
            List<T> lValue = new List<T>();
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                long nItem = redisClient.LLen(listid);
                for (long iItem = 0; iItem < nItem; iItem++)
                {
                    byte[] bytes = redisClient.LIndex(listid, (int)iItem);
                    if (bytes == null) continue;
                    T t;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        t = (T)formatter.Deserialize(ms);
                        lValue.Add(t);
                    }
                }
            }
            return lValue;
        }
        public static bool FindInHash<T>(string hashid, byte[] key, ref T value)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                byte[] bytes = redisClient.HGet(hashid, key);
                if( bytes != null)
                {
                    using(MemoryStream ms = new MemoryStream(bytes))
                    {
                        value = (T)formatter.Deserialize(ms);
                        return true;
                    }
                }
            }

            return false;
        }

        public static void DelFromHash(string hashid, byte[] key)
       {
           using (RedisClient redisClient = GetRedisClient() as RedisClient)
           {
               redisClient.HDel(hashid, key);
           }
       }

        public static void SetInHash<T>(string hashid,byte[] key, T value)
       {
           using (RedisClient redisClient = GetRedisClient() as RedisClient)
           {
               BinaryFormatter formatter = new BinaryFormatter();
               using (MemoryStream ms = new MemoryStream())
               {
                   formatter.Serialize(ms, value);
                   redisClient.HSet(hashid, key, ms.ToArray());
               }
           }
       }

        public static byte[][] GetAllHashKey(string hashid)
        {
            using (RedisClient redisClient = GetRedisClient() as RedisClient)
            {
                return redisClient.HKeys(hashid);
            }
        }

        public static bool IsKeyExist(String key)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                return redisClient.ContainsKey(key);
            }
        }

        public static void SetKeyExpire(String key, TimeSpan ts)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.ExpireEntryIn(key, ts);
            }
        }

        public static void SetKeyExpireAt(String key,DateTime time)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.ExpireEntryAt(key, time);
            }
        }
    }
}