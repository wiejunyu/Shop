using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using WL.Infrastructure.Logging;
using System.Threading;

namespace WL.Infrastructure.Caching
{
    /// <summary>
    /// Redis 缓存管理器。
    /// </summary>
    public class RedisCacheManager : ICacheManager
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
                int db;
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
        public void ChangeDB(int db)
        {
            changeddb = db;
        }

        /// <summary>
        /// 取得一个缓存数据库实例
        /// </summary>
        /// <returns></returns>
        private IRedisClient GetRedisClient()
        {
            //RedisClient redisClient = new RedisClient(Host, Port, Password);
           
            var redisClient = _pool.GetClient();
            if (changeddb >= 0)
            {
                redisClient.Db = changeddb;
            }
            return redisClient;
        }

        /// <summary>
        /// 获得所有 Key。
        /// </summary>
        public IEnumerable<string> Keys
        {
            get
            {
                using (var redisClient = GetRedisClient())
                {
                    return redisClient.GetAllKeys() ?? new List<string>();
                }
            }
        }

        /// <summary>
        /// 获取缓存值。
        /// </summary>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <returns>值对象。</returns>
        public TValue Get<TValue>(string key)
        {
            return GetCache<TValue>(key, false);
        }

        /// <summary>
        /// 获取缓存值，如果不存在，则添加缓存。
        /// </summary>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <param name="acquire">获得值对象的委托。</param>
        /// <returns>值对象。</returns>
        public TValue GetOrAdd<TValue>(string key, Func<TValue> acquire)
        {
            return GetOrAdd(key, acquire, new TimeSpan(0, 0, 20, 0));
        }

        /// <summary>
        /// 获取缓存值，如果不存在，则添加缓存。
        /// </summary>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <param name="acquire">获得值对象的委托。</param>
        /// <param name="validFor"></param>
        /// <returns>值对象。</returns>
        public TValue GetOrAdd<TValue>(string key, Func<TValue> acquire, TimeSpan validFor)
        {
            var result = Get<TValue>(key);

            if (result != null)
                return result;

            result = acquire();

            if (result != null)
                Set(key, result, validFor);

            return result;
        }

        /// <summary>
        /// 增加缓存值。
        /// </summary>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <param name="value">值对象。</param>
        public void Set<TValue>(string key, TValue value)
        {
            this.Set(key, value, new TimeSpan(0, 0, 20, 0));
        }
        /// <summary>
        /// 回调专用
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool LockRedis(string key, string content, TimeSpan validFor)
        {
            using (IRedisClient redisClient = Cacheder.GetRedisClient())
            {
                using (redisClient.AcquireLock(key + "_lock", TimeSpan.FromSeconds(5)))
                {
                    var counter = redisClient.Get<string>(key);

                    if (counter != null && counter != "")
                    {
                        return false;
                    }
                    else
                    {
                        Thread.Sleep(100);

                        redisClient.Set(key, content, validFor);

                        return true;
                    }

                }
            }
        }
        /// <summary>
        /// 增加缓存值。
        /// </summary>
        /// <remarks>
        /// 传入对象时，必须是可序列化才能保存成功。
        /// 假如对象 A 中包含着对象 B。
        /// 则对象 B 也必须要标记为可序列化才可以保存成功。
        /// </remarks>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <param name="value">值对象。</param>
        /// <param name="validFor">有效时长。</param>
        public void Set<TValue>(string key, TValue value, TimeSpan validFor)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key", "键对象不可以是空的。");

            if (value == null)
                throw new ArgumentNullException("value", "值对象不可以是空的。");

            using (IRedisClient redisClient = GetRedisClient())
            {
                redisClient.Set(key, value, validFor);
            }
        }

        /// <summary>
        /// 移除缓存值。
        /// </summary>
        /// <param name="key">键对象。</param>
        public void Remove(string key)
        {
            using (var redisClient = GetRedisClient())
            {
                redisClient.Remove(key);
            }
        }
        /// <summary>
        /// 移除缓存值。
        /// </summary>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <param name="result">值对象。</param>
        public void Remove<TValue>(string key, out TValue result)
        {
            using (var redisClient = GetRedisClient())
            {
                result = redisClient.Get<TValue>(key);

                if (result != null)
                {
                    redisClient.Remove(key);
                }
            }
        }

        /// <summary>
        /// 清空缓存。
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        private TValue GetCache<TValue>(string key, bool isSession, int cacheTime = 40)
        {
            using (IRedisClient redisClient = GetRedisClient())
            {
                var result = redisClient.Get<TValue>(key);

                if (isSession && result != null)
                {
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddMinutes(cacheTime));
                }

                return result;
            }
        }
    }
}