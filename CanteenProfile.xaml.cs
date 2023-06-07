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
    public partial class CanteenProfile : ContentPage
    {
        public static int unik;
        public static string Name, Password, Email, PhoneNo, Type;
        public CanteenProfile()
        {
            InitializeComponent();
        }

        private void RefreshCanteenProfile_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshCanteenProfile.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<tblUser>();
                var user = conn.Table<tblUser>().Where(x => x.Name == Login.Name && x.Password == Login.Password);
                //ProfileInfo.ItemsSource = user;

                if (user != null)
                {
                    //List<tblUser>
                    var filter = conn.Query<tblUser>("SELECT * FROM tblUser WHERE Name =?", Login.Name, " && Password =?", Login.Password);

                    foreach (var s in filter)
                    {
                        //To display at EditProfile
                        unik = s.Id;
                        Name = s.Name;
                        Password = s.Password;
                        Email = s.Email;
                        PhoneNo = s.PhoneNo;
                        Type = s.Type;

                        //To display at profile
                        CanteenName.Text = s.Name;
                        CanteenId.Text = s.Id.ToString();
                        CanteenPhoneNo.Text = s.PhoneNo;
                        CanteenEmail.Text = s.Email;
                    }
                }
            }
        }

        private void CanteenEditProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfile());
        }

        private void CanteenMenu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Menu());
        }

        private void CanteenOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Total_Order());
        }
    }
}