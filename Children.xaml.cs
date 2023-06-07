using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FOFS.Classes;
using SQLite;

namespace FOFS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Children : ContentPage
    {
        public static string StudentName, IC, Grade, Class;
        public Children()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<tblStudent>();
                var user = conn.Table<tblStudent>().Where(x => x.ParentEmail == Profile.Email).ToList();

                //var data = conn.Table<tblUser>();
                //var data1 = data.Where(x => x.Name == UName.Text && x.Password == PWord.Text).FirstOrDefault(); //Linq Query

                DisplayChild.ItemsSource = user;
            }
        }

        //Selected child details
        void OnItemSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            tblStudent item = (tblStudent)((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;
            StudentName = item.StudentName;
            IC = item.IC;
            Class = item.Class;
            Grade = item.Grade;
            Navigation.PushAsync(new Order());
        }
        //private void OnItemSelected(object sender, EventArgs e)
        //{
        //    //var mydetails = e.Item as tblStudent;
        //    //Navigation.PushAsync(new Order(mydetails, mydetails.IC));
        //}

        private void RefreshChildren_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshChildren.IsRefreshing = false;
        }

        private void AddChild_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Child_Information());
        }
    }
}