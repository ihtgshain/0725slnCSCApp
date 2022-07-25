using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace prjCSCApp.Models
{
    public class CMember
    {
        [JsonProperty("MemberPhone")]
        public string MemberPhone { get; set; }

        [JsonProperty("MemberId")]
        public long MemberId { get; set; }

        [JsonProperty("ShoppingCarId")]
        public long ShoppingCarId { get; set; }

        [JsonProperty("MemberEmail")]
        public string MemberEmail { get; set; }

        [JsonProperty("MemberPassword")]
        public string MemberPassword { get; set; }

        [JsonProperty("MemberAddress")]
        public string MemberAddress { get; set; }

        [JsonProperty("MemberName")]
        public string MemberName { get; set; }

        [JsonProperty("MemberBirthDay")]
        public DateTimeOffset MemberBirthDay { get; set; }

        [JsonProperty("MemberPhotoPath")]
        public object MemberPhotoPath { get; set; }

        [JsonProperty("BlackList")]
        public bool BlackList { get; set; }

        [JsonProperty("ShoppingCar")]
        public object ShoppingCar { get; set; }

        [JsonProperty("Comments")]
        public object[] Comments { get; set; }

        [JsonProperty("CouponDetails")]
        public object[] CouponDetails { get; set; }

        [JsonProperty("MyLikes")]
        public object[] MyLikes { get; set; }

        [JsonProperty("Orders")]
        public object[] Orders { get; set; }
    }
}

