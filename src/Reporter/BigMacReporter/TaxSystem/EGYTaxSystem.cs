using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class EGYTaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 15000, 0 },
            { 30000, 0.025 },
            { 45000, 0.1 },
            { 60000, 0.15 },
            { 200000, 0.20 },
            { 400000, 0.225 },
            { long.MaxValue, 0.25 },
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
            return "EGY";
        }
    }
}
