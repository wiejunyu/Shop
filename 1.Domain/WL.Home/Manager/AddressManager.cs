using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Home.Models;
using WL.Infrastructure.Data;
using System.Web;
using Newtonsoft.Json;
using WL.Domain;

namespace WL.Home.Manager
{
    public class AddressManager
    {
        /// <summary>
        /// 写入收货地址
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool SetAddress(AddressModels Address)
        {
            using (WLDbContext db = new WLDbContext())
            {
                UserModels user = HttpContext.Current.Session["user"] as UserModels;
                UserDetails userDetails = db.UserDetails.Single(x => x.UID == user.ID);
                if (!string.IsNullOrWhiteSpace(userDetails.province))
                {
                    List<AddressModels> list = JsonConvert.DeserializeObject<List<AddressModels>>(userDetails.province);
                    list.Add(Address);
                    string json = JsonConvert.SerializeObject(list);
                    userDetails.province = json;
                    db.SaveChanges();
                }
                else
                {
                    Address.Choice = true;
                    List<AddressModels> list = new List<AddressModels>();
                    list.Add(Address);
                    string json = JsonConvert.SerializeObject(list);
                    userDetails.province = json;
                    db.SaveChanges();
                }
                return true;
            }
        }

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <returns></returns>
        public static List<AddressModels> GetAddress()
        {
            using (WLDbContext db = new WLDbContext())
            {
                UserModels user = HttpContext.Current.Session["user"] as UserModels;
                UserDetails userDetails = db.UserDetails.Single(x => x.UID == user.ID);
                List<AddressModels> list = new List<AddressModels>();
                if (!string.IsNullOrWhiteSpace(userDetails.province))
                {
                    list = JsonConvert.DeserializeObject<List<AddressModels>>(userDetails.province);
                }
                return list;
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <returns></returns>
        public static bool DelAddress(int date)
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", user.ID);
            string sql = "Select address from UserDetails where id = @id";
            string json = new BaseDAL().Single<string>(sql, param);
            List<AddressModels> list = JsonConvert.DeserializeObject<List<AddressModels>>(json);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].date == date)
                {
                    if (list[i].Choice)
                    {
                        list.RemoveAt(i);
                        if (list.Count != 0)
                        {
                            int d = list.Min(p => p.date);
                            foreach (AddressModels temp in list)
                            {
                                if (temp.date == d)
                                {
                                    temp.Choice = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        list.RemoveAt(i);
                    }
                }
            }
            if (list.Count == 0)
            {
                sql = "UPDATE UserDetails SET address = null WHERE id = @id";
                return new BaseDAL().Update(sql, param);
            }
            json = JsonConvert.SerializeObject(list);
            param = new DynamicParameters();
            param.Add("@id", user.ID);
            param.Add("@address", json);
            sql = "UPDATE UserDetails SET address = @address WHERE id = @id";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool EditAddress(AddressModels Address)
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", user.ID);
            string sql = "Select address from UserDetails where id = @id";
            string json = new BaseDAL().Single<string>(sql, param);
            if (json != null && json != "")
            {
                List<AddressModels> list = JsonConvert.DeserializeObject<List<AddressModels>>(json);
                foreach (AddressModels temp in list) {
                    if (temp.date == Address.date) {
                        temp.province = Address.province;
                        temp.city = Address.city;
                        temp.district = Address.district;
                        temp.address = Address.address;
                        temp.name = Address.name;
                        temp.tel = Address.tel;
                    }
                }
                json = JsonConvert.SerializeObject(list);
                param = new DynamicParameters();
                param.Add("@id", user.ID);
                param.Add("@address", json);
                sql = "UPDATE UserDetails SET address = @address WHERE id = @id";
                return new BaseDAL().Update(sql, param);
            }
            return false;
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <returns></returns>
        public static bool Choice(int date)
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;

            DynamicParameters param = new DynamicParameters();
            param.Add("@id", user.ID);
            string sql = "Select address from UserDetails where id = @id";
            string json = new BaseDAL().Single<string>(sql, param);
            List<AddressModels> list = JsonConvert.DeserializeObject<List<AddressModels>>(json);
            foreach (AddressModels temp in list)
            {
                temp.Choice = false;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].date == date)
                {
                    list[i].Choice = true;
                }
            }
            json = JsonConvert.SerializeObject(list);
            param = new DynamicParameters();
            param.Add("@id", user.ID);
            param.Add("@address", json);
            sql = "UPDATE UserDetails SET address = @address WHERE id = @id";
            return new BaseDAL().Update(sql, param);
        }


    }
}
