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
        public static string MinWageCompare(string Country1Code, string Country2Code, DateTime? Country1DateTime = null, DateTime? Country2DateTime = null)
        {

            var country1 = SlSession.NH.Get<Country>(Country1Code);
            var country2 = SlSession.NH.Get<Country>(Country2Code);


            var c1MinWage = country1.GetMinWage(Country1DateTime);
            var c2MinWage = country2.GetMinWage(Country2DateTime);

            var c1BigMac = country1.GetBigMacPrice(Country1DateTime);
            var c2BigMac = country2.GetBigMacPrice(Country2DateTime);


            StringBuilder tweet = new StringBuilder();

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

            


            tweet.AppendLine($"{country1.EmojiFlagCode()}#{country1.CountryCode}{country1Year} 🆚 {country2.EmojiFlagCode()}#{country2.CountryCode}{country2Year}");

            tweet.AppendLine();

            tweet.AppendLine("Net asgari ücret:");
             

            tweet.AppendLine($"{country1.NameTR}{country1Year}: {c1MinWage.NetLocalPrice.Value.ToString("F0")}{country1.Currency.SymbolOrCode()}");
            tweet.AppendLine($"{country2.NameTR}{country2Year}: {c2MinWage.NetLocalPrice.Value.ToString("F0")}{country2.Currency.SymbolOrCode()}");

            tweet.AppendLine();


            tweet.AppendLine("Big Mac Fiyatı:");

            tweet.AppendLine($"{country1.NameTR}{country1Year}: {c1BigMac.Value.ToString("F2")}{country1.Currency.SymbolOrCode()}");
            tweet.AppendLine($"{country2.NameTR}{country2Year}: {c2BigMac.Value.ToString("F2")}{country2.Currency.SymbolOrCode()}");


            tweet.AppendLine();

            decimal c1AsgariBigmacSayisi = Math.Round(c1MinWage.NetLocalPrice.Value) / c1BigMac.Value;
            decimal c2AsgariBigmacSayisi = Math.Round(c2MinWage.NetLocalPrice.Value) / c2BigMac.Value;




            decimal denklik = c1AsgariBigmacSayisi * c2BigMac.Value;

            tweet.Append($"#BigMac hesabına göre {country1.NameTR.DeDa()} asgari ücretin alım gücü {country2.NameTR.DeDa()} aylık net {denklik.ToString("F0")}{country2.Currency.SymbolOrCode()}'ya denk.");



            string str =  tweet.ToString();




            StringBuilder tweet2 = new StringBuilder();
            tweet2.AppendLine("Asgari Ücretle Alınabilecek Big Mac Sayısı:");

            tweet2.AppendLine($"{country1.NameTR}{country1Year}: {c1AsgariBigmacSayisi.ToString("F0")}🍔");
            tweet2.Append($"{country2.NameTR}{country2Year}: {c2AsgariBigmacSayisi.ToString("F0")}🍔");


            string str2 = tweet2.ToString();
            return str;

        }


        public static string DeDa(this string Str)
        {
            string fistikciSahap = "fstkçşhp";
            string kalinlar = "aıou";
            string inceler = "eiöü";


            char sonHarf = Str[Str.Length - 1];


            bool isSert = false;
            if (fistikciSahap.Contains(sonHarf))
            {
                isSert = true;
            }


            bool isKalin = false;
            for(int i = Str.Length -1; i >= 0; i--)
            {
                if(inceler.Contains(Str[i]))
                {
                    isKalin = false;
                    break;
                }
                else if (kalinlar.Contains(Str[i]))
                {
                    isKalin = true;
                    break;
                }
                else
                {
                    continue;
                }
            }

            string tbr = Str + "'";
            if (isSert)
            {
                tbr = tbr + "t";
            }
            else
            {
                tbr = tbr + "d";
            }

            if(isKalin)
            {
                tbr = tbr + "a";
            }
            else
            {
                tbr = tbr + "e";
            }

            return tbr;

        }


    }
}
