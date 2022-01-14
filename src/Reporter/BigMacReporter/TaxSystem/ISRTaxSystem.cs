using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class ISRTaxSystem : ITaxSystem
    {
        private SortedDictionary<int, double> rates = new SortedDictionary<int, double>()
        {
            { 75720, 0.1 }, 
            { 108600, 0.14 }, 
            { 174360, 0.20 },
            { 242400, 0.31 },
            { 504360, 0.35 },
            { 649560, 0.47 },
            { int.MaxValue, 0.50 }, 
        };

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal taxableIncome = AnnualGrossWage;

            foreach (var rate in rates)
            {
                decimal maxLimit = rate.Key;

                if (AnnualGrossWage < maxLimit)
                {
                    decimal ratePerc = (decimal)rate.Value;


                    return AnnualGrossWage - (taxableIncome * ratePerc);
                }
            }

            decimal maxRate = (decimal)rates.Last().Value;


            return AnnualGrossWage - (taxableIncome * maxRate);
        }

        public override string CountryCode()
        {
            return "ISR";
        }
    }
}
