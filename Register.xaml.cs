using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FOFS.Classes;
using System.Text.RegularExpressions;

namespace FOFS
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {

        public Register()
        {
            InitializeComponent();
        }

        public static bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(Pwd.Text)) || (string.IsNullOrEmpty(Email.Text)) || (string.IsNullOrEmpty(Name.Text)) || (string.IsNullOrEmpty(PhoneNo.Text)))
            {
                DisplayAlert("Alert", "Please insert value in all input", "OK");
            }

            else
            {
                //For PhoneNo validation in Malaysia
                bool isValid = new System.Text.RegularExpressions.Regex(@"^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$").IsMatch(PhoneNo.Text);

                if ((Pwd.Text.Length < 8))
                {
                    DisplayAlert("Alert", "Please use atleast 8 character in Password", "OK");
                }

                else if(EmailIsValid (Email.Text) == false)
                {
                    DisplayAlert("Alert", "Please use valid Email", "OK");
                }

                else if (isValid == false)
                {
                    DisplayAlert("Alert", "Please use valid PhoneNo", "OK");
                }

                else
                {
                    tblUser user = new tblUser()
                    {
                        Name = Name.Text,
                        Password = Pwd.Text,
                        Email = Email.Text,
                        PhoneNo = PhoneNo.Text,
                        Type = "Parent"
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        var data = conn.Table<tblUser>();
                        var data1 = data.Where(x => x.Email == Email.Text).FirstOrDefault(); //Linq Query 
                        if (data1 != null)
                        {
                            DisplayAlert("Alert", "Please use different Email", "OK");
                        }

                        else
                        {
                            conn.CreateTable<tblUser>();
                            int rowsAdded = conn.Insert(user);
                            Navigation.PushAsync(new Login());
                        }
                    }
                }
            }
        }

        //Properties.Resources.db_con
        //MySqlConnection db = new MySqlConnection("server=localhost;userid=root;password=null;database=dbfofs");
        //db.Open();

        //MySqlCommand cmd = new MySqlCommand("insert into tblParent(Name,Password,Email,PhoneNo) values('" + UName.Text + "','" + PWord.Text + "','" + Email.Text + "','" + PhoneNo + "')",db);
        //var rd = cmd.ExecuteReaderAsync();
        //db.Close();

        /*private void LoadRegisterList()
        {
            testView.Text = "Halimah";//.Add(new RegisterData { name = "Halimah", password = "123", email = "halimah@gmail.com", phoneNo = "01123456789" });
        }

        {
            RegisterData reg = new RegisterData();

            reg.name = UName.Text;
            reg.password = PWord.Text;
            reg.email = Email.Text;
            reg.phoneNo = PhoneNo.Text;

            UName.Text = "";
            PWord.Text = "";
            Email.Text = "";
            PhoneNo.Text = "";

            LoadRegisterList();

        */

    }
}