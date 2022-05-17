using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class DNKTaxSystem : ITaxSystem
    {

        //source: https://dk.talent.com/en/tax-calculator

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {

            var federalTax = AnnualGrossWage * 0.1211M;
            var communalTax = AnnualGrossWage * 0.2082M;

            return AnnualGrossWage - federalTax - communalTax;
        }


        public override int GetTotalAnnualWorkHours()
        {
            return 1392; //source https://businessculture.org/northern-europe/denmark-business-culture/work-life-balance-in-denmark/
        }

        public override string CountryCode()
        {
            return "DNK";
        }
    }
}
