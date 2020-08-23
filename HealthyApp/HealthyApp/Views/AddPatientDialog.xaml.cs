using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthyApp.Models;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatientDialog : ContentView
    {
        public AddPatientDialog()
        {
            InitializeComponent();
        }

        private void AddButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var paitent = new Patient
                {
                    Name = nameEntry.Text,
                    Age = int.Parse(ageEntry.Text),
                    Cholestoral = (int)colSlider.Value,
                    BloodPressure = (int)bloodSlider.Value,
                    AddDateTime = DateTime.Now
                };
                switch (cpPicker.SelectedItem as string)
                {
                    case "Non Anginal Pain":
                        paitent.ChestPain = ChestPain.NonAnginalPain;
                        break;
                    case "Atypical Angina":
                        paitent.ChestPain = ChestPain.AtypicalAngina;
                        break;
                    case "Typical Angina":
                        paitent.ChestPain = ChestPain.TypicalAngina;
                        break;
                    case "Asymptomatic":
                        paitent.ChestPain = ChestPain.asymptomatic;
                        break;
                }

                switch (sexPicker.SelectedItem as string)
                {
                    case "زن":
                        paitent.Sex = Sex.Female;
                        break;
                    case "مرد":
                        paitent.Sex = Sex.Male;
                        break;
                }

                paitent.IsSick = DecisionTree.GetTree().Evaluate(paitent);

                using (var _context = new HDBContext())
                {
                    _context.Patients.Add(paitent);
                    _context.SaveChanges();
                }

                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAllAsync(true);
                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopupPage
                {
                    Content = new PatientDialog(paitent),
                    CloseWhenBackgroundIsClicked = true
                }, true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void BloodSlider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            bloodLabel.Text = $"{e.NewValue}";
        }

        private void ColSlider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            colLabel.Text = $"{e.NewValue}";
        }
    }
}