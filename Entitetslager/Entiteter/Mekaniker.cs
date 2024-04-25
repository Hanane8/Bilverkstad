﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager.Entiteter
{
    public class Mekaniker : Person
    {
        public int AnställningsNr { get; set; }
        public string Yrkesroll { get; set; }
        public string Lösenord { get; set; }
        public string Specialisering { get; set; }
        public ICollection<Bokning> Bokningar { get; set; } = new List<Bokning>();

    }
}
