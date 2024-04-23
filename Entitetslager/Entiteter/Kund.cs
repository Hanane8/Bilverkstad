using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager.Entiteter
{
    public class Kund : Person
    {
        public int KundNr { get; }
        public List<Bokning>? Bokningar { get; set; } = new List<Bokning>();
    }
}
