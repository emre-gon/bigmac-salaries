using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem.Base
{
    public class JPNTaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 1950000, 0.05 },
            { 3300000, 0.10 },
            { 6950000, 0.20 },
            { 9000000, 0.23 },
            { 18000000, 0.33 },
            { 40000000, 0.40 },
            { long.MaxValue, 0.45 },
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
            return "JPN";
        }

        public override int GetTotalAnnualWorkHours()
        {
            return 2160;
        }
    }
}
