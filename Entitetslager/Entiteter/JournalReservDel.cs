using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;

namespace Bilverkstad1
{
    public class JournalReservDel
    {
        public int JournalNr { get; set; }
        public Journal Journal { get; set; }

        public int ReservdelNr { get; set; }
        public ReservDel ReservDel { get; set; }
    }
}
