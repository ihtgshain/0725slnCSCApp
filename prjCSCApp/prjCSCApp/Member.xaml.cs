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
            var items = JsonConvert.DeserializeObject<List<COrder>>(result);
            vm.orderItem = items;
            listOrder.ItemsSource = vm.orderItem;
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            int i = e.ItemIndex;
            int id = vm.orderItem[i].OrderId;


        }
    }
}