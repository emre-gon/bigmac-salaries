using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class ISRTaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 75720, 0.1 }, 
            { 108600, 0.14 }, 
            { 174360, 0.20 },
            { 242400, 0.31 },
            { 504360, 0.35 },
            { 649560, 0.47 },
            { long.MaxValue, 0.50 }, 
        };

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal rate = GetRateFromDict(AnnualGrossWage, rates);

            return AnnualGrossWage - (AnnualGrossWage * rate);
        }

        public override string CountryCode()
        {
            return "ISR";
        }
    }
}
