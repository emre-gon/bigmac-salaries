using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class USATaxSystem : ITaxSystem
    {
        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            //source: https://www.sapling.com/8411457/much-wage-month-after-taxes

            var netWage = AnnualGrossWage - AnnualGrossWage * 0.1093M;

            //federal + state + social security + medicare avg (New York)

            return netWage;
        }

        public override string CountryCode()
        {
            return "USA";
        }
    }
}
