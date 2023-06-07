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
    public partial class MenuParent : ContentPage
    {
        public static string package;

        public MenuParent()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<tblPackage>();
                var packageA = conn.Get<tblPackage>(1);
                var packageB = conn.Get<tblPackage>(2);
                var packageC = conn.Get<tblPackage>(3);
                //ProfileInfo.ItemsSource = user;

                //To display at profile
                ParentPackageAPrice.Text = packageA.Price.ToString();
                ParentPackageBPrice.Text = packageB.Price.ToString();
                ParentPackageCPrice.Text = packageC.Price.ToString();
            }
        }

        private void ViewPackageA_Clicked(object sender, EventArgs e)
        {
            package = "A";
            Navigation.PushAsync(new Package());
        }

        private void ViewPackageB_Clicked(object sender, EventArgs e)
        {
            package = "B";
            Navigation.PushAsync(new Package());
        }

        private void ViewPackageC_Clicked(object sender, EventArgs e)
        {
            package = "C";
            Navigation.PushAsync(new Package());
        }
    }
}