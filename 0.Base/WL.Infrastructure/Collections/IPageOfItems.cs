namespace WL.Infrastructure.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// 分页集合接口
    /// </summary>
    public interface IPageOfItems<out T> : IEnumerable<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// 页大小
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPageCount { get; }

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalItemCount { get; }
    }
}