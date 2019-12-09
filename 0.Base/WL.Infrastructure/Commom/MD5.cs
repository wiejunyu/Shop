using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
//System.Text 命名空间包含表示 ASCII、Unicode、UTF-7 和 UTF-8 字符编码的类；
//用于将字符块转换为字节块和将字节块转换为字符块的抽象基类；
//以及操作和格式化 String 对象而不创建 String 的中间实例的 Helper 类。 
using System.Security.Cryptography;

namespace WL.Infrastructure.Common
{
    public class MD5
    {
        public static  string Md5(string password)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //使用加密服务提供程序 (CSP) 提供的实现，计算输入数据的 MD5 哈希值。无法继承此类。
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(password))).Replace("-", "");
            //System命名空间BitConverter将基础数据类型与字节数组相互转换。
            //GetBytes将指定的数据转换为字节数组。 
            //ComputeHash计算输入数据的哈希值的方法。 
        }
        public static string Md5UTF8(string password)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //使用加密服务提供程序 (CSP) 提供的实现，计算输入数据的 MD5 哈希值。无法继承此类。
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "");
            //System命名空间BitConverter将基础数据类型与字节数组相互转换。
            //GetBytes将指定的数据转换为字节数组。 
            //ComputeHash计算输入数据的哈希值的方法。 
        }

        public static string Md532(String input)
        {
            string cl = input;
            string pwd = "";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

                pwd = pwd + s[i].ToString("X2");

            }
            return pwd;
        }
        /// <summary>
        /// MD5 16位加密 加密后密码为大写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// MD5 16位加密 加密后密码为大写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static byte[] GetMd5Byte(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString));
        }
        /// <summary>
        /// MD5 16位加密 加密后密码为大写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string Md516UFT8(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string DES3Encrypt(string data, string key)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.Mode = CipherMode.CBC;
            DES.Padding = PaddingMode.PKCS7;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(data);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string DES3Decrypt(string data, string key)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.Mode = CipherMode.CBC;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(data);
                result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}
