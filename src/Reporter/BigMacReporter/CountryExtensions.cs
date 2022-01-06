using BigMacReporter.Domain;
using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BigMacReporter.TaxSystem;

namespace BigMacReporter
{
    public static class CountryExtensions
    {
        #region min wage
        public static WageModel GetMinWage(this Country country, DateTime? AtDate = null)
        {
            return country.GetMinWage("General", AtDate);
        }

        public static WageModel GetMinWage(this Country country, string Profession, DateTime? AtDate = null)
        {
            if(AtDate != null)
            {
                AtDate = AtDate.Value.AddDays(1);
            }

            var mw = (from z in SlSession.NH.Query<MinWage>()
                      orderby z.Date descending
                      where z.Country.CountryCode == country.CountryCode
                          && (country.DefaultMinWageSource == null || z.Source == country.DefaultMinWageSource)
                          && (AtDate == null || z.Date < AtDate)
                          && z.Profession == Profession
                      select new WageModel
                      {
                          Profession = z.Profession,
                          Date = z.Date,
                          GrossLocalPrice = z.GrossLocalPrice,
                          NetLocalPrice = z.NetLocalPrice,
                          Type = z.Type
                      }).FirstOrDefault();

            if (mw == null)
                throw new Exception("Minimum Wage is unknown for Country: " + country.Name  + ", Profession: " + Profession + ", Date: " + AtDate);


            var taxSystem = TaxSystemFactory.Instance.GetTaxSystem(country.CountryCode);

            if (mw.NetLocalPrice == null)
            {
                return taxSystem.GetMonthlyNetWage(mw, AtDate);
            }

            return taxSystem.GetMonthlyWage(mw);
        }
        #endregion

        public static decimal? GetTaxWedge(this Country country, string Profession, DateTime? AtDate = null)
        {
            var tw =  (from z in SlSession.NH.Query<TaxWedge>()
                       orderby z.Date descending
                       where z.Country.CountryCode == country.CountryCode
                           && (AtDate == null || z.Date < AtDate)
                           && z.Profession == Profession
                       select new { z.Rate }).FirstOrDefault();


            if (tw == null)
                return null;

            return tw.Rate;
        }



        public static decimal? GetBigMacPrice(this Country country, DateTime? AtDate = null)
        {
            var bm = (from z in SlSession.NH.Query<BigMacPrice>()
                      orderby z.Date descending
                      where z.Country.CountryCode == country.CountryCode
                          && (AtDate == null || z.Date < AtDate)
                      select new { z.LocalPrice }).FirstOrDefault();

            if (bm == null)
                return null;

            return bm.LocalPrice;
        }
    }
}
