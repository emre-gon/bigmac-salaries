using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem.Base
{
    public class KAZTaxSystem : ITaxSystem
    {
        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            return AnnualGrossWage - (AnnualGrossWage * 0.10M);
        }

        public override string CountryCode()
        {
            return "KAZ";
        }
    }
}
