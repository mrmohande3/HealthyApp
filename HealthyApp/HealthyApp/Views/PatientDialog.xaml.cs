using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthyApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientDialog : ContentView
    {
        public PatientDialog()
        {
            InitializeComponent();
        }
        public PatientDialog(Patient patient)
        {
            InitializeComponent();

            if (!patient.IsSick)
            {
                logoLebel.Text = MaterialIconFont.CloseCircle;
                logoLebel.TextColor = Color.Red;
                titleLabel.Text = "مـــریض";
                titleLabel.TextColor = Color.Red;
            }
            else
            {
                logoLebel.Text = MaterialIconFont.CheckCircle;
                logoLebel.TextColor = Color.ForestGreen;
                titleLabel.Text = "ســـالم";
                titleLabel.TextColor = Color.ForestGreen;
            }

            dateLabel.Text = patient.AddDateTime.ToString();
            nameLabel.Text = patient.Name;
            nameEntry.Text = patient.Name;

            if (patient.Sex == Sex.Male)
                sexPicker.SelectedIndex = 1;
            else
                sexPicker.SelectedIndex = 0;

            if (patient.ChestPain == ChestPain.AtypicalAngina)
                cpPicker.SelectedIndex = 1;
            else if (patient.ChestPain == ChestPain.NonAnginalPain)
                cpPicker.SelectedIndex = 0;
            else if (patient.ChestPain == ChestPain.TypicalAngina)
                cpPicker.SelectedIndex = 2;
            else
                cpPicker.SelectedIndex = 3;

            ageEntry.Text = patient.Age.ToString();

            bloodSlider.Value = patient.BloodPressure;

            colSlider.Value = patient.Cholestoral;


        }

    }
}