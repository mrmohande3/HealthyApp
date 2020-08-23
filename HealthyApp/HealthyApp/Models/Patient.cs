using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthyApp.Models
{
    public enum Sex
    {
        Male, Female
    }

    public enum ChestPain
    {
        NonAnginalPain,
        AtypicalAngina,
        TypicalAngina,
        asymptomatic
    }
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public ChestPain ChestPain { get; set; }
        public int BloodPressure { get; set; }
        public int Cholestoral { get; set; }
        public bool HeartDisease { get; set; }
        public DateTime AddDateTime { get; set; }
        public bool IsSick { get; set; }
    }
}
