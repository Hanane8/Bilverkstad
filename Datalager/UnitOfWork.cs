using Datalager;

namespace DataLager
{
    public class UnitOfWork
    {
        private PersonRepository _personRepository;
        private readonly EntityFramework _dbContext;
        private BokningsRepository _bokningsRepository;
        private ReservDelRepository _servDelRepository;
        private JournalRepository _journalRepository;
        private BilRepository _bilRepository;
        private MekanikerRepository _mekanikerRepository;
        public UnitOfWork(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }
        
        /// <summary>
        /// Metod som ansvarar för att låta andra
        /// metoder/klasser använda repository för operationer
        /// angående kund
        /// </summary>
        public PersonRepository PersonRepo
        {
            get
            {
                if (_personRepository == null)
                    _personRepository = new PersonRepository(_dbContext);
                return _personRepository;
            }
        }
        /// <summary>
        /// Metod som ansvarar för att låta andra
        /// metoder/klasser använda repository för operationer
        /// angående Bokningar
        /// </summary>
        public BokningsRepository BokningsRepo
        {
            get
            {
                if (_bokningsRepository == null)
                    _bokningsRepository = new BokningsRepository(_dbContext);
                return _bokningsRepository;
            }
        }
        /// <summary>
        /// Metod som ansvarar för att låta andra
        /// metoder/klasser använda repository för operationer
        /// angående Reservdelar
        /// </summary>
        public ReservDelRepository ReservDelRepo
        {
            get
            {
                if (_servDelRepository == null)
                    _servDelRepository = new ReservDelRepository(_dbContext);
                return _servDelRepository;
            }

        }
        /// <summary>
        /// Metod som ansvarar för att låta andra
        /// metoder/klasser använda repository för operationer
        /// angående Journal
        /// </summary>
        public JournalRepository JournalRepo
        {
            get
            {
                if (_journalRepository == null)
                    _journalRepository = new JournalRepository(_dbContext);
                return _journalRepository;
            }

        }

        public BilRepository BilRepo
        {
            get
            {
                if (_bilRepository == null)
                    _bilRepository = new BilRepository(_dbContext);
                return _bilRepository;
            }
        }

        public MekanikerRepository MekanikerRepo
        {
            get
            {
                if (_mekanikerRepository == null)
                    _mekanikerRepository = new MekanikerRepository(_dbContext);
                return _mekanikerRepository;
            }
        }

        /// <summary>
        /// Metod som ser till att all ny information skriven
        /// till databasen sparas för permanent lagring
        /// </summary>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
