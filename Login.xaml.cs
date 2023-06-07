using FOFS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace FOFS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public static string Name, Password;

        public Login()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var image = new Image { Source = "Logo.jpg" };
            base.OnAppearing();
        }

        private void RefreshLogin_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshLogin.IsRefreshing = false;
        }

        private void Log_In_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var data = conn.Table<tblUser>();
                var data1 = data.Where(x => x.Name == UName.Text && x.Password == PWord.Text).FirstOrDefault(); //Linq Query  
                var data2 = data.Where(x => x.Name == UName.Text && x.Password == PWord.Text && x.Type == "Parent").FirstOrDefault();
                var data3 = data.Where(x => x.Name == UName.Text && x.Password == PWord.Text && x.Type == "Teacher").FirstOrDefault();
                var data4 = data.Where(x => x.Name == UName.Text && x.Password == PWord.Text && x.Type == "Admin").FirstOrDefault();

                //Login as Parent
                if (data1 != null)
                {
                    Name = UName.Text;
                    Password = PWord.Text;

                    //Login as Teacher
                    if (data2 == null)
                    {
                        //Login as Admin
                        if (data3 == null)
                        {
                            //Login as Canteen
                            if (data4 == null)
                            {
                                Navigation.PushAsync(new CanteenProfile());
                            }
                            else
                            {
                                Navigation.PushAsync(new UserList());
                            }

                        }
                        else
                        {
                            Navigation.PushAsync(new Class_Order());
                        }
                    }

                    else
                    {
                        Navigation.PushAsync(new Profile());
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Please use correct username and password" + "\n" + "Or Sign Up", "OK");
                }
            }
        }

        private void SignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }

        private void Display_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserList());
        }

            //Doesn't work
            //protected override void OnAppearing()
            //{
            //    base.OnAppearing();

            //    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //    {
            //        //tblUser user = new tblUser()
            //        //{
            //        //    Name = "Izzati",
            //        //    Password = "123",
            //        //    Email = "Izzati@gmail.com",
            //        //    PhoneNo = "01284689268",
            //        //    Type = "Staff"
            //        //};
            //    }
            //}
        }
}