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






            var tweets = Tweets.MinWageCompare("TUR", "TUR", 
                new DateTime(2002, 5, 1), new DateTime(2022, 7, 10),
                "KYK Bursu");


            var hikeTweet = Tweets.PriceHike("TUR");


            Console.WriteLine(tweets);
        }
    }
}
