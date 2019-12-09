using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Cms.Models
{
    ///权限详细
    public class Menu
    {
        //ID
        public int ID { get; set; }
        //名称
        public string Name { get; set; }
        //URL
        public string Url { get; set; }
        //Name
        public string Action { get; set; }
        //排序
        public int Sort { get; set; }
        //关联节点位置0=根节点，1=页面，2=页面功能类
        public int Lv { get; set; }
        //图标
        public string Icon { get; set; }
        //父id
        public int Pid { get; set; }

        public Menu()
        {
            ID = 0;
            Name = "";
            Url = "";
            Action = "";
            Sort = -1;
            Lv = -1;
            Icon = "";
            Pid = 0;
        }
    }
    ///权限关联
    public class RoleMenu
    {
        //ID
        public int ID { get; set; }
        //关联Menu
        public int MenuID { get; set; }
        //关联Role
        public int RoleID { get; set; }

        public RoleMenu()
        {
            ID = 0;
            MenuID = 0;
            RoleID = 0;
        }
    }
    //角色
    public class Role
    {
        //ID
        public int ID { get; set; }
        //名称
        public string Name { get; set; }
        //备注
        public string Remark { get; set; }


        public Role()
        {
            ID = 0;
            Name = "";
            Remark = "";
        }
    }

    ///权限关联
    public class MenuTreeTop
    {
        public int id { get; set; }
        public string text { get; set; }
        public MenuTreeState state { get; set; }

        public List<MenuTree> children { get; set; }

        public MenuTreeTop()
        {
            id = 0;
            text = "";
            state = new MenuTreeState();
            children = new List<MenuTree>();
        }
    }
    public class MenuTreeState
    {
        public bool opened { get; set; }

        public MenuTreeState()
        {
            opened = true;
        }
    }

    public class MenuTree
    {
        public int id { get; set; }
        public string text { get; set; }
        public MenuState state { get; set; }

        public List<MenuBtnTree> children { get; set; }

        public MenuTree()
        {
            id = 0;
            text = "";
            state = new MenuState();
            children = new List<MenuBtnTree>();
        }
    }

    public class MenuState
    {
        public bool opened { get; set; }

        public MenuState()
        {
            opened = true;
        }
    }
    public class MenuBtnTree
    {
        public int id { get; set; }
        public string text { get; set; }
        public MenuBtnState state { get; set; }

        public MenuBtnTree()
        {
            id = 0;
            text = "";
            state = new MenuBtnState();
        }
    }

    public class MenuBtnState
    {
        public bool selected { get; set; }

        public MenuBtnState()
        {
            selected = false;
        }
    }
}
