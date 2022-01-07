using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class RUSTaxSystem : ITaxSystem
    {
        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            return AnnualGrossWage - (AnnualGrossWage * 0.13M);
        }

        public override string CountryCode()
        {
            return "RUS";
        }
    }
}
