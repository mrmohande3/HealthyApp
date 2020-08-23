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
    public partial class PaitentItem : ContentView
    {
        public PaitentItem()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopupPage
            {
                Content = new PatientDialog((BindingContext as Patient)),
                CloseWhenBackgroundIsClicked = true
            }, true);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == nameof(BindingContext))
            {
                var p = BindingContext as Patient;
                if (p != null)
                {
                    if (!p.IsSick)
                    {
                        statusLogo.Text = MaterialIconFont.CloseCircle;
                        statusLogo.TextColor = Color.Red;
                        statusLabel.Text = "مـــریض";
                        statusLabel.TextColor = Color.Red;
                    }
                    else
                    {
                        statusLogo.Text = MaterialIconFont.CheckCircle;
                        statusLogo.TextColor = Color.ForestGreen;
                        statusLabel.Text = "ســـالم";
                        statusLabel.TextColor = Color.ForestGreen;
                    }

                    if (p.Sex == Sex.Male)
                        ageLabel.Text = "مرد";
                    else
                        ageLabel.Text = "زن";
                }
            }
            base.OnPropertyChanged(propertyName);
        }
    }
}