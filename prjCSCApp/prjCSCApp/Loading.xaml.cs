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
    public partial class Loading : ContentPage
    {
        public Loading()
        {
            InitializeComponent();
            var task=LoadingAnim();
        }

        public async Task LoadingAnim()
        {
            imgLoading.Opacity = 0.2;
            MainPage mp = new MainPage();
            await imgLoading.FadeTo(1, 3850);
            //await imgLoading.FadeTo(1, 2000);
            Application.Current.MainPage = new NavigationPage(mp);
        }
    }
}