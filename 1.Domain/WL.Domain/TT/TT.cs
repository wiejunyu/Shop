


using System.Collections.Generic;

namespace WL.Domain
{
	/// <summary>
    /// 数据库信息
    /// </summary>
    public static class DB
    {
	    /// <summary>
        /// 数据库名称
        /// </summary>
        private static string _DBName = "WL";

        /// <summary>
        /// 数据库名称
        /// </summary>
        public static string DBName { get { return _DBName; } }

		/// <summary>
        /// 数据库表名集合
        /// </summary>
        private static List<string> _TableName = new List<string>()
        {
			"Category",//栏目表
			"Cms_Article",//CMS文章表
			"Cms_Category",//CMS栏目表
			"Cms_Logger",//CMS日志表
			"Cms_Menu",//CMS菜单表
			"Cms_Role",//CMS角色表
			"Cms_RoleMenu",//角色按钮关联表
			"Cms_UserInfo",//CMS用户表
			"Code",//验证码表
			"ExceptionLog",//系统异常记录
			"Hot",//商品热门表
			"Logger",//日志表
			"Order",//订单表
			"Shop",//商品表
			"User",//用户表
			"UserDetails",//用户详情表
		};

		/// <summary>
        /// 获取数据库表名集合
        /// </summary>
        public static List<string> GetTableName() 
		{
            return _TableName;
        }
    }
}