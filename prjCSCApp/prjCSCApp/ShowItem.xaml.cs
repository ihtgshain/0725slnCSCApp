using prjCSCApp.Models;
using prjCSCApp.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjCSCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowItem : ContentPage
    {
        MainPage mp;
        CItemVM vm;
        CProduct product;

        public ShowItem(List<CProduct> list)
        {
            InitializeComponent();
            mp = Application.Current.Properties[CDictionary.mainPage] as MainPage;
            vm = mp.vm;
            listItem.ItemsSource = list;
            product=list[0];
        }

        private void btnPlus(object sender, EventArgs e)=>
            txtQuantity.Text = (int.Parse(txtQuantity.Text) + 1).ToString();

        private void btnMinus(object sender, EventArgs e)
        {
            int q = int.Parse(txtQuantity.Text);
            q = q <= 1 ? 1 : q - 1;
            txtQuantity.Text = q.ToString();
        }

        private void btnQuantity(object sender, EventArgs e)=> 
            txtQuantity.Text = "1";


        private async void btnAddToCart(object sender, EventArgs e)
        {
            bool exited = true;
            if (mp.user == null)
            {
                await Navigation.PushAsync(new Login(0));
                return;
            } 
            else
            {
                foreach (var i in vm.cartItem)
                {
                    if (i.ProductsId == product.ProductId)
                    {
                        i.Quantity+= int.Parse(txtQuantity.Text);
                        exited = false;
                        break;
                    }
                }
            }
            if (exited)
            {
                CCart c = new CCart
                {
                    MemberId = (int)mp.user.MemberId,
                    ProductsId = product.ProductId,
                    Quantity = int.Parse(txtQuantity.Text),
                    Price = product.Price,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    MainPhotoPath = product.MainPhotoPath,
                    Stock = product.Stock,
                };
                vm.cartItem.Add(c);
            }
            await DisplayAlert($"{mp.user.MemberName} 您好", $"\r\n您選的商品已加入購物車","OK");
            vm.ShowCartPage();
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