using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager.Entiteter
{
    public class Bokning
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BokningsNr { get; set; }
        public DateTime? InlämningsDatum { get; set; }
        public DateTime? UtlämningsDatum { get; set; }
        public int KundNr { get; set; }
        public int AnställningsNr { get; set; }

    }
}
