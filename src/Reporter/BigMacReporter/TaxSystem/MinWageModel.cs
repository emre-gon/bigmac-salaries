using BigMacReporter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigMacReporter.TaxSystem
{
    public class WageModel
    {
        public string Profession { get; set; }
        public DateTime Date { get; set; }
        public decimal GrossLocalPrice { get; set; }

        public decimal? NetLocalPrice { get; set; }

        public decimal? TaxWedge { get; set; }
        public MinWageType Type { get; set; }
    }
}
