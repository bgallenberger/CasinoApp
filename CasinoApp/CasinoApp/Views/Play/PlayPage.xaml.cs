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

namespace CasinoApp.Views.Play
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : ContentPage
    {
        Models.Mode mode = new Models.Mode();
        public PlayPage(Models.Mode currentMode)
        {
            InitializeComponent();

            mode = currentMode;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = mode;
            Update();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            var ModeItem = (Models.Mode)BindingContext;

            if (ModeItem.Cost >= 0)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/AddPlay?gameID=" + mode.GameID + "&modeID=" + mode.ID + "&payout=" + mode.Win);
                mode.Win = 0;
                Update();
            }
        }
        async void OnAddJackpotClicked(object sender, EventArgs e)
        {
            var ModeItem = (Models.Mode)BindingContext;
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/AddJackpot?gameID=" + mode.GameID + "&modeID=" + mode.ID);
            Update();
        }
        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Update()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/GetMode?gameID=" + mode.GameID + "&modeID=" + mode.ID);
            var newMode = new Models.Mode();
            newMode = JsonConvert.DeserializeObject<Models.Mode>(response);

            newMode.GameID = mode.GameID;
            newMode.FullName = mode.FullName;

            if (newMode.Jackpots == 0 || newMode.Played == 0)
            {
                newMode.JackpotChance = "0%";
            }
            else
            { 
                newMode.JackpotChance = "" + Math.Round(((double)(newMode.Jackpots / newMode.Played)) * 100) + "%";
            }

            if (newMode.Payedout == 0 || newMode.Played == 0)
            {
                newMode.PayoutPercent = "0%";
            }
            else
            {
                newMode.PayoutPercent = "" + Math.Round((double)(newMode.Payedout / (newMode.Cost * newMode.Played)), 2) * 100 + "%";
            }
            newMode.Win = 0;

            newMode.BannerText = "Played: " + newMode.Played + " times\nPayout Percent: " + newMode.PayoutPercent + "\nBonus: " + newMode.Jackpots + " times\nBonus Chance: " + newMode.JackpotChance;

            mode = newMode;

            this.BindingContext = mode;
        }
    }
}