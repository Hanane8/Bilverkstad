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
        public UnitOfWork(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }
       
        public PersonRepository PersonRepo
        {
            get
            {
                if (_personRepository == null)
                    _personRepository = new PersonRepository(_dbContext);
                return _personRepository;
            }
        }

        public BokningsRepository BokningsRepo
        {
            get
            {
                if (_bokningsRepository == null)
                    _bokningsRepository = new BokningsRepository(_dbContext);
                return _bokningsRepository;
            }
        }

        public ReservDelRepository ReservDelRepo
        {
            get
            {
                if (_servDelRepository == null)
                    _servDelRepository = new ReservDelRepository(_dbContext);
                return _servDelRepository;
            }

        }
        public JournalRepository JournalRepo
        {
            get
            {
                if (_journalRepository == null)
                    _journalRepository = new JournalRepository(_dbContext);
                return _journalRepository;
            }

        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
