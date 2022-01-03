using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigMacReporter.Domain
{
    public class Currency : ITableBase
    {
        [Key]
        [MaxLength(3)]
        public virtual string CurrencyCode { get; set; }


        [Unique]
        [Required]
        [MaxLength(50)]
        public virtual string Name { get; set; }
    }
}
