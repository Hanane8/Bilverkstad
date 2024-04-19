using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager.Entiteter
{
    public class ReservDel
    {
        public int ReservdelNr { get; set; }
        public string Namn { get; set; }
        public double Pris { get; set; }
        public bool Lagerstatus { get; set; }

    }
}
