using Newtonsoft.Json;
using prjCSCApp.Models;
using prjCSCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace prjCSCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        MainPage mp;
        CItemVM vm;
        public Cart(List<CCart> list)
        {
            InitializeComponent();
            mp = Application.Current.Properties[CDictionary.mainPage] as MainPage;
            vm = mp.vm;
            listCart.ItemsSource = list;
            ShowTotal(list);
        }

        private void ShowTotal(List<CCart> list)
        {
            int total = (int)list.Sum(x => x.Price * x.Quantity);
            txtTotal.Text = $"NTD { total} 元";
        }

        private async void btnPay(object sender, EventArgs e)
        {
            string userName = mp.user.MemberName;
            int TotalItems = vm.cartItem.Count;
            int TotalQuantity = vm.cartItem.Sum(x => x.Quantity);
            string TotalPrice = txtTotal.Text;

            bool result = await DisplayAlert($"{userName} 您好，請確認以下的購買資訊："
                , $"\r\n本次購買 {TotalItems} 款商品，共計 {TotalQuantity} 件\r\n總金額 {TotalPrice} 。"
                , "確認購買", "返回購物車");

            if (result)
            {
                await SaveOrder();
                await mp.Navigation.PushAsync(new Member());
            }
        }

        public async Task SaveOrder()
        {
            string url = "https://prjcoffee.azurewebsites.net/api/R_Member";
            using (var client = new HttpClient())
            {
                COrder order = new COrder();
                order.OrderId = 1;
                order.OrderDate = new DateTime();
                order.MemberId = (int)mp.user.MemberId;
                order.OrderState = "NEW";

                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var strId = await response.Content.ReadAsStringAsync();
                await SaveOrderDetail(int.Parse(strId));
            }
        }

        public async Task SaveOrderDetail(int id)
        {
            string url = "https://prjcoffee.azurewebsites.net/api/R_Member/api/PostOrderDetail";
            using (var client = new HttpClient())
            {
                foreach(var i in vm.cartItem)
                {
                    i.OrderId = id;
                    var json = JsonConvert.SerializeObject(i);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                }
            }
        }


        int i = 0;
        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            i=e.ItemIndex;
        }

        private void btnMinus(object sender, EventArgs e)
        {
            if (vm.cartItem[i].Quantity > 1)
            {
                vm.cartItem[i].Quantity -= 1;
                listCart.ItemsSource=null;
                listCart.ItemsSource = vm.cartItem;
                ShowTotal(vm.cartItem);
            }
        }

        private void btnPlus(object sender, EventArgs e)
        {
            vm.cartItem[i].Quantity += 1;
            listCart.ItemsSource=null;
            listCart.ItemsSource = vm.cartItem;
            ShowTotal(vm.cartItem);
        }

        private async void btnDelete(object sender, EventArgs e)
        {
            var item = vm.cartItem[i];
            bool result = await DisplayAlert($"{mp.user.MemberName} 您好，請確認："
                , $"\r\n是否要從購物車中將商品 {item.ProductsId}：{item.Quantity} 件刪除？"
                , "確認刪除", "返回購物車");

            if (result)
            {
                vm.cartItem.RemoveAt(i);
                listCart.ItemsSource = null;
                listCart.ItemsSource = vm.cartItem;
                ShowTotal(vm.cartItem);
            }
        }

        //=============navBtn==================
        private void btnHistory(object sender, EventArgs e) =>
            vm.ShowHistoryPage();

        private async void btnHome(object sender, EventArgs e) =>
            await Navigation.PopToRootAsync();

        private async void btnCart(object sender, EventArgs e)
        {
            if (mp.user == null)
                await Navigation.PushAsync(new Login(1));
            else
                vm.ShowCartPage();
        }

        private async void btnMember(object sender, EventArgs e)
        {
            if (mp.user == null)
                await Navigation.PushAsync(new Login(2));
            else
                await Navigation.PushAsync(new Member());
        }



        //COrder order = new COrder();
        //order.OrderId = 1;
        //order.OrderDate = DateTime.Now;
        //order.MemberId = (int)mp.user.MemberId;
        //order.OrderState = "NEW";

        //await SaveOrder(order);
        //await mp.Navigation.PushAsync(new Member());

        //var client = new HttpClient();
        //client.BaseAddress = new Uri("https://prjcoffee.azurewebsites.net");

        //string json = JsonConvert.SerializeObject(order);

        //var content = new StringContent(json, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("/api/R_Member", content);

        //var r = await response.Content.ReadAsStringAsync();
    }
}