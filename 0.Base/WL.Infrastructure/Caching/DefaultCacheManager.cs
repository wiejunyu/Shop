namespace WL.Infrastructure.Caching
{
    using ServiceStack.Redis;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// 默认缓存管理器。
    /// </summary>
    public class DefaultCacheManager : ICacheManager
    {
        /// <summary>
        /// 静态字典缓存数据。
        /// </summary>
        private readonly IDictionary<string, CacheEntity> _cache = new Dictionary<string, CacheEntity>();

        /// <summary>
        /// 锁定对象。
        /// </summary>
        private static readonly object _lockObject = new object();

        /// <summary>
        /// 获得所有 Key。
        /// </summary>
        public IEnumerable<string> Keys
        {
            get { return _cache.Keys; }
        }

        /// <summary>
        /// 获取缓存值。
        /// </summary>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="key">键对象</param>
        /// <returns>值对象</returns>
        public TValue Get<TValue>(string key)
        {
            CacheEntity cache = null;

            if (this._cache.ContainsKey(key))
                cache = this._cache[key];

            return cache != null && cache.ExpiredTime > DateTime.UtcNow ? (TValue)cache.Value : default(TValue);
        }

        /// <summary>
        /// 获取缓存值，如果不存在，则添加缓存。
        /// </summary>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="key">键对象</param>
        /// <param name="acquire">获得值对象的委托。</param>
        /// <returns>值对象。</returns>
        public TValue GetOrAdd<TValue>(string key, Func<TValue> acquire)
        {
            var result = Get<TValue>(key);

            if (result != null)
                return result;

            result = acquire();

            if (result != null)
                Set(key, result);

            return result;
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
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="key">键对象</param>
        /// <param name="value">值对象</param>
        public void Set<TValue>(string key, TValue value)
        {
            this.Set(key, value, new TimeSpan(0, 0, 20, 0));
        }

        /// <summary>
        /// 增加缓存值。
        /// </summary>
        /// <typeparam name="TValue">值类型。</typeparam>
        /// <param name="key">键对象。</param>
        /// <param name="value">值对象。</param>
        /// <param name="validFor">有效时长。</param>
        /// <exception cref="ArgumentNullException">key 不可以是空的。</exception>
        public void Set<TValue>(string key, TValue value, TimeSpan validFor)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key", "键对象不可以是空的。");

            var cache = new CacheEntity
            {
                Value = value,
                ExpiredTime = DateTime.UtcNow.Add(validFor)
            };

            lock (_lockObject)
            {
                _cache[key] = cache;
            }
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
        /// 移除缓存值。
        /// </summary>
        /// <param name="key">键对象。</param>
        public void Remove(string key)
        {
            if (this._cache.ContainsKey(key))
            {
                this._cache.Remove(key);
            }
        }

        /// <summary>
        /// 移除缓存值。
        /// </summary>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="key">键对象</param>
        /// <param name="result">值对象</param>
        public void Remove<TValue>(string key, out TValue result)
        {
            result = default(TValue);

            if (this._cache.ContainsKey(key))
            {
                result = (TValue)_cache[key].Value;
                this._cache.Remove(key);
            }
        }
        /// <summary>
        /// 清空缓存。
        /// </summary>
        public void Clear()
        {
            this._cache.Clear();
        }
        /// <summary>
        /// 缓存实体。
        /// </summary>
        private class CacheEntity
        {
            /// <summary>
            /// 缓存值。
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// 过期时间（UTC）
            /// </summary>
            public DateTime ExpiredTime { get; set; }
        }
    }
}