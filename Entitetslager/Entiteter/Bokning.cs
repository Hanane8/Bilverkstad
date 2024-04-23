﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager.Entiteter
{
    public class Bokning
    {
        public int BokningsNr { get; set; }
        public DateTime DatumTid { get; set; }
        public DateTime? InlämningsDatum { get; set; }
        public DateTime? UtlämningsDatum { get; set; }
        public Mekaniker? AnsvarigMekaniker { get; set; }

        public Kund Kund { get; set; }
        public int AnställningsID { get; set; }
        public Bokning()
        {
            
        }
    }
}
