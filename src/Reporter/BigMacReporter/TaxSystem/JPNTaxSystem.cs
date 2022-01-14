using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigMacReporter.TaxSystem.Base
{
    public class JPNTaxSystem : ITaxSystem
    {
        private SortedDictionary<long, double> rates = new SortedDictionary<long, double>()
        {
            { 1950000, 0.05 },
            { 3300000, 0.10 },
            { 6950000, 0.20 },
            { 9000000, 0.23 },
            { 18000000, 0.33 },
            { 40000000, 0.40 },
            { long.MaxValue, 0.45 },
        };

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            decimal taxableIncome = AnnualGrossWage;

            foreach (var rate in rates)
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
            return "JPN";
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
                tbr.GrossLocalPrice = input.GrossLocalPrice * 2160;
                tbr.NetLocalPrice = input.NetLocalPrice * 2160;

                return tbr;
            }

            return base.GetAnnualWage(input);
        }
    }
}
