using Sl.DataAccess.NH;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [MaxLength(50)]
        [Unique]
        [Required]
        public virtual string NameTR { get; set; }



        [Required]
        public virtual Currency Currency { get; set; }

        [MaxLength(3)]
        [AnsiString]
        public virtual string FlagCode { get; set; }


        public virtual string EmojiFlagCode()
        {
            return string.Concat(FlagCode.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
        }
    }



}
