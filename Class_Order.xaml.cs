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
    public partial class Class_Order : ContentPage
    {
        public Class_Order()
        {
            InitializeComponent();
        }

        private void RefreshClassOrder_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshClassOrder.IsRefreshing = false;
        }

        private void ClassFilter_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                var student = conn.Table<tblOrder>().Where(x => (x.Grade == (string)Grade.SelectedItem) && (x.Class == (string)Class.SelectedItem));

                if (student != null)
                {
                    var user = conn.Table<tblStudent>().Where(x => (x.Grade == (string)Grade.SelectedItem) && (x.Class == (string)Class.SelectedItem)).ToList();
                    DisplayOrder.ItemsSource = student;
                }

                //else if (student == null)
                //{
                //    Alert.Text = "No order for " + Grade.SelectedItem + "\t" + Class.SelectedItem;
                //    DisplayAlert("Alert", "No order for " + Grade.SelectedItem + "\t" +  Class.SelectedItem, "OK");
                //}
            }
        }
    }
}