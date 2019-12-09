using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WL.Infrastructure;
using WL.Infrastructure.Collections;

namespace WL.Infrastructure.Data
{
    public class BaseDAL
    {
        private static string _connstr = string.Empty;
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connstr))
                {
                    _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
                }
                return _connstr;
            }
        }
        public void GetConnectionString(string con)
        {
            _connstr = System.Configuration.ConfigurationManager.ConnectionStrings[con].ConnectionString;
        }
        public void SetConnectionString(string str)
        {
            _connstr = str;
        }
        public T Single<T>(string sql, object param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                var result = connect.Query<T>(sql, param).FirstOrDefault();
                return result;
            }
        }
        public List<T> GetList<T>(string sql, object param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                var list = connect.Query<T>(sql, param).ToList();
                return list;
            }
        }
        /// <summary>
        /// 调用分页存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IPageOfItems<T> GetListPage<T>(int pageSize, int pageIndex, DynamicParameters param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                List<T> rs = connect.Query<T>("ProcViewPager", param, null, true, null, CommandType.StoredProcedure).ToList();
                int totalCount = param.Get<int>("@recordTotal");//返回总页数
                return new PageOfItems<T>(rs, pageIndex, pageSize, totalCount);
            }
        }

        //yes no
        public bool Add(string sql, object param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                int rs = connect.Execute(sql, param);
                return rs == 1;
            }
        }

        //插入返回自增ID
        public int AddGetID(string sql, DynamicParameters param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                int rs = connect.Execute(sql, param);
                if (rs == 1)
                {
                    return param.Get<int>("@ID");
                }
                else
                {
                    return  -1;

                }
            }
        }
        public bool Update(string sql, object param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                int rs = connect.Execute(sql, param);
                return rs > 0;
            }
        }
        public bool Delete(string sql, object param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                int rs = connect.Execute(sql, param);
                return rs >= 1;
            }
        }
        public IDbConnection BuildConnection()
        {
            IDbConnection connect = new SqlConnection(ConnectionString);
            connect.Open();
            return connect;
        }
        public bool ExecuteStoredProcedureNonQuery(string name, DynamicParameters param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                int rs = connect.Execute(name, param, null, null, CommandType.StoredProcedure);
                return rs > 0;
            }
        }
        public List<T> ExecuteStoredProcedure<T>(string name, DynamicParameters param = null)
        {
            using (IDbConnection connect = BuildConnection())
            {
                List<T> rs = connect.Query<T>(name, param, null, true, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
        }
    }
}
