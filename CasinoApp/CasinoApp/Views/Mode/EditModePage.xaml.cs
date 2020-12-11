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

namespace CasinoApp.Views.Mode
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditModePage : ContentPage
    {
        public EditModePage()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var ModeItem = (Models.Mode)BindingContext;
            if (ModeItem.Name != "" || ModeItem.Name != null && ModeItem.Cost > 0)
            {
                HttpClient client = new HttpClient();
                await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/EditMode?gameID=" + ModeItem.GameID + "&id=" + ModeItem.ID + "&newName=" + ModeItem.Name + "&newCost=" + ModeItem.Cost);
                await Navigation.PopAsync();
            }
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var ModeItem = (Models.Mode)BindingContext;
            HttpClient client = new HttpClient();
            await client.GetStringAsync("http://casinowebapp.azurewebsites.net/API/DeleteMode?gameID=" + ModeItem.GameID + "&id=" + ModeItem.ID);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}