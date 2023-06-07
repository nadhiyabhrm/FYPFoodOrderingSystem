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
    public partial class Total_Order : ContentPage
    {
        public Total_Order()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); ;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<tblOrder>();
                var MpackageA = conn.Table<tblOrder>().Where(x => x.Package == "A" && (x.Grade == "4") || (x.Grade == "5") || (x.Grade == "6"));
                var McountA = MpackageA.Count();
                MorningTotalA.Text = McountA.ToString();

                var MpackageB = conn.Table<tblOrder>().Where(x => x.Package == "B" && (x.Grade == "4")||(x.Grade == "5") ||(x.Grade == "6"));
                var McountB = MpackageB.Count();
                MorningTotalB.Text = McountB.ToString();

                var MpackageC = conn.Table<tblOrder>().Where(x => x.Package == "C" && (x.Grade == "4") || (x.Grade == "5") || (x.Grade == "6"));
                var McountC = MpackageC.Count();
                MorningTotalC.Text = McountC.ToString();

                conn.CreateTable<tblOrder>();
                var ApackageA = conn.Table<tblOrder>().Where(x => x.Package == "A" && (x.Grade == "1") || (x.Grade == "2") || (x.Grade == "3"));
                var AcountA = ApackageA.Count();
                AfternoonTotalA.Text = AcountA.ToString();

                var ApackageB = conn.Table<tblOrder>().Where(x => x.Package == "B" && (x.Grade == "1") || (x.Grade == "2") || (x.Grade == "3"));
                var AcountB = ApackageB.Count();
                AfternoonTotalB.Text = AcountB.ToString();

                var ApackageC = conn.Table<tblOrder>().Where(x => x.Package == "C" && (x.Grade == "1") || (x.Grade == "2") || (x.Grade == "3"));
                var AcountC = ApackageC.Count();
                AfternoonTotalC.Text = AcountC.ToString();

                //List<tblUser>
                //var morning = conn.Query<tblOrder>("SELECT * FROM tblOrder WHERE Grade = '4' || '5' || '6' ");
            }

        }
        private void ClassOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Class_Order());
        }
    }
}