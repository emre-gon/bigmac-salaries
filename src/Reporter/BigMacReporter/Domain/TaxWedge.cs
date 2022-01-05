using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigMacReporter.Domain
{
    public class TaxWedge : ITableBase
    {

        [Key]
        public virtual long Id { get; set; }

        [Required]
        [Unique("Date", "Profession")]

        public virtual Country Country { get; set; }


        [DataType(DataType.Date)]
        public virtual DateTime Date { get; set; }

        [Required]
        [MaxLength(100)]
        public virtual string Profession { get; set; }

        [Required]
        [MaxLength(30)]
        public virtual string Source { get; set; }
        
        public virtual decimal Rate { get; set; }


        public virtual string Description { get; set; }
    }



}
