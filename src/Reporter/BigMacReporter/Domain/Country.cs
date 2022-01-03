using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BigMacReporter.Domain
{
    public class Country : ITableBase
    {

        [MaxLength(3)]
        [AnsiString]
        [Key]
        public virtual string CountryCode { get; set; }

        [MaxLength(50)]
        [Unique]
        [Required]
        public virtual string Name { get; set; }


        [Required]
        public virtual Currency Currency { get; set; }


    }



}
