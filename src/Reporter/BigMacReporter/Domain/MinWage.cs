using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigMacReporter.Domain
{
    public class MinWage : ITableBase
    {
        [Key]
        public virtual long Id { get; set; }

        [Required]
        [Unique("Date", "Type")]

        public virtual Country Country { get; set; }


        [DataType(DataType.Date)]
        public virtual DateTime Date { get; set; }

        [Index("Date")]
        public virtual string Profession { get; set; }

        public virtual decimal GrossLocalPrice { get; set; }

        public virtual decimal? NetLocalPrice { get; set; }

        public virtual string Source { get; set; }

        public virtual MinWageType Type { get; set; }


    }



    public enum MinWageType
    {
        Monthly = 1,
        Hourly = 2,
        Annually = 3

             
    }
}
