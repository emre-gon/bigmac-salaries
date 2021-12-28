using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigMacReporter.Domain
{
    public class CurrencyUSDValue : ITableBase
    {
        [Key]
        public virtual long Id { get; set; }


        [MaxLength(3)]
        [Required]
        [Unique("Date")]
        public virtual Currency Currency { get; set; }

        [DataType(DataType.Date)]
        public virtual DateTime Date { get; set; }

        
        public virtual decimal Close { get; set; }
    }
}
