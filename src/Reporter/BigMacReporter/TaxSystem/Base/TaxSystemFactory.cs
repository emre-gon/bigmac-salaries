using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class TaxSystemFactory
    {
        private Dictionary<string, ITaxSystem> taxSystemMap;
        private TaxSystemFactory()
        {
            taxSystemMap = new Dictionary<string, ITaxSystem>();
            var taxSystemTypes = Assembly.GetAssembly(typeof(ITaxSystem))
                .GetTypes().Where(f => !f.IsAbstract && f.IsSubclassOf((typeof(ITaxSystem))));


            foreach (var type in taxSystemTypes)
            {
                if(type == typeof(GenericTaxSystem))
                {
                    continue;
                }

                var typeObj = (ITaxSystem)Activator.CreateInstance(type);

                taxSystemMap[typeObj.CountryCode()] = typeObj;

            }
        }

        private static TaxSystemFactory _TaxSystemFactory;
        public static TaxSystemFactory Instance
        {
            get
            {
                if (_TaxSystemFactory == null)
                {
                    _TaxSystemFactory = new TaxSystemFactory();
                }

                return _TaxSystemFactory;
            }
        }


        public ITaxSystem GetTaxSystem(string CountryCode)
        {
            if (taxSystemMap.ContainsKey(CountryCode))
            {
                return taxSystemMap[CountryCode];
            }


            return new GenericTaxSystem(CountryCode);
        }
    }
}
