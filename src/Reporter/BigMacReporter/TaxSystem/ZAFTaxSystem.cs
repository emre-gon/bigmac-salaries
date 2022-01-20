using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class ZAFTaxSystem : ITaxSystem
    {
        /// <summary>
        /// up to -> ratio
        /// </summary>
        private SortedDictionary<int, KeyValuePair<double, double>> rates = new SortedDictionary<int, KeyValuePair<double,double>>()
        {
            { 216200,  new KeyValuePair<double,double>(0,0.18) }, 
            { 337800,  new KeyValuePair<double,double>(38916,0.26) }, 
            { 467500,  new KeyValuePair<double,double>(70532,0.31) }, 
            { 613600,  new KeyValuePair<double,double>(110739,0.36) }, 
            { 782200,  new KeyValuePair<double,double>(163335,0.39) }, 
            { 1656600,  new KeyValuePair<double,double>(229089,0.41) },
            { int.MaxValue,  new KeyValuePair<double,double>(587593,0.45) }, 
        };

        ///Source: https://eu-assets.contentstack.com/v3/assets/blt0554f48052bb4620/blt9d1461fa8d7c0944/6036427d2bb1fa4862543cad/Budget_2021_Tax_Guide.pdf
        ///https://www.oldmutual.co.za/personal/tools-and-calculators/income-tax-calculator/
        ///
        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {

            decimal taxableIncome = AnnualGrossWage - 87300;
            if (taxableIncome <= 0)
                return AnnualGrossWage;

            foreach (var rate in rates)
            {
                decimal maxLimit = rate.Key;

                if (taxableIncome < maxLimit)
                {
                    return getAmount(taxableIncome, rate.Value, AnnualGrossWage);
                }
            }

            return getAmount(taxableIncome, rates.Last().Value, AnnualGrossWage);
        }


        private decimal getAmount(decimal taxableIncome, KeyValuePair<double,double> rate, decimal AnnualGrossWage)
        {
            decimal rateAmount = (decimal)rate.Key;
            decimal ratePerc = (decimal)rate.Value;

            decimal ratePercTax = (taxableIncome - rateAmount) * ratePerc; //eg: %26 of taxable income above 216200

            return AnnualGrossWage - rateAmount - ratePercTax;
        }

        public override string CountryCode()
        {
            return "ZAF";
        }

        public override int GetTotalAnnualWorkHours()
        {
            return 2080;
        }

    }
}
