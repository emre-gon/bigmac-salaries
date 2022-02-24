using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    /// <summary>
    /// Source: https://www.quora.com/How-much-tax-is-paid-on-a-salary-in-Ukraine
    /// </summary>
    public class TWNTaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 540000, 0.0192 },
            { 1210000, 0.064 },
            { 2420000, 0.1088 },
            { 4530000, 0.16 },
            { 10310000, 0.1792 },
            { long.MaxValue, 0.45 },
        };

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal rate = GetRateFromDict(AnnualGrossWage, rates);
            return AnnualGrossWage - (AnnualGrossWage * rate);
        }

        public override string CountryCode()
        {
            return "TWN";
        }
    }
}
