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
    public partial class Order : ContentPage
    {
        public Order()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ChildName.Text = Children.StudentName;
            IC.Text = Children.IC;
        }

        private void OnSelectedIndex(object sender, EventArgs e)
        {
            //using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //{
            //    conn.CreateTable<tblPackage>();
            //    var menu = conn.Table<tblPackage>().;
            //}

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var PriceA = conn.Get<tblPackage>(1);
                var PriceB = conn.Get<tblPackage>(2);
                var PriceC = conn.Get<tblPackage>(3);

                if (Package.SelectedIndex == 0)
                {
                    Price.Text = Convert.ToString(PriceA.Price);
                }

                else if (Package.SelectedIndex == 1)
                {
                    Price.Text = Convert.ToString(PriceB.Price);
                }

                else
                {
                    Price.Text = Convert.ToString(PriceC.Price);
                }
            }
        }

        private void ViewMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuParent());
        }


        private void ConfirmOrder_Clicked(object sender, EventArgs e)
        {
            tblOrder order = new tblOrder()
            {
                Date = DateTime.UtcNow.Date,
                ParentEmail = Profile.Email,
                StudentName = Children.StudentName,
                Grade = Children.Grade,
                Class = Children.Class,
                Package = (string)Package.SelectedItem,
                StudentIC = Children.IC,
                PackagePrice = Convert.ToInt16(Price.Text)
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var data = conn.Table<tblOrder>();
                tblOrder data1 = data.Where(x => (x.StudentIC == Children.IC) && ((x.Date <= DateTime.UtcNow.Date) || (x.Date == DateTime.UtcNow.Date))).FirstOrDefault(); //Linq Query 
                if (data1 != null)
                {
                    //var get = conn.Table<tblOrder>

                    DisplayAlert("Alert", Children.StudentName + " order already exists \nPackage ordered " + data1.Package , "OK");
                }

                else
                {
                    conn.CreateTable<tblOrder>();
                    int rowsAdded = conn.Insert(order);
                    Navigation.PopAsync();
                }
            }

        }
    }
}