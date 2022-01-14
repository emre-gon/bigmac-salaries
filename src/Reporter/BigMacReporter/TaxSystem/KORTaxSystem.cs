using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem.Base
{
    public class KORTaxSystem : ITaxSystem
    {
        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            return AnnualGrossWage - (AnnualGrossWage * 0.118M);
        }

        public override string CountryCode()
        {
            return "KOR";
        }

        public override WageModel GetAnnualWage(WageModel input)
        {
            if(input.Type == Domain.MinWageType.Hourly)
            {
                WageModel tbr = new WageModel()
                {
                    Type = Domain.MinWageType.Annually,
                    Profession = input.Profession,
                    Date = input.Date,
                    TaxWedge = input.TaxWedge
                };
                tbr.GrossLocalPrice = input.GrossLocalPrice * 2508;
                tbr.NetLocalPrice = input.NetLocalPrice * 2508;

                return tbr;
            }

            return base.GetAnnualWage(input);
        }
    }
}
