using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using CasinoApp.Models;
using CasinoApp.Views.Mode;

namespace CasinoApp.Views.Home
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<Game> list;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/GameList");
            list = JsonConvert.DeserializeObject<List<Game>>(response);
            foreach(var item in list)
            {
                if(item.Jackpots == 0)
                {
                    item.JackpotChance = "0%";
                }
                else
                {
                    item.JackpotChance = "" + Math.Round((double)(item.Jackpots / item.TotalPlayed), 2) + "%";
                }

                if (item.TotalPayout == 0)
                {
                    item.PayoutPercent = "0%";
                }
                else
                {
                    item.PayoutPercent = "" + Math.Round((double)(item.TotalPayout / (item.TotalPutIn)), 2) * 100 + "%";
                }
            }

            listView.ItemsSource = list;
        }
        //async void Add(object sender, EventArgs e)
        //{
        //    HttpClient client = new HttpClient();
        //    var response = await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/GameList");
        //    var game = JsonConvert.DeserializeObject<List<Game>>(response);
        //}
        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddGamePage
            {
                BindingContext = new Game()
            });
        }

        async void EditGame(object sender, EventArgs e)
        {
            var item = (Image)sender;
            var gesture = (TapGestureRecognizer)item.GestureRecognizers[0];
            String itemName = (string)gesture.CommandParameter;
            var selectedItem = (from game in list
                                where game.Name == itemName
                                select game).FirstOrDefault<Game>();
            await Navigation.PushAsync(new EditGamePage
            {
                BindingContext = selectedItem as Game
            });
        }
        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ModeMainPage(e.SelectedItem as Game));
            }
        }
    }
}
