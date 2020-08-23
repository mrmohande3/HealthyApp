using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyApp.Models
{
    public class DecisionQuery<T> : Decision<T>
    {
        public string Title { get; set; }
        public Decision<T> Positive { get; set; }
        public Decision<T> Negative { get; set; }
        public Func<T, bool> Test { get; set; }

        public override bool Evaluate(T client)
        {
            bool result = this.Test(client);
            if (result)
            {
                return this.Positive.Evaluate(client);
            }
            else
            {
                return this.Negative.Evaluate(client);
            }
        }
    }
}
