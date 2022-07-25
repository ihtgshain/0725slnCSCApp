using Newtonsoft.Json;
using prjCSCApp.Models;
using prjCSCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjCSCApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        MainPage mp;
        CItemVM vm;
        int targetFlag=0;
        public Login(int tFlag)
        {
            InitializeComponent();
            mp = Application.Current.Properties[CDictionary.mainPage] as MainPage;
            vm = mp.vm;
            targetFlag = tFlag;
        }

        private async void btnLogin(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string pw = txtPW.Text;
            if (id == "" || pw == "")
            {
                await DisplayAlert("錯誤", "帳號密碼不可為空白。", "OK");
                return;
            }

            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync($"https://prjcoffee.azurewebsites.net/api/R_Member/{id}/{pw}");
            var member = JsonConvert.DeserializeObject<CMember>(result);
            if (member == null)
            {
                await DisplayAlert("錯誤", "帳號密碼有誤，請重新輸入。", "OK");
                txtID.Text = txtPW.Text = "";
            }
            else
            {
                Application.Current.Properties[CDictionary.user] = member;
                mp.user = member;
                Navigation.RemovePage(this);
                
                if (targetFlag == 1)
                    vm.ShowCartPage();
                else if(targetFlag==2)
                    await Navigation.PushAsync(new Member());
                //else if (targetFlag == 0)
                //    await Navigation.PopAsync();
            }
        }

        private void btnDemo(object sender, EventArgs e)
        {
            txtID.Text = "0933333333";
            txtPW.Text = "0933333333";
        }
    }


}