using WL.Infrastructure.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WL.Infrastructure.Common
{
    public class Common
    {
        private static int seed = 1;
        private static int key_seed = 1;
        private static int noseed = 1;
        private static int domseed = 1;
        /// <summary> 
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址 
        /// </summary> 
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;

                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理; 
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式; 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。; 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址 ;
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式; 
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP ;
                    }

                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];



                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                if (result == "::1")
                {
                    result = "127.0.0.1";
                }
                return result;
            }
        }
        /**/
        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^/d{1,3}[/.]/d{1,3}[/.]/d{1,3}[/.]/d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
        /// <summary>
        /// 时间转换时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertDateTimeInt(string time)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Parse(time);
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }
        /// <summary>
        /// 时间戳转换时间
        /// </summary>
        /// <param name="datestr"></param>
        /// <returns></returns>
        public static DateTime ConvertDate(string datestr)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(datestr + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertDateTimeInt_flot(string time)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Parse(time);
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 4);
            return timeStamp;
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="_rootPath"></param>
        /// <param name="_file"></param>
        /// <param name="_filename"></param>
        /// <param name="type_s"></param>
        /// <returns></returns>
        public static string FileUploader(string _rootPath, HttpPostedFileBase _file, string _filename = null, string type_s = "")
        {
            string sFileName = _filename;
            if (_file != null)
            {
                string _filePath = _file.FileName;
                if (_filePath != string.Empty)
                {
                    string _fileType = _filePath.Substring(_filePath.LastIndexOf("."));
                    string sFileRoot = _rootPath;
                    if (!System.IO.Directory.Exists(sFileRoot))
                        System.IO.Directory.CreateDirectory(sFileRoot);
                    if (sFileName == null)
                    {
                        if (type_s != "")
                        {
                            sFileName = DateTime.Now.ToString("yyyyMMddHHmmssms") + type_s;
                        }
                        else
                        {
                            sFileName = DateTime.Now.ToString("yyyyMMddHHmmssms") + _fileType;
                        }
                    }
                    else
                    {
                        if (type_s != "")
                        {
                            sFileName = sFileName + type_s;
                        }
                        else
                        {
                            sFileName = sFileName + _fileType;
                        }
                    }
                    string sFilePath = sFileRoot + "\\" + sFileName;
                    _file.SaveAs(sFilePath);
                }
            }
            return sFileName;
        }
        /// <summary>
        /// 根据路径把文件转换成数据流
        /// </summary>
        /// <param name="strpath"></param>
        /// <returns></returns>
        public static byte[] Returnbyte(string strpath)
        {
            // 以二进制方式读文件
            FileStream fsMyfile = new FileStream(strpath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            // 创建一个二进制数据流读入器，和打开的文件关联
            BinaryReader brMyfile = new BinaryReader(fsMyfile);
            // 把文件指针重新定位到文件的开始
            brMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = brMyfile.ReadBytes(Convert.ToInt32(fsMyfile.Length.ToString()));
            // 关闭以上new的各个对象
            brMyfile.Close();
            return bytes;
        }
        public static void DeleteFile(string filepatch)
        {
            FileInfo file = new FileInfo(filepatch);//指定文件路径
            if (file.Exists)//判断文件是否存在
            {
                file.Attributes = FileAttributes.Normal;//将文件属性设置为普通,比方说只读文件设置为普通
                file.Delete();//删除文件
            }
        }
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        private DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }
        /**//// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        private DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }

        /**//// <summary>
        /// 取得上个月第一天
        /// </summary>
        /// <param name="datetime">要取得上个月第一天的当前时间</param>
        /// <returns></returns>
        public DateTime FirstDayOfPreviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1);
        }

        /**//// <summary>
        /// 取得上个月的最后一天
        /// </summary>
        /// <param name="datetime">要取得上个月最后一天的当前时间</param>
        /// <returns></returns>
        public DateTime LastDayOfPrdviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddDays(-1);
        }


        /// <summary>
        /// 判断字符串是否为正整数
        /// </summary>
        /// <param name="str">要判断的字符串对象</param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            bool isInt = false;
            if (!string.IsNullOrEmpty(str))
            {
                isInt = Regex.IsMatch(str, @"^(0|([1-9]\d*))$");
            }
            return isInt;
        }

        /// <summary>
        /// 判断是否为DateTime
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 生成单号
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static string GetOrderNo()
        {
            if (seed == int.MaxValue)
            {
                seed = 1;
            }
            seed++;
            string tbout_trade_no = "";
            string guid = Guid.NewGuid().ToString();
            string last = guid.Replace("-", "");
            char[] cc = last.ToCharArray();
            StringBuilder sb = new StringBuilder(4);
            Random rd = new Random((unchecked((int)DateTime.Now.Ticks + seed)));
            for (int i = 0; i < 6; i++)
            {
                sb.Append(cc[rd.Next(cc.Length)]);
            }

            tbout_trade_no = sb + "-" + DateTime.Now.ToString("yyyyMMddHHmmssff");
            return tbout_trade_no;
        }

        /// <summary>
        /// 生成密匙
        /// </summary>
        /// <param name="mch_id"></param>
        /// <returns></returns>
        public static string GetGGAPIKey(string num_id)
        {
            if (key_seed == int.MaxValue)
            {
                key_seed = 1;
            }
            key_seed++;
            string key = "";
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(4);
            for (int i = 0; i < 5; i++)
            {
                Random rd = new Random((unchecked((int)DateTime.Now.Ticks + i)));
                sb.Append(constant[rd.Next(62)]);
            }
            key = MD5.Md5UTF8(num_id + sb).ToLower();
            return key;
        }

        /// <summary>
        /// PostUTF8格式的JSON
        /// </summary>
        /// <param name="json"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostUtf8Json(string json, string url, int timeout = 1000000)
        {
            byte[] encodebytes = System.Text.Encoding.UTF8.GetBytes(json);
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            item.Timeout = timeout;
            item.Method = "POST";
            item.PostdataByte = encodebytes;
            item.PostEncoding = Encoding.UTF8;
            item.PostDataType = PostDataType.Byte;
            HttpResult result = helper.GetHtml(item);
            string msg = "";
            if ((int)result.StatusCode < 400)
            {
                msg = result.Html;
                if (msg == "操作超时")
                {
                    //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
                    msg = "";
                }
            }
            else
            {
                //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
            }
            return msg;
        }

        /// <summary>
        /// PostUTF8格式的表单
        /// </summary>
        /// <param name="json"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostGB2312Form(string json, string url, int timeout = 5000)
        {
            byte[] encodebytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(json);
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            item.Timeout = timeout;
            item.Method = "POST";
            item.Allowautoredirect = true;
            item.ContentType = "application/x-www-form-urlencoded";
            item.PostdataByte = encodebytes;
            item.PostEncoding = Encoding.GetEncoding("GB2312");
            item.PostDataType = PostDataType.Byte;
            HttpResult result = helper.GetHtml(item);
            string msg = "";
            if ((int)result.StatusCode < 400)
            {
                msg = result.Html;
                if (msg == "操作超时")
                {
                    //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
                    msg = "";
                }
            }
            else
            {
                //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
            }
            return msg;
        }

        /// <summary>
        /// PostUTF8格式的JSON
        /// </summary>
        /// <param name="json"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostUtf8File(byte[] encodebytes, string url, int timeout = 1000000)
        {
            //byte[] encodebytes = System.Text.Encoding.UTF8.GetBytes(json);
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            item.Timeout = timeout;
            item.Method = "POST";
            item.ContentType = "multipart/form-data";
            item.PostdataByte = encodebytes;
            item.PostEncoding = Encoding.UTF8;
            item.PostDataType = PostDataType.Byte;
            HttpResult result = helper.GetHtml(item);
            string msg = "";
            if ((int)result.StatusCode < 400)
            {
                msg = result.Html;
                if (msg == "操作超时")
                {
                    //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
                    msg = "";
                }
            }
            else
            {
                //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
            }
            return msg;
        }

        /// <summary>
        /// Get格式
        /// </summary>
        /// <param name="json"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrl(string url, int timeout = 1000000)
        {
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            item.Timeout = timeout;
            item.URL = url;
            HttpResult result = helper.GetHtml(item);
            string msg = "";
            if ((int)result.StatusCode < 400)
            {
                msg = result.Html;
                if (msg == "操作超时")
                {
                    //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
                    msg = "";
                }
            }
            else
            {
                //LoggerFactory.Current.Create().LogError("请求错误，状态码为" + (int)result.StatusCode + ",url为" + url);
            }
            return msg;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        public static string get_noce_str()
        {
            if (noseed == int.MaxValue)
            {
                noseed = 1;
            }
            noseed++;
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(16);
            Random rd = new Random((unchecked((int)DateTime.Now.Ticks + noseed)));
            for (int i = 0; i < 16; i++)
            {
                sb.Append(constant[rd.Next(62)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成6位订单号
        /// </summary>
        /// <returns></returns>
        public static string Nrandom()
        {
            if (domseed == int.MaxValue)
            {
                domseed = 1;
            }
            domseed++;
            string rm = "";
            Random rd = new Random((unchecked((int)DateTime.Now.Ticks + domseed)));
            for (int i = 0; i < 6; i++)
            {
                rm += rd.Next(0, 9).ToString();
            }
            return rm;
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkStringB(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkStringC(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Value);
            }
            return prestr.ToString();
        }

        /// <summary>
        /// 多余字段用指定字符串代替
        /// </summary>
        /// <param name="MaxLength">字符串最大长度</param>
        /// <param name="ReplaceRemark">超出时代替的符号</param>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static string Overflow(int MaxLength, string ReplaceRemark, string value)
        {
            if (value.Length > MaxLength)
            {
                return value = value.Remove(MaxLength) + ReplaceRemark;
            }
            return value;
        }

        #region 得到一周的周一和周日的日期
        /// <summary> 
        /// 计算本周的周一日期 
        /// </summary> 
        /// <returns></returns> 
        public static DateTime GetMondayDate()
        {
            return GetMondayDate(DateTime.Now);
        }
        /// <summary> 
        /// 计算本周周日的日期 
        /// </summary> 
        /// <returns></returns> 
        public static DateTime GetSundayDate()
        {
            return GetSundayDate(DateTime.Now);
        }
        /// <summary> 
        /// 计算某日起始日期（礼拜一的日期） 
        /// </summary> 
        /// <param name="someDate">该周中任意一天</param> 
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns> 
        public static DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }
        /// <summary> 
        /// 计算某日结束日期（礼拜日的日期） 
        /// </summary> 
        /// <param name="someDate">该周中任意一天</param> 
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns> 
        public static DateTime GetSundayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。 
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }
        #endregion
    }

    public class ValidateCode
    {
        public ValidateCode()
        {
        }
        /// <summary>
        /// 验证码的最大长度
        /// </summary>
        public int MaxLength
        {
            get { return 10; }
        }
        /// <summary>
        /// 验证码的最小长度
        /// </summary>
        public int MinLength
        {
            get { return 1; }
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }
        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="context">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        public void CreateValidateGraphic(string validateCode, HttpContext context)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                context.Response.Clear();
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(stream.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        /// <summary>
        /// 得到验证码图片的长度
        /// </summary>
        /// <param name="validateNumLength">验证码的长度</param>
        /// <returns></returns>
        public static int GetImageWidth(int validateNumLength)
        {
            return (int)(validateNumLength * 12.0);
        }
        /// <summary>
        /// 得到验证码的高度
        /// </summary>
        /// <returns></returns>
        public static double GetImageHeight()
        {
            return 22.5;
        }



        //C# MVC 升级版
        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}