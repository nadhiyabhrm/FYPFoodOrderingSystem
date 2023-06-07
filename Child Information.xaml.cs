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
    public partial class Child_Information : ContentPage
    {
        public Child_Information()
        {
            InitializeComponent();
        }

        private void AddChild_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StudentName.Text) || string.IsNullOrEmpty(IC.Text) || string.IsNullOrEmpty(Grade.Title) || string.IsNullOrEmpty(Class.Title))
            {
                DisplayAlert("Alert", "Please insert value in all input", "OK");
            }

            else
            {
                tblStudent user = new tblStudent()
                {
                    StudentName = StudentName.Text,
                    IC = IC.Text,
                    Grade = Grade.SelectedItem.ToString(),
                    Class = Class.SelectedItem.ToString(),
                    ParentEmail = Profile.Email,
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    var data = conn.Table<tblStudent>();
                    var data1 = data.Where(x => x.IC == IC.Text).FirstOrDefault(); //Linq Query 
                    if (data1 != null)
                    {
                        DisplayAlert("Alert", "Your IC already in the system", "OK");
                    }

                    else
                    {
                        conn.CreateTable<tblStudent>();
                        int rowsAdded = conn.Insert(user);
                        Navigation.PopAsync();
                    }
                }
            }
        }
    }
}