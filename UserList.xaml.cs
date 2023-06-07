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
    public partial class UserList : ContentPage
    {
        public UserList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Insert in table
            //tblMenu day = new tblMenu()
            //{
            //    Day = "Monday"
            //    Date = Convert.ToString(DateTime.UtcNow.Date),
            //    ParentEmail = "test@gmail.com",
            //    StudentName = "Mimi",
            //    Package = "B",
            //    StudentIC = "121212121212",
            //    PackagePrice = 15,
            //};

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                //Insert in table
                //var data = conn.Table<tblMenu>();
                //var data1 = data.Where(x => x.Id == 6).FirstOrDefault(); //Linq Query 

                //conn.CreateTable<tblMenu>();
                //int rowsAdded = conn.Insert(day);

                //Uncomment and change values to add user manually
                var cmd = new SQLiteCommand(conn);
                cmd.CommandText = "INSERT INTO tblUser(Name, Password, Email, PhoneNo, Type) VALUES('Nurul Aisyah', 'nurulaisyah', 'Aisyah@gmail.com', '0163384930', 'Canteen')";
                cmd.ExecuteNonQuery();

                //Set certain value more than 1
                //conn.CreateTable<tblOrder>();
                //var data2 = conn.Table<tblOrder>();
                //var data3 = data2.Where(x => x.StudentIC == "121212121212");
                //foreach (var item in data3)
                //{
                //    item.PackagePrice = 15;
                //    item.Grade = "4";
                //    item.Class = "Bestari";
                //    conn.Update(item);
                //}

                //Delete row
                //conn.CreateTable<tblMenu>();
                //var data2 = conn.Table<tblMenu>();
                //var data3 = data2.Where(x => x.Id == 17);
                //foreach (var item in data3)
                //{
                //    conn.Delete(item);
                //}

                conn.CreateTable<tblUser>();
                var user = conn.Table<tblUser>().ToList();
                DisplayUser.ItemsSource = user;
            }
        }
    }
}