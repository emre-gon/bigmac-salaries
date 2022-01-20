using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem.Base
{
    public class KORTaxSystem : ITaxSystem
    {
        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            return AnnualGrossWage - (AnnualGrossWage * 0.118M);
        }

        public override string CountryCode()
        {
            return "KOR";
        }


        public override int GetTotalAnnualWorkHours()
        {
            return 2508;
        }

    }
}
