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
using CasinoApp.Views.Play;

namespace CasinoApp.Views.Mode
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModeMainPage : ContentPage
    {
        Game game;
        List<Models.Mode> list;
        public ModeMainPage(Game currentGame)
        {
            InitializeComponent();
            game = currentGame;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/ModeList?id=" + game.ID);
            list = JsonConvert.DeserializeObject<List<Models.Mode>>(response);
            foreach (var item in list)
            {
                item.FullName = "$" + item.Cost + " " + item.Name;
                item.GameID = game.ID;
                if (item.Jackpots == 0 || item.Played == 0)
                {
                    item.JackpotChance = "0%";
                }
                else
                {
                    item.JackpotChance = "" + Math.Round((double)(item.Jackpots / item.Played), 2) + "%";
                }

                if (item.Payedout == 0 || item.Played == 0)
                {
                    item.PayoutPercent = "0%";
                }
                else
                {
                    item.PayoutPercent = "" + Math.Round((double)(item.Payedout / (item.Cost * item.Played)), 2) * 100 + "%";
                }
            }

            listView.ItemsSource = list;
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            var newMode = new Models.Mode();
            newMode.GameID = game.ID;
            await Navigation.PushAsync(new AddModePage
            {
                BindingContext = newMode
            });
        }

        async void EditMode(object sender, EventArgs e)
        {
            var item = (Image)sender;
            var gesture = (TapGestureRecognizer)item.GestureRecognizers[0];
            double itemName = (double)gesture.CommandParameter;
            var selectedItem = (from mode in list
                                where mode.Cost == itemName
                                select mode).FirstOrDefault<Models.Mode>();
            await Navigation.PushAsync(new EditModePage
            {
                BindingContext = selectedItem as Models.Mode
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new PlayPage(e.SelectedItem as Models.Mode));
            }
        }
    }
}