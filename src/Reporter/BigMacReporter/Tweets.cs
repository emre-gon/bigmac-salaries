using BigMacReporter.Domain;
using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BigMacReporter
{
    public static class Tweets
    {
        public static List<string> MinWageCompare(string Country1Code, string Country2Code, DateTime? Country1DateTime = null, DateTime? Country2DateTime = null)
        {

            var country1 = SlSession.NH.Get<Country>(Country1Code);
            var country2 = SlSession.NH.Get<Country>(Country2Code);


            var c1MinWage = country1.GetMinWage(Country1DateTime);
            var c2MinWage = country2.GetMinWage(Country2DateTime);

            var c1BigMac = country1.GetBigMacPrice(Country1DateTime);
            var c2BigMac = country2.GetBigMacPrice(Country2DateTime);


            StringBuilder tweet1 = new StringBuilder();

            string country1Year = "";
            string country2Year = "";
            if (Country1DateTime.HasValue || Country2DateTime.HasValue)
            {
                country1Year = " " +  Country1DateTime.Value.Year.ToString();

                if (Country2DateTime.HasValue)
                {
                    country2Year = " " + Country2DateTime.Value.Year.ToString();
                }
                else
                {
                    country2Year = " " + DateTime.Now.Date.Year;
                }
            }

            


            tweet1.AppendLine($"{country1.EmojiFlagCode()}#{country1.CountryCode}{country1Year} 🆚 {country2.EmojiFlagCode()}#{country2.CountryCode}{country2Year}");

            tweet1.AppendLine();

            tweet1.AppendLine("Net asgari ücret:");
             

            tweet1.AppendLine($"{country1.NameTR}{country1Year}: {c1MinWage.NetLocalPrice.Value.ToString("F0")}{country1.Currency.SymbolOrCode()}");
            tweet1.AppendLine($"{country2.NameTR}{country2Year}: {c2MinWage.NetLocalPrice.Value.ToString("F0")}{country2.Currency.SymbolOrCode()}");

            tweet1.AppendLine();


            tweet1.AppendLine("Big Mac Fiyatı:");

            tweet1.AppendLine($"{country1.NameTR}{country1Year}: {c1BigMac.Value.ToString("F2")}{country1.Currency.SymbolOrCode()}");
            tweet1.AppendLine($"{country2.NameTR}{country2Year}: {c2BigMac.Value.ToString("F2")}{country2.Currency.SymbolOrCode()}");


            tweet1.AppendLine();

            decimal c1AsgariBigmacSayisi = Math.Round(c1MinWage.NetLocalPrice.Value) / c1BigMac.Value;
            decimal c2AsgariBigmacSayisi = Math.Round(c2MinWage.NetLocalPrice.Value) / c2BigMac.Value;




            decimal denklik = c1AsgariBigmacSayisi * c2BigMac.Value;


            string c1YearDeDa = "";

            if (!string.IsNullOrEmpty(country1Year))
            {
                int year = int.Parse(country1Year);

                c1YearDeDa = year + year.ToWordsTr().DeDa(true) + " "; //2009'da
            }

            string c2CountryYearDeda = country2.NameTR.DeDa();
            if (!string.IsNullOrEmpty(country2Year))
            {

                int year = int.Parse(country2Year);

                if (country1.CountryCode == country2.CountryCode)
                {
                    c2CountryYearDeda = year + year.ToWordsTr().DeDa(true); //2022'de
                }
                else
                {
                    c2CountryYearDeda = c2CountryYearDeda + " " + year + year.ToWordsTr().DeDa(true); //Türkiye'de 2022'de
                }
            }


            tweet1.Append($"#BigMac hesabına göre {c1YearDeDa}{country1.NameTR.DeDa()} asgari ücretin alım gücü {c2CountryYearDeda} aylık net {denklik.ToString("F0")}{country2.Currency.SymbolOrCode()}'ya denk.");





            List<string> tweets = new List<string>();

            tweets.Add(tweet1.ToString());



            StringBuilder tweet2 = new StringBuilder();
            tweet2.AppendLine("Asgari Ücretle Alınabilecek Big Mac Sayısı:");

            tweet2.AppendLine($"{country1.NameTR}{country1Year}: {c1AsgariBigmacSayisi.ToString("F0")}🍔");
            tweet2.Append($"{country2.NameTR}{country2Year}: {c2AsgariBigmacSayisi.ToString("F0")}🍔");


            tweets.Add(tweet2.ToString());
        }
    }
}
