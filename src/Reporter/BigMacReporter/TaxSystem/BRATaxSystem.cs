using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class BRATaxSystem : ITaxSystem
    {
        private SortedDictionary<double, KeyValuePair<double, double>> rates = new SortedDictionary<double, KeyValuePair<double, double>>()
        {
            { 1903.98, new KeyValuePair<double,double>(0,0) },
            { 2826.65, new KeyValuePair<double,double>(0.075, 142.8)},
            { 3751.05, new KeyValuePair<double,double>(0.15, 354.8)},
            { 4664.68, new KeyValuePair<double,double>(0.225, 636.13)  },
            { double.MaxValue, new KeyValuePair<double,double>(0.275, 869.36) },
        };

        //Source: https://www2.deloitte.com/br/en/pages/living-and-working/articles/income-taxation.html

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal monthlyGross = AnnualGrossWage / 12;



            foreach (var rate in rates)
            {
                decimal maxLimit = (decimal)rate.Key;

                if (monthlyGross < maxLimit)
                {
                    decimal ratePerc = (decimal)rate.Value.Key;
                    decimal deductiblePortion = (decimal)rate.Value.Value;

                    decimal monthlyNet = monthlyGross - (monthlyGross * ratePerc) - deductiblePortion;

                    return monthlyNet * 13; //wages are paid 13 times a year
                }
            }


            return -1;
        }

        public override string CountryCode()
        {
            return "BRA";
        }
    }
}
