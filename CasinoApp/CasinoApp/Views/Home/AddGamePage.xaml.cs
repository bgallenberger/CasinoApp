using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using CasinoApp.Models;

namespace CasinoApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddGamePage : ContentPage
    {
        public AddGamePage()
        {
            InitializeComponent();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            var GameItem = (Game)BindingContext;

            if (GameItem.Name != "" || GameItem.Name != null)
            {
                HttpClient client = new HttpClient();
                await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/AddGame?Name=" + GameItem.Name);
                await Navigation.PopAsync();
            }
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}