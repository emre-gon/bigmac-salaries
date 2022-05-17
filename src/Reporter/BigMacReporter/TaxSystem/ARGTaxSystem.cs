using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class ARGTaxSystem : ITaxSystem
    {
        //Source: https://en.wikipedia.org/wiki/Taxation_in_Argentina

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal socialSecurity = 0.17M;
            decimal healthCare = 0.03M;

            return AnnualGrossWage - (AnnualGrossWage * (socialSecurity + healthCare));
        }

        public override string CountryCode()
        {
            return "ARG";
        }
    }
}
