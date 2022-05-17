using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class CZETaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 155644, 0 }, 
            { 50270, 0.15 },
            { long.MaxValue, 0.26 }, 
        };

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {

            decimal socialSecurity = AnnualGrossWage * 0.065M;
            decimal healthInsurance = AnnualGrossWage * 0.045M;

            return AnnualGrossWage - socialSecurity - healthInsurance;
        }

        public override string CountryCode()
        {
            return "CZE";
        }
    }
}
