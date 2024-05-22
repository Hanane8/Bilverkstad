using Entitetslager.Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLager;

namespace Datalager
{
    public class MekanikerRepository : GenericRepository<Mekaniker>
    {



        public MekanikerRepository(EntityFramework dbContext) : base(dbContext)
        {
        }
        public IEnumerable<Mekaniker> HämtaAllaMekaniker() => Hämta();
        public bool VerifieraInloggning(string användarnamn, string lösenord)
        {
            Mekaniker? användare = SingleOrDefault(x => x.Namn == användarnamn);

            //Om användaren inte finns
            if (användare == null)
            {
                return false;
            }

            return användare.Lösenord == lösenord;
        }

        public Mekaniker HämtaMekanikerNr(int anställningsNr)
        {
            return SingleOrDefault(m => m.AnställningsNr == anställningsNr);
        }
    }
}
