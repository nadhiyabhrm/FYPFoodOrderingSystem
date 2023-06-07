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
    public partial class Profile : ContentPage
    {
        public static int unik;
        public static string Name, Password, Email, PhoneNo, Type;
        public Profile()
        {
            InitializeComponent();
        }

        private void RefreshProfile_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshProfile.IsRefreshing = false;
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
                        ProfileName.Text = s.Name;
                        ProfileId.Text = s.Id.ToString();
                        ProfilePhoneNo.Text = s.PhoneNo;
                        ProfileEmail.Text = s.Email;
                    }
                }
            }
        }

        private void EditProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfile());
        }

        private void Child_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Children());
        }

        private void Menu_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuParent());
        }

        private void Order_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Order());
        }
    }
}