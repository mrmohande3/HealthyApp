using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyApp.Models
{
    class DecisionTree
    {
        public static DecisionQuery<Patient> GetTree()
        {
            var atypical = new DecisionQuery<Patient>
            {
                Test = (Patient) => Patient.Age <= 56 ? false : true,
                Negative = new DecisionResult<Patient> { Result = false },
                Positive = new DecisionQuery<Patient>
                {
                    Test = (Patient) => Patient.Cholestoral <= 226 ? false : true,
                    Negative = new DecisionResult<Patient> { Result = false },
                    Positive = new DecisionQuery<Patient>
                    {
                        Test = (Patient) => Patient.Age <= 68 ? false : true,
                        Positive = new DecisionResult<Patient> { Result = true },
                        Negative = new DecisionResult<Patient> { Result = false }
                    }
                }
            };
            var nonaniginalpain = new DecisionQuery<Patient>
            {
                Test = (Patient) => Patient.Sex == Sex.Male ? true : false,
                Negative = new DecisionResult<Patient> { Result = false },
                Positive = new DecisionQuery<Patient>
                {
                    Test = (Patient) => Patient.Age > 55 ? true : false,
                    Positive = new DecisionResult<Patient> { Result = true },
                    Negative = new DecisionResult<Patient> { Result = false }
                }
            };
            var asymptomatio = new DecisionQuery<Patient>
            {
                Test = (Patient) => Patient.Sex == Sex.Female ? true : false,
                Negative = new DecisionResult<Patient> { Result = true },
                Positive = new DecisionQuery<Patient>
                {
                    Test = (Patient) => Patient.BloodPressure > 139 ? true : false,
                    Positive = new DecisionResult<Patient> { Result = true },
                    Negative = new DecisionQuery<Patient>
                    {
                        Test = (Patient) => Patient.Cholestoral > 304 ? true : false,
                        Positive = new DecisionResult<Patient> { Result = true },
                        Negative = new DecisionQuery<Patient>
                        {
                            Test = (Patient) => Patient.Age > 63 ? true : false,
                            Positive = new DecisionResult<Patient> { Result = true },
                            Negative = new DecisionResult<Patient> { Result = false }
                        }
                    }
                }
            };

            var TopQuery = new DecisionQuery<Patient>
            {
                Test = (paitent) =>
                {
                    if (paitent.ChestPain == ChestPain.TypicalAngina || paitent.ChestPain == ChestPain.AtypicalAngina)
                        return true;
                    else
                        return false;
                },
                Positive = new DecisionQuery<Patient>
                {
                    Test = (patient) => patient.ChestPain == ChestPain.TypicalAngina ? true : false,
                    Positive = new DecisionResult<Patient> { Result = false },
                    Negative = atypical
                },
                Negative = new DecisionQuery<Patient>
                {
                    Test = (patient) => patient.ChestPain == ChestPain.NonAnginalPain ? true : false,
                    Negative = asymptomatio,
                    Positive = nonaniginalpain
                }
            };



            return TopQuery;

        }
    }
}
