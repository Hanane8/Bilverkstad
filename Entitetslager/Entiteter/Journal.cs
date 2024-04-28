using Bilverkstad1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager.Entiteter
{
    public class Journal
    {
        public int JournalNr { get; set; }
        public string Åtgärder { get; set; }
        public string RegNr { get; set; }
        public Bil Bil { get; set; }
        public Bokning Bokning { get; set; }
        public List<JournalReservDel> JournalReservDelar { get; set; }
        public int AnställningsNr { get; set; }
    }
}
