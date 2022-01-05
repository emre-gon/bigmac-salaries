using BigMacReporter.Domain;
using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BigMacReporter
{
    public static class CountryExtensions
    {
        #region min wage
        public static MinWageModel GetMinWage(this Country country, DateTime? AtDate = null)
        {
            return country.GetMinWage("General", AtDate);
        }



        public class MinWageModel
        {
            public string Profession { get; set; }
            public DateTime Date { get; set; }
            public decimal GrossLocalPrice { get; set; }

            public decimal? NetLocalPrice { get; set; }

            public decimal? TaxWedge { get; set; }
            public MinWageType Type { get; set; }


            public void ConvertToMonthly()
            {
                if(Type == MinWageType.Annually)
                {
                    GrossLocalPrice = GrossLocalPrice / 12;
                    NetLocalPrice = NetLocalPrice / 12;
                }
                else if(Type == MinWageType.Hourly)
                {
                    GrossLocalPrice = 37.5M * GrossLocalPrice;
                    NetLocalPrice = 37.5M * NetLocalPrice;
                }
            }
        }

        public static MinWageModel GetMinWage(this Country country, string Profession, DateTime? AtDate = null)
        {
            if(AtDate != null)
            {
                AtDate = AtDate.Value.AddDays(1);
            }



            var mw = (from z in SlSession.NH.Query<MinWage>()
                      orderby z.Date descending
                      where z.Country.CountryCode == country.CountryCode
                          && (AtDate == null || z.Date < AtDate)
                          && z.Profession == Profession
                      select new MinWageModel
                      {
                          Profession = z.Profession,
                          Date = z.Date,
                          GrossLocalPrice = z.GrossLocalPrice,
                          NetLocalPrice = z.NetLocalPrice,
                          Type = z.Type
                      }).FirstOrDefault();

            if (mw == null)
                throw new Exception("Minimum Wage is unknown for Country: " + country.Name  + ", Profession: " + Profession + ", Date: " + AtDate);



            if(mw.NetLocalPrice == null)
            {
                var tw = country.GetTaxWedge("Min Wage", AtDate);

                if(tw == null)
                {
                    tw = country.GetTaxWedge("General", AtDate);
                }

                if(tw == null)
                {
                    throw new Exception("Tax Wedge is unknown. Cannot calculate net local price for " + country.Name + ", Profession: " + Profession + ", Date: " + AtDate);
                }

                mw.TaxWedge = tw;
                mw.NetLocalPrice = mw.GrossLocalPrice - (mw.GrossLocalPrice * tw);

            }

            mw.ConvertToMonthly();
            return mw;
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
