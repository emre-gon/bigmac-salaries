using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter
{
    public static class TRLanguageExtensions
    {
        public static string DeDa(this string Str, bool DeDaOnly = false)
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
            for (int i = Str.Length - 1; i >= 0; i--)
            {
                if (inceler.Contains(Str[i]))
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

            string tbr = DeDaOnly ? "" : Str;


            tbr = tbr + "'";
            if (isSert)
            {
                tbr = tbr + "t";
            }
            else
            {
                tbr = tbr + "d";
            }

            if (isKalin)
            {
                tbr = tbr + "a";
            }
            else
            {
                tbr = tbr + "e";
            }

            return tbr;

        }

        private static readonly string[] sayilar = { "sıfır", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
        private static readonly string[] onluklar = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
        private static readonly string[] birimler = { "yüz", "bin", "milyon", "milyar", "trilyon", "katrilyon", "ketilyon" };


        public static string ToWordsTr(this int Source)
        {
            string ToBeReturned = "";
            if (Source < 0)
            {
                ToBeReturned += "eksi ";
                Source = Math.Abs(Source);
            }
            else if (Source == 0)
                return sayilar[0];

            string str = Source.ToString();


            for (int i = 0; i < str.Length; i++)
            {
                int num = (int)Char.GetNumericValue(str[i]);

                int mod = (str.Length - i - 1) % 3;
                if (mod == 0)
                {
                    int currentBirim = (str.Length - i) / 3;

                    if (num != 0)
                    {
                        if (!(currentBirim == 1 && num == 1 && str.Length == 4))
                            ToBeReturned += sayilar[num];
                    }

                    if (currentBirim != 0)
                        ToBeReturned += birimler[currentBirim];

                }
                else if (mod == 1)
                {
                    ToBeReturned += onluklar[num];
                }
                else
                {
                    if (num > 1)
                        ToBeReturned += sayilar[num];

                    if (num != 0)
                        ToBeReturned += birimler[0];

                }

            }

            return ToBeReturned.Trim();
        }
    }
}
