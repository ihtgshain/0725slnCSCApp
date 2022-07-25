using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace prjCSCApp.Models
{

    public partial class CCart
    {
        [JsonProperty("ShoppingCarDetialsId")]
        public int ShoppingCarDetialsId { get; set; }

        [JsonProperty("MemberId")]
        public int MemberId { get; set; }

        [JsonProperty("ProductsId")]
        public int ProductsId { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }
        public string FullQuantity { get { return "數量:"+Quantity; } }

        [JsonProperty("Price")]
        public int Price { get; set; }

        public string FullPrice { get { return "單價:"+Price; } }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("MainPhotoPath")]
        public string MainPhotoPath { get; set; }

        public string FullPhotoPath { get { return "https://prjcoffee.azurewebsites.net/api/R_Shop/" + MainPhotoPath; } }

        [JsonProperty("Stock")]
        public int Stock { get; set; }

        public string Sum { get { return "小計:"+(Quantity * Price); } }
    }
}
