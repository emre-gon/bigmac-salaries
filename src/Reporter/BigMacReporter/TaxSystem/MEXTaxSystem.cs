using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class MEXTaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 5952, 0.0192 },
            { 50524, 0.064 },
            { 88793, 0.1088 },
            { 103218, 0.16 },
            { 123580, 0.1792 },
            { 249243, 0.2136 },
            { 392841, 0.2352 },
            { 750000, 0.30 },
            { 1000000, 0.32 },
            { 3000000, 0.34 },
            { long.MaxValue, 0.35 },
        };

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal taxableIncome = AnnualGrossWage;
            decimal monthlyGrossWage = AnnualGrossWage / 12;

            //monthly gross wage üzerinden hesaplanıyor
            decimal rate = GetRateFromDict(monthlyGrossWage, rates);

            return AnnualGrossWage - (AnnualGrossWage * rate);

        }

        public override string CountryCode()
        {
            return "MEX";
        }
    }
}
