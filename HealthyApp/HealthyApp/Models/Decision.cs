using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyApp.Models
{
    public abstract class Decision<T>
    {
        public abstract bool Evaluate(T item);
    }
}
