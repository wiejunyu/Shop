using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WL.Infrastructure.Common;
using WL.Cms.Models;
using WL.Cms.Manager;

namespace WL.Web.Cms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write(postTest());
        }

        public static void xml()
        {
        }

        public static string postTest()
        {
            ////https://www.ggfx.com 读取开始时
            string type = "get_data";
            string table_name = "";
            string sign = "";
            string url = "";
            string from = "";
            string json = "";

            //article
            //table_name = "article";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            List<Article> Article = JsonConvert.DeserializeObject<List<Article>>(json);
            foreach (Article temp in Article)
            {
                try
                {
                    TestManager.SetArticle(temp);
                }
                catch (Exception ex)
                {
                    return "article" + ex.Message;
                }
            }

            ////financial_calendar
            //table_name = "financial_calendar";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<testFinancial_Calendar> testFinancial_Calendar = JsonConvert.DeserializeObject<List<testFinancial_Calendar>>(json);
            //foreach (testFinancial_Calendar temp in testFinancial_Calendar)
            //{
            //    try
            //    {
            //        TestManager.SetTestFinancial_Calendar(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "financial_calendar" + ex.Message;
            //    }
            //}

            ////register_source
            //table_name = "register_source";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<test_registration_source> test_registration_source = JsonConvert.DeserializeObject<List<test_registration_source>>(json);
            //int a = test_registration_source.Count;
            //foreach (test_registration_source temp in test_registration_source)
            //{
            //    try
            //    {
            //        TestManager.SetTest_registration_source(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "register_source" + ex.Message;
            //    }
            //}

            ////bonus_list
            //table_name = "bonus_list";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<BonusList> BonusList = JsonConvert.DeserializeObject<List<BonusList>>(json);
            //foreach (BonusList temp in BonusList)
            //{
            //    try
            //    {
            //        TestManager.SetBonusList(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "bonus_list" + ex.Message;
            //    }
            //}

            ////bonus
            //table_name = "bonus";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<Bonus> Bonus = JsonConvert.DeserializeObject<List<Bonus>>(json);
            //foreach (Bonus temp in Bonus)
            //{
            //    try
            //    {
            //        TestManager.SetBonus(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "bonus" + ex.Message;
            //    }
            //}

            ////invite
            //table_name = "invite";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<Invite> Invite = JsonConvert.DeserializeObject<List<Invite>>(json);
            //foreach (Invite temp in Invite)
            //{
            //    try
            //    {
            //        TestManager.SetInvite(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "invite" + ex.Message;
            //    }
            //}

            ////invite_detail
            //table_name = "invite_detail";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://test.ggfx.com/api";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<InviteDetail> InviteDetail = JsonConvert.DeserializeObject<List<InviteDetail>>(json);
            //foreach (InviteDetail temp in InviteDetail)
            //{
            //    try
            //    {
            //        TestManager.SetInviteDetail(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "invite_detail" + ex.Message;
            //    }
            //}


            //////https://www.ggfx.com 读取结束

            ////// http://admin.ggfx.com 读取开始

            ////mt_users
            //table_name = "mt_users";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://admin.ggfx.com/api.php";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<testMt_User> testMt_User = JsonConvert.DeserializeObject<List<testMt_User>>(json);
            //foreach (testMt_User temp in testMt_User)
            //{
            //    try
            //    {
            //        if (TestManager.GetTestMt_User(temp) != 0)
            //        {
            //            TestManager.UpdateTestMt_User(temp);
            //        }
            //        else
            //        {
            //            TestManager.SetTestMt_User(temp);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return "mt_users" + ex.Message;
            //    }
            //}

            ////mt_users_demo
            //table_name = "mt_users_demo";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://admin.ggfx.com/api.php";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<testMt_User_Demo> testMt_User_Demo = JsonConvert.DeserializeObject<List<testMt_User_Demo>>(json);
            //foreach (testMt_User_Demo temp in testMt_User_Demo)
            //{
            //    try
            //    {
            //        if (TestManager.GetTestMt_User_Demo(temp) != 0)
            //        {
            //            TestManager.UpdateTestMt_User_Demo(temp);
            //        }
            //        else
            //        {
            //            TestManager.SetTestMt_User_Demo(temp);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return "mt_users_demo" + ex.Message;
            //    }
            //}

            ////deposit
            //table_name = "deposit";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://admin.ggfx.com/api.php";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<testDeposit> testDeposit = JsonConvert.DeserializeObject<List<testDeposit>>(json);
            //foreach (testDeposit temp in testDeposit)
            //{
            //    try
            //    {
            //        TestManager.SetTestDeposit(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "deposit" + ex.Message;
            //    }
            //}

            ////withdrawals
            //table_name = "withdrawals";
            //sign = MD5.Md5UTF8(type + table_name + mt.key).ToLower();
            //url = "http://admin.ggfx.com/api.php";
            //from = "type=" + type + "&table_name=" + table_name + "&sign=" + sign;
            //json = PostWebRequest(url, from);
            //List<testWithdrawals> testWithdrawals = JsonConvert.DeserializeObject<List<testWithdrawals>>(json);
            //foreach (testWithdrawals temp in testWithdrawals)
            //{
            //    try
            //    {
            //        TestManager.SetTestWithdrawals(temp);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "withdrawals" + ex.Message;
            //    }
            //}



            return "OK";
        }
        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="postUrl">URL</param>
        /// <param name="paramData">参数</param>
        /// <returns></returns>
        public static string PostWebRequest(string postUrl, string paramData)
        {
            int a = 1;
            string result = "";
            while (a == 1)
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(postUrl);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";

                    byte[] data = Encoding.UTF8.GetBytes(paramData);
                    req.ContentLength = data.Length;
                    using (Stream reqStream = req.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }

                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                    a = 0;
                }
                catch
                {
                    a = 1;
                }
            }
            return result;
        }
    }
}