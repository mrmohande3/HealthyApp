using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyApp.Models
{
    class DecisionResult<T> : Decision<T>
    {
        public bool Result { get; set; }
        public double ResultRank { get; set; }
        public override bool Evaluate(T item)
        {
            return Result;
        }
    }
}
