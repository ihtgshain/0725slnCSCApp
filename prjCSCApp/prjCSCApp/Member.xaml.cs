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

namespace prjCSCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Member : ContentPage
    {
        MainPage mp;
        CItemVM vm;
        public Member()
        {
            InitializeComponent();
            mp = Application.Current.Properties[CDictionary.mainPage] as MainPage;
            vm = mp.vm;
            OrderLoad();
        }

        public async void OrderLoad()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("https://prjcoffee.azurewebsites.net/api/R_Member/o_" + mp.user.MemberId);
            var items = JsonConvert.DeserializeObject<List<COrder>>(result).OrderByDescending(x=>x.OrderDate).ToList();
            vm.orderItem = items;
            listOrder.ItemsSource = vm.orderItem;

        }
        int i = -1;
        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (i!=e.ItemIndex)
            {
                i = e.ItemIndex;
                int id = vm.orderItem[i].OrderId;
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync("https://prjcoffee.azurewebsites.net/api/R_Member/d_" + id);
                var items = JsonConvert.DeserializeObject<List<COrderDetail>>(result);
                listDetail.ItemsSource = items;
                int total = items.Sum(x => x.Quantity * x.Price);
                txtTotal.Text = $"第{i + 1}筆訂單總金額：{total} 元";
            }
            else
            {
                listDetail.ItemsSource = null;
                txtTotal.Text = "請點擊訂單查閱詳情。";
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
    }
}