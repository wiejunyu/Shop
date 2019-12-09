using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Cms.Models
{
    class HttpModels
    {
    }

    //订单
    public class TradeRecordModels
    {
        public int order { get; set; }
        public int login { get; set; }
        public string symbol { get; set; }
        public int digits { get; set; }
        public int cmd { get; set; }
        public int volume { get; set; }
        public long open_time { get; set; }
        public int state { get; set; }
        public double open_price { get; set; }
        public double sl { get; set; }
        public double tp { get; set; }
        public long close_time { get; set; }
        public int gw_volume { get; set; }
        public long expiration { get; set; }
        public string reason { get; set; }
        public string conv_reserv { get; set; }
        public double[] conv_rates { get; set; }
        public double commission { get; set; }
        public double commission_agent { get; set; }
        public double storage { get; set; }
        public double close_price { get; set; }
        public double profit { get; set; }
        public double taxes { get; set; }
        public int magic { get; set; }
        public string comment { get; set; }
        public int gw_order { get; set; }
        public int activation { get; set; }
        public short gw_open_price { get; set; }
        public short gw_close_price { get; set; }
        public double margin_rate { get; set; }
        public long timestamp { get; set; }
        public int[] api_data { get; set; }
        public TradeRecordModels()
        {
            order = 0;
            login = 0;
            symbol = "";
            digits = 0;
            cmd = 0;
            volume = 0;
            open_time = 0;
            open_price = 0;
            sl = 0;
            tp = 0;
            close_time = 0;
            gw_volume = 0;
            expiration = 0;
            reason = "";
            conv_reserv = "";
            commission = 0;
            commission_agent = 0;
            storage = 0;
            close_price = 0;
            profit = 0;
            taxes = 0;
            magic = 0;
            comment = "";
            gw_order = 0;
            activation = 0;
            gw_open_price = 0;
            gw_close_price = 0;
            margin_rate = 0;
            timestamp = 0;
            api_data = new int[4];
            conv_rates = new double[2];
        }
    }
}
