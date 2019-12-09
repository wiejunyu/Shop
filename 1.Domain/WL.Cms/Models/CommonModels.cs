using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WL.Cms.Models
{
    public class CommonModels
    {
        /// <summary>
        /// cms获取网址
        /// </summary>
        /// <returns></returns>
        public static string GetCmsWww()
        {
            return "http://localhost:44577/";
        }

        /// <summary>
        /// Home获取网址
        /// </summary>
        /// <returns></returns>
        public static string GetHomeWww()
        {
            return "http://localhost:44577/";
        }
    }
}