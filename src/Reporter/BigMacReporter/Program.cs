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



            var enfTweet = Tweets.Enflasyon("TUR");

            var hikeTweet = Tweets.PriceHike("TUR");




            var tweets = Tweets.MinWageCompare("GBR", "TUR");


            var tweets2 = Tweets.MinWageCompare("TUR", "GBR");

            Console.WriteLine(tweets);
        }
    }
}
