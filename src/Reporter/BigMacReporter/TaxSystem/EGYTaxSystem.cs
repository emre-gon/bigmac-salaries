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
            decimal rate = GetRateFromDict(AnnualGrossWage, rates);

            return AnnualGrossWage - (AnnualGrossWage * rate);
        }
        public override string CountryCode()
        {
            return "EGY";
        }
    }
}
