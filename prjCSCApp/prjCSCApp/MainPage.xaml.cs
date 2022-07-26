using Newtonsoft.Json;
using prjCSCApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.Threading;
using prjCSCApp.ViewModels;
using Xamarin.Forms.Internals;

namespace prjCSCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public CItemVM vm;
        public CMember user;

        public MainPage()
        {
            InitializeComponent();
            Application.Current.Properties[CDictionary.mainPage] = this;
            vm= new CItemVM();
        }
        //==============vm===============
        
        private void SubItemView_SelectionChanged(object sender, SelectionChangedEventArgs e)=>
            vm.ShowItemPage(e.CurrentSelection[0] as CProduct);
        
        private void HotItemView_Clicked(object sender, EventArgs e)=>
            vm.ShowItemPage(vm.mainItem[HotItemView.Position]);


        //===============mp=================
        public void BindingMainSubCM()
        {
            HotItemView.ItemsSource = vm.mainItem;
            SubItemView.ItemsSource = vm.subItem;
        }

        bool timerFlag = true;
        private void btnStop_Clicked(object sender, EventArgs e)
        {
            timerFlag = !timerFlag;
            //btnStop.Source = timerFlag ? "play.png" : "stop.png";
            btnStop.Source = timerFlag ? "stop.png" : "play.png";
        }

        public void RunBanner(int count)
        {
            Device.StartTimer(new TimeSpan(0, 0, 3), () =>
            {
                int p = HotItemView.Position;
                if (timerFlag)
                    HotItemView.Position = p + 1 == count ? 0 : p + 1;
                return true;
            });
        }

        Random rd = new Random();
        private void btnGoodLuck(object sender, EventArgs e)
        {
            CSearch words = new CSearch();
            int r = rd.Next(0, words.all.Length);
            string w = words.all[r];
            txtSearch.Text = w;
        }


        string btnFlag = "";
        string c = "";
        private void btnSel(object sender, EventArgs e)
        {
            CSearch s = new CSearch();
            string b = ((Button)sender).Text;
            int count = 0;
            if (b == btnFlag){
                selector.ItemsSource = s.empty;
                subSel.IsVisible = false;
                mainSel.HeightRequest = 105;
                btnFlag = "";
                return;
            }

            if (b == "生產洲別"){
                selector.ItemsSource = s.Continent;
                count = s.Continent.Length;
                c = "co";
            }
            else if (b == "生產國別"){
                selector.ItemsSource = s.Country;
                count = s.Country.Length;
                c = "cy";
            }
            else if (b == "烘培度"){
                selector.ItemsSource = s.Roasting;
                count = s.Roasting.Length;
                c = "ro";
            }
            else if (b == "處理法"){
                selector.ItemsSource = s.Process;
                count = s.Process.Length;
                c = "pr";
            }
            else if (b == "包裝法"){
                selector.ItemsSource = s.Package;
                count = s.Package.Length;
                c = "pa";
            }
            subSel.IsVisible = true;
            btnFlag = b;
            count = count%4==0 ? (count / 4):(count/4 + 1 );
            mainSel.HeightRequest = (65 * count)+105;
        }

        public void btnSearch(object sender, EventArgs e)
        {
            CSearch cs = new CSearch();
            Button b = sender as Button;
            string str = b.Text;
            int i = cs.all.IndexOf(str);
            int index = cs.i[i];
            vm.SearchLoad(c, index);
        }
        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            string kw = txtSearch.Text;
            vm.KWSearchLoad(kw);
        }

        //=============navBtn==================
        private void btnHistory(object sender, EventArgs e) =>
            vm.ShowHistoryPage();

        private async void btnHome(object sender, EventArgs e) =>
            await Navigation.PopToRootAsync();

        private async void btnCart(object sender, EventArgs e)
        {
            if (user == null)
                await Navigation.PushAsync(new Login(1));
            else
                vm.ShowCartPage();
        }

        private async void btnMember(object sender, EventArgs e)
        {
            if (user == null)
                await Navigation.PushAsync(new Login(2));
            else
                await Navigation.PushAsync(new Member());
        }

    }
}
