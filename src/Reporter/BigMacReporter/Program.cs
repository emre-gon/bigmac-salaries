using BigMacReporter.Domain;
using FluentNHibernate.Cfg.Db;
using Sl.DataAccess.NH;
using Sl.DataAccess.NH.AutoMap;
using System;
using System.Linq;
using System.Reflection;

namespace BigMacReporter
{
    class Program
    {
        static void Main(string[] args)
        {

            var dbConfig = SQLiteConfiguration.Standard.UsingFile("../../../../../../data/db.sqlite");


            SlSession.ConfigureSessionFactory(Assembly.GetAssembly(typeof(BigMacPrice)),
                dbConfig, SessionContextType.ThreadStatic,
                null,
                 DBSchemaUpdateMode.Update_Tables);




            if (!SlSession.NH.Query<CurrencyUSDValue>().Any(f => f.Currency.CurrencyCode == "USD"))
            {
                using (var trns = SlSession.NH.BeginTransaction())
                {
                    CurrencyUSDValue v = new CurrencyUSDValue()
                    {
                        Currency = SlSession.NH.Load<Currency>("USD"),
                        Close = 1,
                        Date = new DateTime(1900, 1, 1)
                    };

                    SlSession.NH.Save(v);
                    trns.Commit();
                }
            }




            string str = Tweets.MinWageCompare("GBR", "TUR");// new DateTime(2021,12,20));


            Console.WriteLine(str);


        }
    }
}
