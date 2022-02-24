using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class TRYTaxSystem : ITaxSystem
    {
        /// <summary>
        /// up to -> ratio
        /// </summary>
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 12570, 0 }, //personal allowance
            { 50270, 0.2 }, //basic rate
            { 150000, 0.4 }, //higher rate
            { long.MaxValue, 0.45 }, //additional rate
        };



        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {

            decimal taxableIncome = AnnualGrossWage - rates.First().Key;


            decimal rate = GetRateFromDict(taxableIncome, rates);


            return AnnualGrossWage - (taxableIncome * rate);
        }

        public override string CountryCode()
        {
            return "GBR";
        }
    }
}
