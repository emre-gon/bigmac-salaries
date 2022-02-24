using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    /// <summary>
    /// Source: https://www.quora.com/How-much-tax-is-paid-on-a-salary-in-Ukraine
    /// </summary>
    public class UKRTaxSystem : ITaxSystem
    {

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal incomeTax = 0.18M;

            if(date > new DateTime(2014, 1, 1))
            {
                incomeTax += 0.015M; //military tax
            }


            return AnnualGrossWage - (AnnualGrossWage * incomeTax);

        }

        public override string CountryCode()
        {
            return "UKR";
        }
    }
}
