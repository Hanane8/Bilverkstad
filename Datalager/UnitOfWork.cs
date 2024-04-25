namespace DataLager
{
    public class UnitOfWork
    {
        private PersonRepository _personRepository;
        private readonly EntityFramework _dbContext;
        private BokningsRepository _bokningsRepository;
        private ReservDelRepository _servDelRepository;
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
