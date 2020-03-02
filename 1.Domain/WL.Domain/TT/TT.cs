


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
			"Category",//Category
			"Cms_Article",//Cms_Article
			"Cms_Category",//Cms_Category
			"Cms_Logger",//Cms_Logger
			"Cms_Menu",//Cms_Menu
			"Cms_Role",//Cms_Role
			"Cms_RoleMenu",//Cms_RoleMenu
			"Cms_UserInfo",//Cms_UserInfo
			"Code",//Code
			"Hot",//Hot
			"Logger",//Logger
			"Order",//Order
			"Shop",//Shop
			"test",//test
			"User",//User
			"UserDetails",//UserDetails
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