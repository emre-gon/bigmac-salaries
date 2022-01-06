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
        private SortedDictionary<int, double> rates = new SortedDictionary<int, double>()
        {
            { 12570, 0 }, //personal allowance
            { 50270, 0.2 }, //basic rate
            { 150000, 0.4 }, //higher rate
            { int.MaxValue, 0.45 }, //additional rate
        };



        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {

            decimal taxableIncome = AnnualGrossWage - rates.First().Key;

            foreach(var rate in rates)
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
            return "GBR";
        }
    }
}
