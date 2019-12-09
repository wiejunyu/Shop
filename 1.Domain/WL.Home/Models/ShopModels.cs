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
        //缩略图
        public string Image { get; set; }
        //高清图
        public string HighImage { get; set; }
        //备注
        public string Remarks { get; set; }
        //参数
        public string Parameter { get; set; }
        //品牌证书
        public string Certificate { get; set; }

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
            Image = "";
            HighImage = "";
            Remarks = "";
            Parameter = "";
            Certificate = "";
        }
    }

    public class ParameterModels
    {
        //参数名
        public string ParameterName { get; set; }
        //参数值
        public string ParameterData { get; set; }
        public ParameterModels()
        {
            ParameterName = "";
            ParameterData = "";
        }
    }

    public class OrderModels
    {
        //id
        public int Id { get; set; }
        //订单号
        public string Order { get; set; }
        //用户id
        public int UserId { get; set; }
        //商品ID
        public int Sid { get; set; }
        //商品数量
        public int Number { get; set; }
        //运费
        public decimal Freight { get; set; }
        //应付金额
        public decimal Money { get; set; }
        //收货地址
        public string Address { get; set; }
        //发票信息
        public int Invoice { get; set; }
        //订单生成日期
        public DateTime Date { get; set; }
        //支付状态
        public bool PayState { get; set; }
        //支付方式,0货到付款
        public int PayMethod { get; set; }
        //物流状态，0未发货，1运输中，2已收货
        public int LogisticsState { get; set; }
        //物流信息
        public string Logistics { get; set; }
        //备注
        public string Remarks { get; set; }
        //购物车订单商品IDjson
        public string MultipleSid { get; set; }
        //是否为购物车订单
        public bool ShoppingCart { get; set; }
        

        public OrderModels()
        {
            Id = 0;
            Sid = 0;
            Number = 0;
            Freight = 0;
            Money = 0;
            Address = "";
            Invoice = 0;
            Remarks = "";
            Date = DateTime.Now;
            PayState = false;
            PayMethod = 0;
            LogisticsState = 0;
            Logistics = "";
            MultipleSid = "";
            ShoppingCart = false;
        }
    }

    public class OrderViewModels
    {
        //id
        public int Id { get; set; }
        //订单号
        public string Order { get; set; }
        //用户id
        public int UserId { get; set; }
        //商品ID
        public int Sid { get; set; }
        //商品名称
        public string Sname { get; set; }
        //商品价格
        public decimal Price { get; set; }
        //商品数量
        public int Number { get; set; }
        //运费
        public decimal Freight { get; set; }
        //应付金额
        public decimal Money { get; set; }
        //收货地址
        public string Address { get; set; }
        //发票信息
        public int Invoice { get; set; }
        //订单生成日期
        public DateTime Date { get; set; }
        //订单生成日期字符串
        public string DateStr { get; set; }
        //支付状态
        public bool PayState { get; set; }
        //支付方式,0货到付款
        public int PayMethod { get; set; }
        //物流状态，0未发货，1运输中，2已收货
        public int LogisticsState { get; set; }
        //物流信息
        public string Logistics { get; set; }
        //备注
        public string Remarks { get; set; }

        public OrderViewModels()
        {
            Id = 0;
            Sid = 0;
            Sname = "";
            Price = 0;
            Number = 0;
            Freight = 0;
            Money = 0;
            Address = "";
            Invoice = 0;
            Remarks = "";
            Date = DateTime.Now;
            DateStr = "";
            PayState = false;
            PayMethod = 0;
            LogisticsState = 0;
            Logistics = "";
        }
    }

    public class OrderListModels
    {
        //订单号
        public string Order { get; set; }
        //订单选中状态
        public string State { get; set; }


        public OrderListModels()
        {
            Order = "";
            State = "";
        }
    }

    public class ShoppingCartModels
    {
        //id
        public int Id { get; set; }
        //用户id
        public int UserId { get; set; }
        //商品ID
        public int ShopId { get; set; }
        //数量
        public int Number { get; set; }
        //加入购物车时间
        public DateTime Date { get; set; }

        public ShoppingCartModels()
        {
            Id = 0;
            ShopId = 0;
            UserId = 0;
            Number = 0;
            Date = new DateTime();
        }
    }

}
