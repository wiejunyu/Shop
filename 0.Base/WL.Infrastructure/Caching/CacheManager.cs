namespace WL.Infrastructure.Caching
{
    using System;

    /// <summary>
    /// 缓存管理器。
    /// </summary>
    public class CacheManager
    {
        #region Members

        /// <summary>
        /// 缓存管理器实例。
        /// </summary>
        private static readonly CacheManager _instance = new CacheManager();

        /// <summary>
        /// 当前缓存管理器。
        /// </summary>
        private ICacheManager _current = new DefaultCacheManager();

        /// <summary>
        /// 锁对象。
        /// </summary>
        private readonly static object _lockOjbect = new object();

        #endregion Members

        #region Properties

        /// <summary>
        /// 当前缓存管理器。
        /// </summary>
        public static ICacheManager Current
        {
            get
            {
                lock (_lockOjbect)
                {
                    return _instance.InnerCurrent;
                }
            }
        }

        /// <summary>
        /// 当前缓存管理器。
        /// </summary>
        private ICacheManager InnerCurrent
        {
            get
            {
                return _current;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// 设置当前使用的缓存方式。
        /// </summary>
        /// <param name="cacheManager">缓存管理器。</param>
        public static void SetCacheManager(ICacheManager cacheManager)
        {
            _instance.InnerSetCacheManager(cacheManager);
        }

        #endregion Public Methods

        #region Pricate Method

        /// <summary>
        /// 设置当前使用的缓存方式。
        /// </summary>
        /// <param name="cacheManager">缓存管理器。</param>
        private void InnerSetCacheManager(ICacheManager cacheManager)
        {
            if (cacheManager == null)
            {
                throw new ArgumentNullException("cacheManager");
            }

            _current = cacheManager;
        }

        #endregion Pricate Method
    }
}