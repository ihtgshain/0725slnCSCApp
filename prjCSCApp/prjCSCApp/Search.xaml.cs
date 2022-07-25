using prjCSCApp.Models;
using prjCSCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjCSCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        MainPage mp;
        CItemVM vm;
        public Search(List<CProduct> list)
        {
            InitializeComponent();
            SubItemView.ItemsSource = list;
            mp = Application.Current.Properties[CDictionary.mainPage] as MainPage;
            vm = mp.vm;
        }

        private void SubItemView_SelectionChanged(object sender, SelectionChangedEventArgs e) => 
            vm.ShowItemPage(e.CurrentSelection[0] as CProduct);
        

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