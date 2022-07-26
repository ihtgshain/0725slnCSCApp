using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace prjCSCApp
{
    public partial class CProduct
    {
        [JsonProperty("ProductId")]
        public int ProductId { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("Price")]
        public int Price { get; set; }

        public string FullPrice { get { return "單價:" + Price +"元"; } }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Stock")]
        public int Stock { get; set; }

        [JsonProperty("MainPhotoPath")]
        public string MainPhotoPath { get; set; }

        public string FullPhotoPath { get { return "https://prjcoffee.azurewebsites.net/api/R_Shop/" + MainPhotoPath; } }
    }
}
