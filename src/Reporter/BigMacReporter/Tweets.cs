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
        public static string MinWageCompare(string Country1Code, string Country2Code, DateTime? dateTime = null)
        {

            var country1 = SlSession.NH.Get<Country>(Country1Code);
            var country2 = SlSession.NH.Get<Country>(Country2Code);


            var c1MinWage = country1.GetMinWage(dateTime);
            var c2MinWage = country2.GetMinWage(dateTime);

            var c1BigMac = country1.GetBigMacPrice(dateTime);
            var c2BigMac = country2.GetBigMacPrice(dateTime);


            StringBuilder tweet = new StringBuilder();


            tweet.AppendLine($"{country1.EmojiFlagCode()}#{country1.CountryCode} 🆚 {country2.EmojiFlagCode()}#{country2.CountryCode}");


            tweet.AppendLine("Net asgari ücret:");
             

            tweet.AppendLine($"{country1.NameTR}: {c1MinWage.NetLocalPrice.Value.ToString("F0")}{country1.Currency.SymbolOrCode()}");
            tweet.AppendLine($"{country2.NameTR}: {c2MinWage.NetLocalPrice.Value.ToString("F0")}{country2.Currency.SymbolOrCode()}");

            tweet.AppendLine();


            tweet.AppendLine("Big Mac Fiyatı:");

            tweet.AppendLine($"{country1.NameTR}: {c1BigMac.Value.ToString("F2")}{country1.Currency.SymbolOrCode()}");
            tweet.AppendLine($"{country2.NameTR}: {c2BigMac.Value.ToString("F2")}{country2.Currency.SymbolOrCode()}");


            tweet.AppendLine();

            decimal c1AsgariBigmacSayisi = Math.Round(c1MinWage.NetLocalPrice.Value) / c1BigMac.Value;
            decimal c2AsgariBigmacSayisi = Math.Round(c2MinWage.NetLocalPrice.Value) / c2BigMac.Value;




            decimal denklik = c1AsgariBigmacSayisi * c2BigMac.Value;

            tweet.AppendLine($"#BigMac hesabına göre {country1.NameTR.DeDa()} asgari ücret {country2.NameTR.DeDa()} {denklik.ToString("F0")}{country2.Currency.SymbolOrCode()}'ya denk");



            string str =  tweet.ToString();




            StringBuilder tweet2 = new StringBuilder();
            tweet2.AppendLine("Asgari Ücretle Alınabilecek Big Mac Sayısı:");

            tweet2.AppendLine($"{country1.NameTR}: {c1AsgariBigmacSayisi.ToString("F0")}🍔");
            tweet2.AppendLine($"{country2.NameTR}: {c2AsgariBigmacSayisi.ToString("F0")}🍔");



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
