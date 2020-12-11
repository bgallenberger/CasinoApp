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
    public partial class EditGamePage : ContentPage
    {
        public EditGamePage()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var GameItem = (Game)BindingContext;
            if (GameItem.Name != "" || GameItem.Name != null)
            {
                HttpClient client = new HttpClient();
                await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/EditGame?id=" + GameItem.ID + "&newName=" + GameItem.Name);
                await Navigation.PopAsync();
            }
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var GameItem = (Game)BindingContext;
            HttpClient client = new HttpClient();
            await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/DeleteGame?id=" + GameItem.ID);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}