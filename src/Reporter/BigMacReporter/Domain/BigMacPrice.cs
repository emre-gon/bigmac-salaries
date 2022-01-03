using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigMacReporter.Domain
{
    public class BigMacPrice : ITableBase
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [Unique("Date")]
        public virtual Country Country { get; set; }


        [DataType(DataType.Date)]
        public virtual DateTime Date { get; set; }


        public virtual decimal LocalPrice { get; set; }

        public virtual decimal USDPrice { get; set; }

        [MaxLength(50)]
        public virtual string Source { get; set; }
    }
}
