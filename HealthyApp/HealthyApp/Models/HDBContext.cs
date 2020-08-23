using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace HealthyApp.Models
{
    public class HDBContext : DbContext
    {
        public HDBContext()
        {
            try
            {
                Database.EnsureCreatedAsync();
            }
            catch (Exception e)
            {
                Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopupPage
                {
                    Content = new Frame
                    {
                        Content = new Label { Text = e.Message }
                    }
                });
            }
        }
        public DbSet<Patient> Patients { get; set; }

        private const string databaseName = "HdatabaseB.db3";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String databasePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName); ;
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
