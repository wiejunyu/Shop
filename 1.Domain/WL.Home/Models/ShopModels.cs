using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class ShopModels
    {
        //id
        public int id { get; set; }
        //商品名称
        public string Name { get; set; }
        //栏目ID
        public int Catid { get; set; }
        //上架时间
        public DateTime Uptime { get; set; }
        //计量单位
        public string Units { get; set; }
        //品牌
        public string Brand { get; set; }
        //优惠价
        public decimal Trueprice { get; set; }
        //市场价
        public decimal Price { get; set; }
        //详细介绍
        public string Body { get; set; }
        //型号
        public string Model { get; set; }
        //行业
        public string Vocation { get; set; }
        //点击次数
        public int Click { get; set; }
        //购买次数
        public int Buy { get; set; }
        //推荐
        public bool Recommend { get; set; }

        public ShopModels()
        {
            id = 0;
            Name = "";
            Catid = 0;
            Uptime = new DateTime();
            Units = "";
            Brand = "";
            Trueprice = 0;
            Price = 0;
            Body = "";
            Model = "";
            Vocation = "";
            Click = 0;
            Buy = 0;
            Recommend = false;
        }
    }
}
