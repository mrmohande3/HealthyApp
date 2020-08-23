using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HealthyApp.Models;
using Microcharts;
using Rg.Plugins.Popup.Pages;
using SkiaSharp;
using Xamarin.Forms;

namespace HealthyApp.Views
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Patient> Patients { get; set; }
        public MainPage()
        {
            try
            {
                Patients = new ObservableCollection<Patient>();
                using (var _context = new HDBContext())
                {
                    _context.Patients.ToList().ForEach(p => Patients.Add(p));
                }

                var entries = new List<ChartEntry>();
                for (int i = -1; i < 2; i++)
                {
                    var day = DateTime.Now.AddDays(i);
                    entries.Add(new ChartEntry(Patients.Count(p => p.AddDateTime.Date == day.Date))
                    {
                        Label = day.ToShortDateString(),
                        TextColor = SKColor.Parse("#fff"),
                        Color = SKColor.Parse("#eee"),
                        ValueLabel = Patients.Count(p => p.AddDateTime.Date == day.Date).ToString()
                    });
                }
                InitializeComponent();
                mainChart.Chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    PointMode = PointMode.Square,
                    BackgroundColor = SKColor.Parse("#3399FF"),
                    LabelColor = SKColor.Parse("#fff"),
                    Margin = 0
                };
                mainChartB.Chart = new RadialGaugeChart()
                {
                    Entries = new List<ChartEntry>
                {
                    new ChartEntry(Patients.Count(p=>p.IsSick))
                    {
                        Color = SKColor.Parse("#2ecc71"),
                        TextColor = SKColor.Parse("#eee"),
                        ValueLabel = Patients.Count(p=>p.IsSick).ToString(),
                        Label = "NotSick"
                    },
                    new ChartEntry(Patients.Count(p=>p.IsSick==false))
                    {
                        Color = SKColor.Parse("#e74c3c"),
                        TextColor = SKColor.Parse("#eee"),
                        ValueLabel = Patients.Count(p=>p.IsSick==false).ToString(),
                        Label = "Sick"
                    },
                },
                    BackgroundColor = SKColor.Parse("#3399FF"),
                    LabelColor = SKColor.Parse("#fff"),
                    Margin = 0
                };
                mainCollection.ItemsSource = Patients;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void AddPatientButton_OnClicked(object sender, EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopupPage
            {
                Content = new AddPatientDialog(),
                CloseWhenBackgroundIsClicked = true
            }, true);
        }


        private void RefreshView_OnRefreshing(object sender, EventArgs e)
        {
            Patients.Clear();
            using (var _context = new HDBContext())
            {
                _context.Patients.ToList().ForEach(p => Patients.Add(p));
            }

            var entries = new List<ChartEntry>();
            for (int i = -1; i < 2; i++)
            {
                var day = DateTime.Now.AddDays(i);
                entries.Add(new ChartEntry(Patients.Count(p => p.AddDateTime.Date == day.Date))
                {
                    Label = day.ToShortDateString(),
                    TextColor = SKColor.Parse("#fff"),
                    Color = SKColor.Parse("#eee"),
                    ValueLabel = Patients.Count(p => p.AddDateTime.Date == day.Date).ToString()
                });
            }
            mainChart.Chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Square,
                BackgroundColor = SKColor.Parse("#3399FF"),
                LabelColor = SKColor.Parse("#fff"),
                Margin = 0
            };
            mainChartB.Chart = new RadialGaugeChart()
            {
                Entries = new List<ChartEntry>
                {
                    new ChartEntry(Patients.Count(p=>p.IsSick))
                    {
                        Color = SKColor.Parse("#2ecc71"),
                        TextColor = SKColor.Parse("#eee"),
                        ValueLabel = Patients.Count(p=>p.IsSick).ToString(),
                        Label = "NotSick"
                    },
                    new ChartEntry(Patients.Count(p=>p.IsSick==false))
                    {
                        Color = SKColor.Parse("#e74c3c"),
                        TextColor = SKColor.Parse("#eee"),
                        ValueLabel = Patients.Count(p=>p.IsSick==false).ToString(),
                        Label = "Sick"
                    },
                },
                BackgroundColor = SKColor.Parse("#3399FF"),
                LabelColor = SKColor.Parse("#fff"),
                Margin = 0
            };
            mainCollection.ItemsSource = Patients;
            refreshView.IsRefreshing = false;
        }
    }
}
