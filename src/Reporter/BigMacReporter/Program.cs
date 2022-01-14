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






            string str = Tweets.MinWageCompare("JPN", "TUR");// new DateTime(2021,12,20));


            Console.WriteLine(str);


        }
    }
}
