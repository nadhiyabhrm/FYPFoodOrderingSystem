using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using FOFS.Classes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOFS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMenu : ContentPage
    {
        public EditMenu()
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

                //To display at EditProfile
                CurrentAPrice.Text = packageA.Price.ToString();
                CurrentBPrice.Text = packageB.Price.ToString();
                CurrentCPrice.Text = packageC.Price.ToString();
            }
        }

        private void UpdatedPrice_Clicked(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(NewAPrice.Text) || (string.IsNullOrEmpty(NewBPrice.Text) || (string.IsNullOrEmpty(NewCPrice.Text)))))
            {
                DisplayAlert("Alert", "Please insert value in all input", "OK");
            }

            else
            {
                bool isValidA = new System.Text.RegularExpressions.Regex(@"[0-9][0-9]").IsMatch(NewAPrice.Text);
                bool isValidB = new System.Text.RegularExpressions.Regex(@"[0-9][0-9]").IsMatch(NewBPrice.Text);
                bool isValidC = new System.Text.RegularExpressions.Regex(@"[0-9][0-9]").IsMatch(NewCPrice.Text);

                if ((isValidA == false) && (isValidB == false) && (isValidC == false))
                {
                    DisplayAlert("Alert", "Please use number only", "OK");
                }

                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<tblPackage>();
                        var priceA = conn.Get<tblPackage>(1);
                        var priceB = conn.Get<tblPackage>(2);
                        var priceC = conn.Get<tblPackage>(3);

                        priceA.Price = float.Parse(NewAPrice.Text);
                        priceB.Price = float.Parse(NewBPrice.Text);
                        priceC.Price = float.Parse(NewCPrice.Text);
                        conn.Update(priceA);
                        conn.Update(priceB);
                        conn.Update(priceC);
                        Navigation.PopAsync();
                    }
                }
            }
        }
    }
}