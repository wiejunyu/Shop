namespace WL.Infrastructure.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 分页集合。
    /// </summary>
    /// <typeparam name="TSource">对象类型。</typeparam>
    [Serializable]
    public class PageOfItems<TSource> : List<TSource>, IPageOfItems<TSource>
    {
        /// <summary>
        /// 页码。
        /// </summary>
        private int _pageIndex = 1;

        /// <summary>
        /// 页大小。
        /// </summary>
        private int _pageSize = 10;

        /// <summary>
        /// 总记录数。
        /// </summary>
        private int _totalItemCount = 0;

        /// <summary>
        /// 初始化。
        /// </summary>
        public PageOfItems()
            : this(null, 1, 10, 0)
        { }

        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="items">列表。</param>
        /// <param name="pageIndex">页码。</param>
        /// <param name="pageSize">页大小。</param>
        /// <param name="totalItemCount">总记录数。</param>
        public PageOfItems(IEnumerable<TSource> items, int pageIndex, int pageSize, int totalItemCount)
        {
            if (items != null)
                AddRange(items);

            //页码。
            this._pageIndex = pageIndex;

            //页大小。
            this._pageSize = pageSize;

            //总记录数。
            this._totalItemCount = totalItemCount;

            //计算总页数。
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

            //当前页数小于 1 时，设置当前页数是 1。
            if (this._pageIndex < 1)
            {
                this._pageIndex = 1;
            }

            //页大小小于 1 时，设置页大小是 10。
            if (this._pageSize < 1)
            {
                this._pageSize = 10;
            }

            if (this.TotalPageCount <= 0)
            {
                this.TotalPageCount = 1;
            }

            //当前页数大于最大页数时，设置当前页数等于最大页数。
            if (this._pageIndex > this.TotalPageCount)
            {
                this._pageIndex = this.TotalPageCount;
            }
        }

        #region IPageOfItems<T> Members

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return this._pageIndex; }
            set
            {
                if (value > 0)
                {
                    //当前页数大于最大页数时，设置当前页数等于最大页数。
                    if (value > this.TotalPageCount)
                    {
                        this._pageIndex = this.TotalPageCount;
                    }
                    else
                    {
                        this._pageIndex = value;
                    }
                }
                else
                {
                    this._pageIndex = 1;
                }
            }
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return this._pageSize; }
            set
            {
                if (value < 1)
                {
                    this._pageSize = 10;
                }
                else
                {
                    this._pageSize = value;
                }

                this.TotalPageCount = (int)Math.Ceiling(this._totalItemCount / (double)this._pageSize);
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalItemCount
        {
            get { return this._totalItemCount; }
            set
            {
                if (value >= 0)
                {
                    this._totalItemCount = value;
                    this.TotalPageCount = (int)Math.Ceiling(this._totalItemCount / (double)this._pageSize);
                }
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageCount { get; private set; }

        #endregion IPageOfItems<T> Members
    }
}