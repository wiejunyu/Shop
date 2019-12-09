using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Cms.Manager;
using WL.Cms.Models;
using WL.Home.Models;
using WL.Infrastructure.Common;

namespace WL.Web.Cms.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Shop

        public ActionResult AddShopColumn()
        {
            return View();
        }

        public ActionResult ViewShopColumn()
        {
            return View();
        }

        public ActionResult AddShop()
        {
            List<CategoryModels> list = ShopManager.RankGetShopColumuAll();
            ViewData["list"] = list;
            return View();
        }

        public ActionResult EditShop(int sid)
        {
            List<CategoryModels> list = ShopManager.RankGetShopColumuAll();
            ViewData["list"] = list;
            ShopModels shop = ShopManager.AidGetShop(sid);
            return View(shop);
        }

        public ActionResult ViewShop()
        {
            List<CategoryModels> list = ShopManager.RankGetShopColumuAll();
            ViewData["list"] = list;
            return View();
        }

        public ActionResult EditShopColumn(int cid)
        {
            List<CategoryModels> list = ShopManager.RankGetShopColumuAll();
            ViewData["list"] = list;
            CategoryModels cat = ShopManager.CidGetShopColumu(cid);
            return View(cat);
        }

        //写入商品
        [ValidateInput(false)]
        public JsonResult SetShop()
        {
            ShopModels shop = new ShopModels();

            string json = null;

            //添加栏目ID（Catid）
            shop.Catid = Convert.ToInt32(Request.Form["Catid"]);

            //添加名称（Name）
            shop.Name = Request.Form["Name"];

            //添加计量单位（Units）
            shop.Units = Request.Form["Units"];

            //添加品牌（Brand）
            shop.Brand = Request.Form["Brand"];

            //添加优惠价（Trueprice）
            shop.Trueprice = Convert.ToDecimal(Request.Form["Trueprice"]);

            //添加市场价（Price）
            shop.Price = Convert.ToDecimal(Request.Form["Price"]);

            //添加详细介绍（Body）
            if(Request.Form["Body"] != null)
                shop.Body = Request.Form["Body"].Replace("<p></p>", "");

            //添加型号（Model）
            shop.Model = Request.Form["Model"];

            //添加行业（Vocation）
            shop.Vocation = Request.Form["Vocation"];

            //添加是否推荐（Recommend）
            if (Convert.ToInt32(Request.Form["Recommend"]) == 1)
            {
                shop.Recommend = true;
            }

            //添加缩略图（Image）
            HttpPostedFileBase Picture = Request.Files["Image"];
            string Picture_url = "";
            if (Picture != null && Picture.ContentLength > 0)
            {
                Picture_url = Upload(Picture);
            }
            shop.Image = CommonModels.GetCmsWww() + Picture_url;

            //添加备注（Remarks）
            shop.Remarks = Request.Form["Remarks"];

            //添加参数（Parameter）
            json = Request.Form["Parameter"];
            if (json != "")
            {
                List<ParameterModels> list = new List<ParameterModels>();
                list = JsonConvert.DeserializeObject<List<ParameterModels>>(json);
                if (list.Count != 0)
                {
                    list = (from str in list where str != null select str).ToList();
                    json = JsonConvert.SerializeObject(list);
                }
            }
            shop.Parameter = json;
            json = null;

            //添加品牌证书（Certificate）
            if (Request.Form["Certificate"] != null)
                shop.Certificate = Request.Form["Certificate"].Replace("<p></p>", "");

            //添加高清图（HighImage）
            json = Request.Form["HighImage"];
            if (json != "")
            {
                List<ParameterModels> list = new List<ParameterModels>();
                list = JsonConvert.DeserializeObject<List<ParameterModels>>(json);
                list = (from str in list where str != null select str).ToList();
                foreach (ParameterModels Temp in list)
                {
                    Temp.ParameterData = CommonModels.GetCmsWww() + Temp.ParameterData.Replace("\\", "/");
                }
                List<string> arr = list.Select(p => p.ParameterData).ToList();
                json = JsonConvert.SerializeObject(arr);
            }
            shop.HighImage = json;

            //添加时间
            shop.Uptime = DateTime.Now;

            //写入商品
            json = null;
            if (ShopManager.SetShop(shop))
            {
                json = "1";
            }
            else
            {
                json = "-1";
            }
            return Json(json);
        }

        //按栏目级别获取商品栏目
        public string RankGetShopColumu(int Rank)
        {
            List<CategoryModels> list = ShopManager.RankGetShopColumu(Rank);
            string str = null;
            foreach (CategoryModels temp in list)
            {
                str += "<option value=\"" + temp.id + "\">" + temp.Name + "</option>";
            }
            return str;
        }

        //写入商品栏目
        public JsonResult SetShopColumu()
        {
            HttpPostedFileBase Picture = Request.Files["Picture"];
            string Picture_url = "";

            //获取缩略图
            if (Picture != null && Picture.ContentLength > 0)
            {
                Picture_url = CommonModels.GetCmsWww() + Upload(Picture);
            }
            CategoryModels cat = new CategoryModels();
            
            cat.Name = Request.Form["Name"];
            cat.Rank = Convert.ToInt32(Request.Form["Rank"]);
            cat.Picture = Picture_url;
            cat.Url = Request.Form["Url"];
            cat.Parentid = Convert.ToInt32(Request.Form["Parentid"]);
            if (Convert.ToInt32(Request.Form["Recommend"]) == 0)
            {
                cat.Recommend = false;
            }
            else
            {
                cat.Recommend = true;
            }
            cat.Click = Convert.ToInt32(Request.Form["Click"]);
            if (ShopManager.SetShopColumu(cat))
            {
                return Json("1");
            }
            else
            {
                return Json("插入库失败");
            }
        }

        //更新商品栏目
        public JsonResult UpdateShopColumu()
        {
            CategoryModels cat = new CategoryModels();

            cat.id = Convert.ToInt32(Request.Form["id"]);
            cat.Name = Request.Form["Name"];
            cat.Rank = Convert.ToInt32(Request.Form["Rank"]);
            
            cat.Url = Request.Form["Url"];
            cat.Parentid = Convert.ToInt32(Request.Form["Parentid"]);
            if (Convert.ToInt32(Request.Form["Recommend"]) == 0)
            {
                cat.Recommend = false;
            }
            else
            {
                cat.Recommend = true;
            }

            //获取缩略图
            HttpPostedFileBase Picture = Request.Files["Picture"];
            string Picture_url = "";
            if (Picture != null && Picture.ContentLength > 0)
            {
                Picture_url = CommonModels.GetCmsWww() + Upload(Picture);
            }
            else
            {
                Picture_url = ShopManager.GetShopColumuImage(cat.id);
            }
            cat.Picture = Picture_url;

            cat.Click = Convert.ToInt32(Request.Form["Click"]);
            if (ShopManager.UpdateShopColumu(cat))
            {
                return Json("1");
            }
            else
            {
                return Json("插入库失败");
            }
        }

        //按栏目ID获取商品栏目
        public JsonResult ColumuIdGetShopList(int cid)
        {
            List<ShopModels> list = ShopManager.ColumuIdGetShopList(cid);
            foreach (ShopModels temp in list)
            {
                temp.Remarks = CommonModels.GetHomeWww() + "Shop/Product?id=" + temp.id.ToString();
                temp.Certificate = temp.Uptime.ToString("yyyy-MM-dd hh:mm:ss");
            }
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }

        //按条件获取商品栏目
        public JsonResult TimeGetShopList(string st,string et)
        {
            List<ShopModels> list = ShopManager.ColumuIdGetShopList(0);
            list = list.Where(p => p.Uptime >= DateTime.Parse(st + " 00:00:00")).ToList();
            list = list.Where(p => p.Uptime <= DateTime.Parse(et + " 23:59:59")).ToList();
            foreach (ShopModels temp in list)
            {
                temp.Remarks = CommonModels.GetHomeWww() + "Shop/Product?id=" + temp.id.ToString();
                temp.Certificate = temp.Uptime.ToString("yyyy-MM-dd hh:mm:ss");
            }
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }

        //按级别获取商品栏目列表
        public JsonResult RankGetShopColumuList(int cid)
        {
            List<CategoryModels> list = ShopManager.RankGetShopColumu(cid);
            List<CategoryModels> listAll = ShopManager.RankGetShopColumuAll();
            
            foreach (CategoryModels temp in list)
            {
                temp.Url = CommonModels.GetHomeWww() + "Shop/List?id=" + temp.id.ToString();
                if (temp.Parentid != 0)
                {
                    foreach (CategoryModels tempAll in listAll)
                    {
                        if (temp.Parentid == tempAll.id)
                        {
                            temp.Picture = tempAll.Name;
                        }
                    }
                }
                else
                {
                    temp.Picture = "当前为顶级栏目";
                }
            }
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }

        //按父栏目获取商品栏目列表
        public JsonResult ParentidGetShopColumuList(int cid)
        {
            List<CategoryModels> list = ShopManager.ParentidGetShopColumu(cid);
            List<CategoryModels> listAll = ShopManager.RankGetShopColumuAll();

            foreach (CategoryModels temp in list)
            {
                temp.Url = CommonModels.GetHomeWww() + "Shop/List?id=" + temp.id.ToString();
                foreach (CategoryModels tempAll in listAll)
                {
                    if (temp.Parentid == tempAll.id)
                    {
                        temp.Picture = tempAll.Name;
                    }
                }
            }
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }


        //删除商品栏目
        public JsonResult ShopColumuDelete(int cid)
        {
            int rel = -1;
            if (ShopManager.DelShopColumu(cid))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        //批量删除商品栏目
        public JsonResult ColumuBatchDel(string cid)
        {
            List<string> list = JsonConvert.DeserializeObject<List<string>>(cid);
            int rel = 0;
            foreach (string temp in list)
            {
                if (ShopManager.DelShopColumu(Convert.ToInt32(temp)) == false)
                {
                    rel++;
                }
            }
            return Json(rel);
        }

        //删除商品
        public JsonResult DelShop(int sid)
        {
            int rel = -1;
            if (ShopManager.DelShop(sid))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        //更新商品
        [ValidateInput(false)]
        public JsonResult UpdateShop()
        {
            ShopModels shop = new ShopModels();

            string json = null;
            //添加商品ID（id）
            shop.id = Convert.ToInt32(Request.Form["id"]);

            //添加栏目ID（Catid）
            shop.Catid = Convert.ToInt32(Request.Form["Catid"]);

            //添加名称（Name）
            shop.Name = Request.Form["Name"];

            //添加计量单位（Units）
            shop.Units = Request.Form["Units"];

            //添加品牌（Brand）
            shop.Brand = Request.Form["Brand"];

            //添加优惠价（Trueprice）
            shop.Trueprice = Convert.ToDecimal(Request.Form["Trueprice"]);

            //添加市场价（Price）
            shop.Price = Convert.ToDecimal(Request.Form["Price"]);

            //添加详细介绍（Body）
            if (Request.Form["Body"] != null)
                shop.Body = Request.Form["Body"].Replace("<p></p>", "");

            //添加型号（Model）
            shop.Model = Request.Form["Model"];

            //添加行业（Vocation）
            shop.Vocation = Request.Form["Vocation"];

            //添加是否推荐（Recommend）
            if (Convert.ToInt32(Request.Form["Recommend"]) == 1)
            {
                shop.Recommend = true;
            }

            //添加缩略图（Image）
            HttpPostedFileBase Picture = Request.Files["Image"];
            string Picture_url = "";
            if (Picture != null && Picture.ContentLength > 0)
            {
                Picture_url = Upload(Picture);
                shop.Image = CommonModels.GetCmsWww() + Picture_url;
            }
            else
            {
                Picture_url = ShopManager.GetShopImage(shop.id);
                shop.Image = Picture_url;
            }

            //添加备注（Remarks）
            shop.Remarks = Request.Form["Remarks"];

            //添加参数（Parameter）
            json = Request.Form["Parameter"];
            if (json != "")
            {
                List<ParameterModels> list = new List<ParameterModels>();
                list = JsonConvert.DeserializeObject<List<ParameterModels>>(json);
                if (list.Count != 0)
                {
                    list = (from str in list where str != null select str).ToList();
                    json = JsonConvert.SerializeObject(list);
                }
            }
            shop.Parameter = json;
            json = null;

            //添加品牌证书（Certificate）
            if (Request.Form["Certificate"] != null)
                shop.Certificate = Request.Form["Certificate"].Replace("<p></p>", "");

            //添加高清图（HighImage）
            json = Request.Form["HighImage"];
            if (json != "")
            {
                List<ParameterModels> list = new List<ParameterModels>();
                list = JsonConvert.DeserializeObject<List<ParameterModels>>(json);
                list = (from str in list where str != null select str).ToList();
                foreach (ParameterModels Temp in list)
                {
                    if (Temp.ParameterData.Length >= 7 && Temp.ParameterData.Substring(0, 7) == "http://")
                    {
                        Temp.ParameterData = Temp.ParameterData.Replace("\\", "/");
                    }
                    else
                    {
                        Temp.ParameterData = CommonModels.GetCmsWww() + Temp.ParameterData.Replace("\\", "/");
                    }
                }
                List<string> arr = list.Select(p => p.ParameterData).ToList();
                json = JsonConvert.SerializeObject(arr);
            }
            shop.HighImage = json;

            //添加时间
            shop.Uptime = DateTime.Now;

            if (ShopManager.UpdateShop(shop))
            {
                return Json("1");
            }
            else
            {
                return Json("-1");
            }
        }

        //批量删除商品
        public JsonResult BatchDel(string sid)
        {
            List<string> list = JsonConvert.DeserializeObject<List<string>>(sid);
            int rel = 0;
            foreach (string temp in list)
            {
                if (ShopManager.DelShop(Convert.ToInt32(temp)) == false)
                {
                    rel++;
                }
            }
            return Json(rel);
        }

        //批量修改商品到栏目
        public JsonResult BatchMove(string sid, string cid)
        {
            int rel = 0;
            List<string> list = JsonConvert.DeserializeObject<List<string>>(sid);
            foreach (string temp in list) {
                if (ShopManager.UpdateShopCatid(Convert.ToInt32(temp), Convert.ToInt32(cid)) == false)
                {
                    rel++;
                }
            }
            return Json(rel);
        }

        [HttpPost]
        public ActionResult UpLoadProcess(HttpPostedFileBase file)
        {
            //保存到临时文件夹  
            string filePathName = string.Empty;
            string localPath = "Uploads\\images\\" + DateTime.Now.ToString("yyyyMMdd");
            string localPathAll = HttpRuntime.AppDomainAppPath + localPath;
            if (Request.Files.Count == 0)
            {
                return Json(new { status = 0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPathAll))
            {
                System.IO.Directory.CreateDirectory(localPathAll);
            }
            file.SaveAs(Path.Combine(localPathAll, filePathName));

            return Json(new
            {
                status = 1,
                filePath = "\\" + localPath + "\\" + filePathName
            });
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string Upload(HttpPostedFileBase file, string name = null)
        {
            string time = DateTime.Now.ToString("yyyyMMdd");
            string sFileRoot = Server.MapPath("~/Uploads/images/" + time);
            string sFileName = Common.FileUploader(sFileRoot, file, name);
            return "/Uploads/images/" + time + "/" + sFileName;
        }
    }
}