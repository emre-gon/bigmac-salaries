using BigMacReporter.Domain;
using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class GenericTaxSystem : ITaxSystem
    {
        private string _countryCode;
        public GenericTaxSystem(string CountryCode)
        {
            this._countryCode = CountryCode;
        }

        public override decimal CalculateAnnualNetWage(decimal AnnualGrossWage, DateTime date)
        {
            var country = SlSession.NH.Get<Country>(_countryCode);

            var tw = country.GetTaxWedge("Min Wage", date);

            if (tw == null)
            {
                tw = country.GetTaxWedge("General", date);
            }

            if (tw == null)
            {
                throw new Exception("Tax Wedge is unknown. Cannot calculate net local price for " + country.Name + ", Profession: Min Wage, Date: " + date);
            }


            return AnnualGrossWage - (AnnualGrossWage * tw.Value);
        }

        public override string CountryCode()
        {
            return _countryCode;
        }
    }
}
