using WL.Cms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class MenuManager
    {
        #region 菜单管理
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <returns></returns>
        public static List<Menu> GetMenuList()
        {
            string sql = "Select * from Cms_Menu";
            return new BaseDAL().GetList<Menu>(sql, null);
        }
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <returns></returns>
        public static List<RoleMenu> GetRoleMenuList(string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoleID", id);
            string sql = "Select * from Cms_RoleMenu where RoleID=@RoleID";
            return new BaseDAL().GetList<RoleMenu>(sql, param);
        }
        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static Menu GetMenu(string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ID", id);
            string sql = "Select * from Cms_Menu where ID=@ID";
            return new BaseDAL().Single<Menu>(sql, param);
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateMenu(Menu temp)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Update Cms_Menu set Name=@Name,");
            sb.Append("Url=@Url,Action=@Action,Sort=@Sort,Lv=@Lv,Icon=@Icon,Pid=@Pid");
            sb.Append(" where ID=@ID");

            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", temp.Name);
            param.Add("@Url", temp.Url);
            param.Add("@Action", temp.Action);
            param.Add("@Sort", temp.Sort);
            param.Add("@Lv", temp.Lv);
            param.Add("@Icon", temp.Icon);
            param.Add("@Pid", temp.Pid);
            param.Add("@ID", temp.ID);
            #endregion

            return new BaseDAL().Update(sb.ToString(), param);
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int AddMenu(Menu temp)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Insert into Cms_Menu (");
            sb.Append("Name,Url,Action,Sort,Lv,Icon,Pid)");
            sb.Append(" values (");
            sb.Append("@Name,@Url,@Action,@Sort,@Lv,@Icon,@Pid);SELECT @ID=SCOPE_IDENTITY()");
            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", temp.Name);
            param.Add("@Url", temp.Url);
            param.Add("@Action", temp.Action);
            param.Add("@Sort", temp.Sort);
            param.Add("@Lv", temp.Lv);
            param.Add("@Icon", temp.Icon);
            param.Add("@Pid", temp.Pid);
            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            #endregion

            return new BaseDAL().AddGetID(sb.ToString(), param);
        }
        /// <summary>
        /// 添加菜单关联
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddRoleMenu(RoleMenu temp)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Insert into Cms_RoleMenu (");
            sb.Append("MenuID,RoleID)");
            sb.Append(" values (");
            sb.Append("@MenuID,@RoleID)");
            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoleID", temp.RoleID);
            param.Add("@MenuID", temp.MenuID);
            #endregion

            return new BaseDAL().Add(sb.ToString(), param);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static bool DeleteMenu(string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ID", id);
            string sql = "select id from Cms_Menu where Pid = @ID";
            List<Menu> list = new BaseDAL().GetList<Menu>(sql, param);
            //如果还有子菜单，继续递归
            if (list != null && list.Count > 0)
            {
                foreach (Menu m in list)
                {
                    DeleteMenu(m.ID.ToString());
                    DeleteRoleMenu(m.ID.ToString());
                }
            }
            //删除当前菜单
            param = new DynamicParameters();
            param.Add("@ID", id);
            sql = "Delete Cms_Menu where ID=@ID";
            return new BaseDAL().Delete(sql, param);
        }

        /// <summary>
        /// 删除菜单关联
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static bool DeleteRoleMenu(string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MenuID", id);
            string sql = "Delete Cms_RoleMenu where MenuID=@MenuID";

            return new BaseDAL().Delete(sql, param);
        }
        /// <summary>
        /// 删除菜单关联
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static bool DeleteRoleMenuByRoleID(string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoleID", id);
            string sql = "Delete Cms_RoleMenu where RoleID=@RoleID";

            return new BaseDAL().Delete(sql, param);
        }
        /// <summary>
        /// 查询菜单权限
        /// </summary>
        /// <returns></returns>
        public static List<Menu> GetMenuListByPermission(string permission)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Permission", permission);
            string sql = "Select * from Cms_RoleMenu where RoleID=@Permission";
            List<RoleMenu> list = new BaseDAL().GetList<RoleMenu>(sql, param);
            List<Menu> last = new List<Menu>();
            foreach (RoleMenu m in list)
            {
                param = new DynamicParameters();
                param.Add("@ID", m.MenuID);
                sql = "Select * from Cms_Menu where ID=@ID";
                Menu d = new BaseDAL().Single<Menu>(sql, param);
                last.Add(d);
            }
            return last;
        }
        #endregion
        #region 权限
        /// <summary>
        /// 查询权限;
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static Role GetRoleByID(string ID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ID", ID);
            string sql = "Select * from Cms_Role where ID=@ID";
            return new BaseDAL().Single<Role>(sql, param);
        }
        /// <summary>
        /// 查询权限;
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static Role GetRoleByName(string Name)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", Name);
            string sql = "Select * from Cms_Role where Name=@Name";
            return new BaseDAL().Single<Role>(sql, param);
        }
        /// <summary>
        /// 更新账号信息
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public static bool UpdateRole(Role Role)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Update Cms_Role set Name=@Name,");
            sb.Append(" Remark=@Remark");
            sb.Append(" where ID=@ID");

            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", Role.Name);

            param.Add("@Remark", Role.Remark);

            param.Add("@ID", Role.ID);
            #endregion

            return new BaseDAL().Update(sb.ToString(), param);
        }
        /// <summary>
        /// 添加新权限
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public static bool AddRole(Role Role)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Insert into Role (");
            sb.Append("Name,Remark)");
            sb.Append(" values (");
            sb.Append("@Name,@Remark)");
            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", Role.Name);

            param.Add("@Remark", Role.Remark);

            #endregion

            return new BaseDAL().Add(sb.ToString(), param);
        }
        /// <summary>
        /// 删除权限;
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static bool DeleteRole(string ID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ID", ID);
            string sql = "Delete Role where ID=@ID";

            return new BaseDAL().Delete(sql, param);
        }
        /// <summary>
        /// 查询权限列表
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetRoleList()
        {
            string sql = "Select * from Role";
            List<Role> list = new BaseDAL().GetList<Role>(sql, null);
            return list;
        }



        #endregion
    }
}
