using Newtonsoft.Json;
using prjCSCApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace prjCSCApp.ViewModels
{
    public class CItemVM
    {
        MainPage mp;
        public List<CProduct> mainItem = new List<CProduct>();
        public List<CProduct> subItem = new List<CProduct>();
        public List<CProduct> histItem = new List<CProduct>();
        public List<CProduct> searItem = new List<CProduct>();
        public List<CProduct> tempItem = new List<CProduct>();
        public List<CCart> cartItem = new List<CCart>();
        public List<COrder> orderItem = new List<COrder>();
        public int TotalPrice = 0;
        int numOfHistory = 10;
        int numOfMain = 5;
        //int numOfSub = 10;
        bool isCartLoaded = false;

        public CItemVM()
        {
            mp = Application.Current.Properties[CDictionary.mainPage] as MainPage;
            InitialLoad();
        }


        public async void InitialLoad()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("https://prjcoffee.azurewebsites.net/api/R_Shop");
            var items = JsonConvert.DeserializeObject<List<CProduct>>(result);
            

            mainItem = items.Take(numOfMain).ToList();
            subItem = items.Skip(numOfMain-1).ToList();
            //for (int i = 0; i < numOfMain; i++)
            //{
            //    //if (mainItem[i].MainPhotoPath == null)
            //    //    mainItem[i].MainPhotoPath = "49562fbf-8247-436c-9fa2-32d626786eee.jpg";
            //}
            //for (int i = numOfMain; i < numOfMain + numOfSub; i++)
            //{
            //    //if (subItem[i].MainPhotoPath == null)
            //    //    subItem[i].MainPhotoPath = "49562fbf-8247-436c-9fa2-32d626786eee.jpg";
            //}
            mp.BindingMainSubCM();
            mp.RunBanner(mainItem.Count);
        }

        public async void SearchLoad(string c, int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("https://prjcoffee.azurewebsites.net/api/R_Shop/" + c + "/" + id);
            var items = JsonConvert.DeserializeObject<List<CProduct>>(result);
            searItem = items;
            await mp.Navigation.PushAsync(new Search(searItem));
        }

        public async void KWSearchLoad(string kw)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("https://prjcoffee.azurewebsites.net/api/R_Shop/" + kw + "/0");
            var items = JsonConvert.DeserializeObject<List<CProduct>>(result);
            searItem = items;
            await mp.Navigation.PushAsync(new Search(searItem));
        }

        public async void ShowCartPage()
        {
            if (!isCartLoaded)
            {
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync("https://prjcoffee.azurewebsites.net/api/R_Cart/" + mp.user.MemberId);
                var items = JsonConvert.DeserializeObject<List<CCart>>(result);
                cartItem = items.Concat(cartItem).ToList();
                isCartLoaded = true;
            }

            TotalPrice = cartItem.Sum(x => x.Quantity * x.Price);
            await mp.Navigation.PushAsync(new Cart(cartItem));
        }

        public async void ShowItemPage(CProduct p)
        {
            tempItem.Add(p);
            await mp.Navigation.PushAsync(new ShowItem(tempItem));
            tempItem.Clear();
            AddToHistory(p);
        }

        private void AddToHistory(CProduct p)   //private
        {
            if (!histItem.Contains(p))  histItem.Insert(0, p);
            if (histItem.Count > numOfHistory)    histItem.RemoveAt(numOfHistory+1);
        }

        public async void ShowHistoryPage() =>     //use the same with search(page)
            await mp.Navigation.PushAsync(new Search(histItem));


    }
}
