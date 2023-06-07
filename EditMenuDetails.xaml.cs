using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using FOFS.Classes;

namespace FOFS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMenuDetails : ContentPage
    {
        public EditMenuDetails()
        {
            InitializeComponent();
        }

        private void RefreshEditMenuDetails_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshEditMenuDetails.IsRefreshing = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CurrentFood.Text = Package.food;
            CurrentDrink.Text = Package.drink;
            CurrentFruit.Text = Package.fruit;
            CurrentSnack.Text = Package.snack;
        }

        private void UpdatedMenuDetails_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var data = conn.Table<tblMenu>();
                var data1 = data.Where(x => x.Id == Package.id).FirstOrDefault(); //Linq Query 
                if (data1 != null)
                {
                    conn.CreateTable<tblMenu>();
                    var data2 = conn.Table<tblMenu>();
                    var data3 = data2.Where(x => x.Id == Package.id);
                    foreach (var item in data3)
                    {
                        item.Food = NewFood.Text;
                        item.Drink = NewDrink.Text;
                        item.Fruit = NewFruit.Text;
                        item.Snack = NewSnack.Text;
                        conn.Update(item);
                    }
                    Navigation.PopAsync();
                }
            }
        }
    }
}