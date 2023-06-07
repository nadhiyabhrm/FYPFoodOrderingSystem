using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using FOFS.Classes;
using System.Text.RegularExpressions;

namespace FOFS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfile : ContentPage
    {
        public UpdateProfile()
        {
            InitializeComponent();
        }

        private void RefreshEditProfile_Refreshing(object sender, EventArgs e)
        {
            Task.Delay(3000);
            RefreshEditProfile.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (CanteenProfile.Type == "Canteen")
            {
                //As canteen
                CurrentName.Text = CanteenProfile.Name;
                CurrentPassword.Text = CanteenProfile.Password;
                CurrentEmail.Text = CanteenProfile.Email;
                CurrentPhoneNo.Text = CanteenProfile.PhoneNo;
            }

            if (Profile.Type == "Parent")
            {
                //As parent
                CurrentName.Text = Profile.Name;
                CurrentPassword.Text = Profile.Password;
                CurrentEmail.Text = Profile.Email;
                CurrentPhoneNo.Text = Profile.PhoneNo;
            }
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

        private void UpdatedProfile_Clicked(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(NewPassword.Text)) || (string.IsNullOrEmpty(NewEmail.Text)) || (string.IsNullOrEmpty(NewName.Text)) || (string.IsNullOrEmpty(NewPhoneNo.Text)))
            {
                DisplayAlert("Alert", "Please insert value in all input", "OK");
            }

            else
            {
                //For PhoneNo validation in Malaysia
                bool isValid = new System.Text.RegularExpressions.Regex(@"^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$").IsMatch(NewPhoneNo.Text);

                if ((NewPassword.Text.Length < 8))
                {
                    DisplayAlert("Alert", "Please use atleast 8 character in Password", "OK");
                }

                else if (EmailIsValid(NewEmail.Text) == false)
                {
                    DisplayAlert("Alert", "Please use valid Email", "OK");
                }

                else if (isValid == false)
                {
                    DisplayAlert("Alert", "Please use valid PhoneNo", "OK");
                }

                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        var data = conn.Table<tblUser>();
                        var data1 = data.Where(x => x.Email == NewEmail.Text).FirstOrDefault(); //Linq Query 
                        if (data1 != null)
                        {
                            //var ans = DisplayAlert("Question?", "Would you like keep current Email?", "Yes", "No");
                            //if (ans.ToString() != "Yes")
                            //{
                                DisplayAlert("Alert", "Please use different Email", "OK");
                            //}
                            //else if (ans.ToString() == "Yes")
                            //{
                            //    conn.CreateTable<tblUser>();
                            //    var data2 = conn.Table<tblUser>();
                            //    var data3 = data2.Where(x => x.Id == Profile.unik);
                            //    foreach (var item in data3)
                            //    {
                            //        item.Name = NewName.Text;
                            //        item.Password = NewPassword.Text;
                            //        item.Email = CurrentEmail.Text;
                            //        item.PhoneNo = NewPhoneNo.Text;
                            //        conn.Update(item);
                            //    }
                            //    Navigation.PopAsync();
                            //}
                            //else
                            //{
                            //    DisplayAlert("Test", "Test", "OK");
                            //}
                        }

                        else
                        {
                            conn.CreateTable<tblUser>();
                            var data2 = conn.Table<tblUser>();
                            var data3 = data2.Where(x => x.Id == Profile.unik);
                            foreach (var item in data3)
                            {
                                item.Name = NewName.Text;
                                item.Password = NewPassword.Text;
                                item.Email = NewEmail.Text;
                                item.PhoneNo = NewPhoneNo.Text;
                                conn.Update(item);
                            }
                            Navigation.PopAsync();
                        }
                    }
                }
            }                        
        }
    }
}


                //var existingUser = conn.Query<tblUser>("select * from tblUser where Id = ?", Profile.unik).FirstOrDefault();
                //if (existingUser != null)
                //{
                //    conn.Update
                //    (
                //        new tblUser
                //        {
                //            Name = NewName.Text,
                //            Password = NewPassword.Text,
                //            Email = NewEmail.Text,
                //            PhoneNo = NewPhoneNo.Text,
                //        }
                //    );

                //    //existingUser.Name = user.Name;
                //    //existingUser.Email = user.Email;
                //    //existingUser.Username = user.Username;
                //    //existingUser.Surname = user.Surname;
                //    //existingUser.EmployeeNumber = user.EmployeeNumber;
                //    //existingUser.Password = user.Password;
                //    //dbConn.RunInTransaction(() =>
                //    //{
                //    //    dbConn.Update(existingUser);
                //    //});
                //}
                //else
                //{
                //    DisplayAlert("Alert", "Please use correct username and password" + "\n" + "Or Sign Up", "OK");
                //}
                //int rowsAdded = conn.Execute("UPDATE tblUser SET Name = ? ", NewName, "WHERE Name =?", Login.Name, " && Password =?", Login.Password);
                //var filter = conn.Query<tblUser>("SELECT * FROM tblUser WHERE Name =?", Login.Name, " && Password =?", Login.Password);
                //conn.Insert(user);