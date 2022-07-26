﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace prjCSCApp.Models
{
    public class COrder
    {
        [JsonProperty("OrderId")]
        public int OrderId { get; set; }

        [JsonProperty("MemberId")]
        public int MemberId { get; set; }

        [JsonProperty("OrderDate")]
        public DateTime OrderDate { get; set; }

        public string FullOrderDate { get { return "下單日期："+OrderDate.ToString("yyyy/MM/dd"); } }

        [JsonProperty("OrderState")]
        public string OrderState { get; set; }

        public string FullOrderState { get { return "訂單狀況：" + OrderState; } }
    }
}
