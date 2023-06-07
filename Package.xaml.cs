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
    public partial class Package : ContentPage
    {
        public static int id;

        public static string food, drink, snack, fruit;
        public Package()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                if ((MenuParent.package == "A") || (Menu.package == "A"))
                {
                    PakejA.Text = "A";
                    conn.CreateTable<tblMenu>();
                    var user = conn.Table<tblMenu>().Where<tblMenu>(x => x.Id == 1 || x.Id == 2 || x.Id == 3 || x.Id == 4 || x.Id == 5).ToList();

                    DisplayMenu.ItemsSource = user;
                }
                else if ((MenuParent.package == "B") || (Menu.package == "B"))
                {
                    PakejA.Text = "B";
                    conn.CreateTable<tblMenu>();
                    var user = conn.Table<tblMenu>().Where<tblMenu>(x => x.Id == 6 || x.Id == 7 || x.Id == 8 || x.Id == 9 || x.Id == 10).ToList();

                    DisplayMenu.ItemsSource = user;
                }
                else
                {
                    PakejA.Text = "C";
                    conn.CreateTable<tblMenu>();
                    var user = conn.Table<tblMenu>().Where<tblMenu>(x => x.Id == 11 || x.Id == 12 || x.Id == 13 || x.Id == 14 || x.Id == 15).ToList();

                    DisplayMenu.ItemsSource = user;
                }
            }
        }

        //Selected menu to edit
        void OnItemSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (CanteenProfile.Type == "Canteen")
            {
                tblMenu item = (tblMenu)((ListView)sender).SelectedItem;
                ((ListView)sender).SelectedItem = null;
                id = item.Id;
                food = item.Food;
                drink = item.Drink;
                snack = item.Snack;
                fruit = item.Fruit;
                Navigation.PushAsync(new EditMenuDetails());
            }
        }
    }
}