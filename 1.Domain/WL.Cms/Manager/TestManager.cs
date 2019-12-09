using WL.Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class TestManager
    {
        public static void SetArticle(Article temp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", temp.id);
            param.Add("@catid", temp.catid);
            param.Add("@userid", temp.userid);
            param.Add("@username", temp.username);
            param.Add("@title", temp.title);
            param.Add("@keywords", temp.keywords);
            param.Add("@description", temp.description);
            param.Add("@content", temp.content);
            param.Add("@thumb", temp.thumb);
            param.Add("@listorder", temp.listorder);
            param.Add("@url", temp.url);
            param.Add("@hits", temp.hits);
            param.Add("@createtime", temp.createtime);
            param.Add("@updatetime", temp.updatetime);
            param.Add("@lang", temp.lang);
            string sql = "INSERT INTO Article VALUES (@id,@catid,@userid,@username,@title,@keywords,@description,@content,@thumb,@listorder,@url,@hits,@createtime,@updatetime,@lang)";
            new BaseDAL().Add(sql, param);
        }

        //public static void SetBonus(Bonus temp)
        //{
        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@id", temp.id);
        //    param.Add("@name", temp.name);
        //    param.Add("@phone", temp.phone);
        //    param.Add("@email", temp.email);
        //    param.Add("@state", temp.state);
        //    param.Add("@date", temp.date);
        //    string sql = "INSERT INTO Bonus VALUES (@id,@name,@phone,@email,@state,@date)";
        //    new BaseDAL().Add(sql, param);
        //}
    }
}
