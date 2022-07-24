using BigMacReporter.Domain;
using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace BigMacReporter
{
    public static class Tweets
    {
        public static string PriceHike(string CountryCode)
        {
            var country = SlSession.NH.Get<Country>(CountryCode);
            var query = SlSession.NH.Query<BigMacPrice>()
                .Where(f => f.Country.CountryCode == CountryCode)
                .OrderByDescending(f => f.Date)
                .Select(f=>f.LocalPrice)
                .Take(2).ToList();


            var currentPrice = query.First();
            var previousPrice = query.Last();
            var hikeRate = (currentPrice - previousPrice) / previousPrice;



            int alarmRate = (int)Math.Round(Math.Max(0,Math.Log2((double)hikeRate * 100))) + 1;



            StringBuilder tweet = new StringBuilder("#zam ");
            
            for(int i = 0; i < alarmRate; i++)
            {
                tweet.Append("🚨");
            }

            tweet.AppendLine();
            tweet.AppendLine();

            tweet.AppendLine($"BigMac {currentPrice.ToString("0.##")}{country.Currency.Symbol} oldu.");

            tweet.AppendLine();

            tweet.AppendLine($"Bir önceki fiyat: {previousPrice.ToString("0.##")}{country.Currency.Symbol}");


            tweet.Append($"Zam oranı: {hikeRate.ToString("p2", new CultureInfo("tr-TR"))}");

            return tweet.ToString();

        }



        public static List<string> MinWageCompare(string Country1Code, string Country2Code,
            DateTime? Country1DateTime = null, DateTime? Country2DateTime = null,
            string Profession = "General")
        {

            var country1 = SlSession.NH.Get<Country>(Country1Code);
            var country2 = SlSession.NH.Get<Country>(Country2Code);


            var c1MinWage = country1.GetMinWage(Profession, Country1DateTime);
            var c2MinWage = country2.GetMinWage(Profession, Country2DateTime);

            var c1BigMac = country1.GetBigMacPrice(Country1DateTime);
            var c2BigMac = country2.GetBigMacPrice(Country2DateTime);


            StringBuilder tweet1 = new StringBuilder();

            string country1Year = "";
            string country2Year = "";

            string country1Month = "";
            string country2Month = "";

            if (Country1DateTime.HasValue || Country2DateTime.HasValue)
            {
                country1Year = " " +  Country1DateTime.Value.Year.ToString();
                country1Month = " " + Country1DateTime.Value.ToString("MMMM", new CultureInfo("tr-TR"));


                if (Country2DateTime.HasValue)
                {
                    country2Month = " " + Country2DateTime.Value.ToString("MMMM", new CultureInfo("tr-TR"));
                    country2Year = " " + Country2DateTime.Value.Year.ToString();
                }
                else
                {
                    country2Month = " " + DateTime.Now.Date.ToString("MMMM", new CultureInfo("tr-TR")); 
                    country2Year = " " + DateTime.Now.Date.Year;
                }
            }


                    


            tweet1.AppendLine($"{country1.EmojiFlagCode()}#{country1.CountryCode}{country1Month}{country1Year} 🆚 {country2.EmojiFlagCode()}#{country2.CountryCode}{country2Month}{country2Year}");

            tweet1.AppendLine();



            string professionStr;
            switch (Profession)
            {
                case "KYK Bursu":
                    professionStr = "KYK Bursu";
                    break;
                default:
                    professionStr = "Net asgari ücret";
                    break;
            }


            tweet1.AppendLine($"{professionStr}:");


            if(!(!string.IsNullOrEmpty(country1Year)
                && country1Year == country2Year))
            {
                country1Month = "";
                country2Month = "";
            }

            
            string country1NameTr = country1.NameTR;
            string country2NameTr = country2.NameTR;


            if(country1NameTr == country2NameTr)
            {
                country1NameTr = "";
                country2NameTr = "";
                country1Month = country1Month.Trim();
                country2Month = country2Month.Trim();
            }

            tweet1.AppendLine($"{country1NameTr}{country1Month}{country1Year}: {c1MinWage.NetLocalPrice.Value.ToString("F0")}{country1.Currency.SymbolOrCode()}");
            tweet1.AppendLine($"{country2NameTr}{country2Month}{country2Year}: {c2MinWage.NetLocalPrice.Value.ToString("F0")}{country2.Currency.SymbolOrCode()}");


            tweet1.AppendLine();


            tweet1.AppendLine("Big Mac Fiyatı:");

            tweet1.AppendLine($"{country1NameTr}{country1Month}{country1Year}: {c1BigMac.Value.ToString("F2")}{country1.Currency.SymbolOrCode()}");
            tweet1.AppendLine($"{country2NameTr}{country2Month}{country2Year}: {c2BigMac.Value.ToString("F2")}{country2.Currency.SymbolOrCode()}");


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




            string professionStr2;
            switch (Profession)
            {
                case "KYK Bursu":
                    professionStr2 = "KYK Bursunun";
                    break;
                default:
                    professionStr2 = "asgari ücretin";
                    break;
            }





            tweet1.Append($"#BigMac hesabına göre {country1Month} {c1YearDeDa}{country1NameTr.DeDa()}{professionStr2} alım gücü {country2Month} {c2CountryYearDeda} aylık net {denklik.ToString("F0")}{country2.Currency.SymbolOrCode()}'ya denk.");





            List<string> tweets = new List<string>();

            tweets.Add(tweet1.ToString());



            string professionStr3;
            switch (Profession)
            {
                case "KYK Bursu":
                    professionStr3 = "KYK Bursuyla";
                    break;
                default:
                    professionStr3 = "Asgari Ücretle";
                    break;
            }


            StringBuilder tweet2 = new StringBuilder();
            tweet2.AppendLine($"{professionStr3} Alınabilecek Big Mac Sayısı:");

            tweet2.AppendLine($"{country1NameTr}{country1Month}{country1Year}: {c1AsgariBigmacSayisi.ToString("F0")}🍔");
            tweet2.Append($"{country2NameTr}{country2Month}{country2Year}: {c2AsgariBigmacSayisi.ToString("F0")}🍔");


            tweets.Add(tweet2.ToString());

            return tweets;
        }
    }
}
